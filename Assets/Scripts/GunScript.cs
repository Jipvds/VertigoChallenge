using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //Set the speed of the bullet
    [SerializeField] float bulletSpeed = 10;

    //Set the firerate of the bullet
    [SerializeField] float fireRate;

    //Is the gun in single fire or automatic mode
    [SerializeField] bool automatic;

    //How much ammo can the gun hold
    public float initialAmmo = 25;

    //How much ammo is in the gun right now
    public float ammo;

    //This times if the gun can fire again
    float nextFire;

    //This talks to the hand
    PickUpScript pickUpScript;

    //This is where the bullets come from
    Transform origin;

    //This shows us how many bullets we have left in the gun
    Transform ammoCounter;

    //Set the key that toggles the automatic or single fire mode
    KeyCode automaticKey = KeyCode.LeftShift;


    void Start()
    {
        //Initialize the things
        origin = gameObject.transform.GetChild(0).transform;
        ammoCounter = gameObject.transform.GetChild(1).transform;
        pickUpScript = null;
        ammo = initialAmmo;
    }

    void Update()
    {
        //Update the ammo bar
        float ammoPercentage = 1 * (ammo / initialAmmo);
        ammoCounter.localScale = new Vector3(ammoCounter.localScale.x, ammoCounter.localScale.y, ammoPercentage);

        //make sure that we don't have too many bullets in the gun
        ammo = Mathf.Clamp(ammo, 0, initialAmmo);

        //Get the hand that feeds us. Only when the item gets picked up
        if (transform.parent != null)
        {
            pickUpScript = transform.parent.gameObject.GetComponent<PickUpScript>();

            //Set the right key for toggling automatic and single fire mode
            if (pickUpScript.isRight)
            {
                automaticKey = KeyCode.RightShift;
            }

            //Toggle automatic and single fire mode
            if (Input.GetKeyDown(automaticKey))
            {
                if (automatic)
                {
                    automatic = false;
                }
                else
                {
                    automatic = true;
                }
            }

            //Check if we can fire the gun and then fire
            if(ammo > 0)
            {
                if (Time.time > nextFire)
                {
                    if (pickUpScript.useItem)
                    {
                        Use();
                    }
                }
            }
        }
        //If there is no hand to feed us we set this to null
        else
        {
            pickUpScript = null;
        }
    }


    public void Use()
    {
        Debug.Log("Shoot gun");

        //The fire rate timer
        nextFire = Time.time + fireRate;

        //Decreases ammo in the gun with every shot
        ammo -= 1;

        //Create the bullet
        GameObject bullet = Instantiate((GameObject)Resources.Load("Bullet"), origin.position, origin.rotation) as GameObject;

        //Make the bullet go fast
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.up * bulletSpeed, ForceMode.Impulse);

        //Set the bool in the hand to false if we are on single fire mode
        if (!automatic)
        {
            pickUpScript.useItem = false;
        }

        //Start deleting the bullet
        StartCoroutine(DeleteBullets(bullet));        
    }

    IEnumerator DeleteBullets(GameObject bullets)
    {
        //Wait a couple of seconds before deleting the bullet
        yield return new WaitForSeconds(3);

        //Actually delete te bullet
        Destroy(bullets);
    }
}
