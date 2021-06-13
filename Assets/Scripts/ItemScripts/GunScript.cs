using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : UsableObject
{
    //Set the speed of the bullet
    [SerializeField] float bulletSpeed = 10;

    //Set the firerate of the bullet
    [SerializeField] float fireRate;

    //Is the gun in single fire or automatic mode
    [SerializeField] bool automatic;

    //How much ammo can the gun hold
    [SerializeField] float initialAmmo = 25;

    public float InitialAmmo
    {
        get
        {
            return initialAmmo;
        }
    }

    //How much ammo is in the gun right now
    private float ammo;
    public float Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            ammo = value;
        }
    }

    //This times if the gun can fire again
    float nextFire;

    //This is where the bullets come from
    Transform origin;

    //This shows us how many bullets we have left in the gun
    Transform ammoCounter;

    //Set the key that toggles the automatic or single fire mode
    KeyCode automaticKey = KeyCode.LeftShift;

    //The material of the ammo bar
    Renderer ammoMat;

    //Set material of the ammo bar in single fire mode
    [SerializeField] Material ammoMatSingle;

    //Set material of the ammo bar in automatic fire mode
    [SerializeField] Material ammoMatAuto;

    //initialize the pickup specific things
    public override void Initialize()
    {
        base.Initialize();
        origin = gameObject.transform.GetChild(0).transform;
        ammoCounter = gameObject.transform.GetChild(1).transform;
        ammo = initialAmmo;
        ammoMat = ammoCounter.GetChild(0).GetComponent<MeshRenderer>();
    }

    public override void Update()
    {
        base.Update();

        //Update the ammo bar
        float ammoPercentage = 1 * (ammo / initialAmmo);
        ammoCounter.localScale = new Vector3(ammoCounter.localScale.x, ammoCounter.localScale.y, ammoPercentage);

        //make sure that we don't have too many bullets in the gun
        ammo = Mathf.Clamp(ammo, 0, initialAmmo);

        //Set the right key for changing automatic and single fire mode
        if (isPickedUp)
        {
            if (pickUpScript.IsRight)
            {
                automaticKey = KeyCode.RightShift;
            }
            else
            {
                automaticKey = KeyCode.LeftShift;
            }

            //Set the gun to automatic or single 
            if (Input.GetKeyDown(automaticKey))
            {

                if (automatic)
                {
                    automatic = false;
                    ammoMat.material = ammoMatSingle;
                }
                else
                {
                    automatic = true;
                    ammoMat.material = ammoMatAuto;
                }
            }
        }
    }


    public override void Use()
    {
        if (ammo > 0)
        {
            if (Time.time > nextFire)
            {
                if (pickUpScript.UseItem)
                {

                    Debug.Log("Shoot gun");

                    //The fire rate timer
                    nextFire = Time.time + fireRate;

                    //Decreases ammo in the gun with every shot
                    ammo -= 1;

                    //Create the bullet
                    GameObject bullet = Instantiate((GameObject)Resources.Load("Bullet"), origin.position, origin.rotation) as GameObject;

                    //Give the bullet the right material
                    bullet.GetComponent<MeshRenderer>().material = ammoMat.material;

                    //Make the bullet go fast
                    bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.up * bulletSpeed, ForceMode.Impulse);

                    //Set the bool in the hand to false if we are on single fire mode
                    if (!automatic)
                    {
                        pickUpScript.UseItem = false;
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
        }
    }
}
        
