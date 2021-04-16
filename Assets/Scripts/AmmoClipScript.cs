using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClipScript : MonoBehaviour
{
    //How much ammo can the clip hold and how much is in it
    [SerializeField] float initialAmmo = 25;
    [SerializeField] float ammo = 25;
    
    //In what hand are we holding the ammo clip. 
    bool isRight;

    //This talks to the hand
    PickUpScript pickUpScript;

    //This shows us how many bullets we have left in the clip
    Transform ammoCounter;


    void Start()
    {
        //Initialize the things
        pickUpScript = null; 
        ammoCounter = gameObject.transform.GetChild(0).transform;
    }

    void Update()
    {
        //Get the hand that feeds us. Only when the item gets picked up
        if (transform.parent != null)
        {
            pickUpScript = transform.parent.gameObject.GetComponent<PickUpScript>();

            //Check if the hand want to use the item, but only if there are bullets left
            if (pickUpScript.useItem)
            {
                if (ammo > 0)
                {
                    Use();
                }
            }

            //Check if this is being held in right or the left hand
            isRight = pickUpScript.isRight;
        }
        //If there is no hand to feed us we set this to null
        else
        {
            pickUpScript = null;
        }
    }

    void Use()
    {
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
