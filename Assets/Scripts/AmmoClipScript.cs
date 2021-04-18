using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClipScript : UsableObject
{
    //How much ammo can the clip hold and how much is in it
    [SerializeField] float initialAmmo = 25;
    [SerializeField] float ammo = 25;
    
    //In what hand are we holding the ammo clip. 
    bool isRight;

    //This shows us how many bullets we have left in the clip
    Transform ammoCounter;

    //initialize the pickup specific things
    public override void Initialize()
    {
        base.Initialize();
        ammoCounter = gameObject.transform.GetChild(0).transform;
    }

    public override void Use()
    {
        if (ammo > 0)
        {
            //Check if the clip is picked up and what hand is holding it
            if (isPickedUp)
            {
                isRight = pickUpScript.isRight;
            }

            Debug.Log("Use Ammo Clip");

        //Tell the hand we only want to be used once
        pickUpScript.useItem = false;

            Transform otherHand;
            HeldItemsScript helditemScript;
            helditemScript = transform.root.GetComponent<HeldItemsScript>();

            //Check what the other hand is holding
            if (isRight)
            {
                otherHand = helditemScript.itemLeft;
            }
            else
            {
                otherHand = helditemScript.itemRight;
            }

            //Make sure that the other hand is not empty
            if (otherHand != null)
            {
                //Check if the other hand is holding a gun 
                if (otherHand.CompareTag("Gun"))
                {
                    //Check how many bullets the gun has left
                    GunScript gunScript;
                    gunScript = otherHand.GetComponent<GunScript>();

                    float gunAmmo = otherHand.GetComponent<GunScript>().ammo;
                    float gunInitialAmmo = otherHand.GetComponent<GunScript>().initialAmmo;

                    Debug.Log(gunAmmo);

                    // Check if we need to reload the gun. We don't need to reload a gun that's full
                    if (gunAmmo < gunInitialAmmo)
                    {
                        //Calculate how many bullets we need to add to the gun and how much needs to be removed from the ammo clip. 
                        if (gunScript.ammo + ammo > gunScript.initialAmmo)
                        {
                            float difference;
                            difference = gunScript.initialAmmo - gunScript.ammo;
                            ammo -= difference;
                            gunScript.ammo = gunScript.initialAmmo;

                            //Update the ammo bar
                            float ammoPercentage = 1 * (ammo / initialAmmo);
                            ammoCounter.localScale = new Vector3(ammoCounter.localScale.x, ammoCounter.localScale.y, ammoPercentage);
                        }
                        //We don't need to calculate anything if there are not enough bullets in the clip to overflow the gun
                        else
                        {
                            gunScript.ammo += ammo;
                            ammo = 0;

                            //Set the ammo bar to zero
                            ammoCounter.localScale = new Vector3(ammoCounter.localScale.x, ammoCounter.localScale.y, 0);
                        }
                    }

                }
            }
        }
    }
}
