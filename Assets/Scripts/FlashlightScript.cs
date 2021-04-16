using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    //Checks if the light is on or not
    [SerializeField] bool lightOn;

    //This is the light of the flashlight
    GameObject lightSource;

    //This talks to the hand
    PickUpScript pickUpScript;


    void Start()
    {
        //Initialize the things
        pickUpScript = null;
        lightSource = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        //Get the hand that feeds us. Only when the item gets picked up
        if (transform.parent != null)
        {
            pickUpScript = transform.parent.gameObject.GetComponent<PickUpScript>();

            //See if the hand wants to use the item
            if (pickUpScript.useItem)
            {
                Use();
            }
        }

        //If the light is active we turn it off, if it's inactive we turn it on
        if (lightOn)
        {
            lightSource.SetActive(true);
        }
        else
        {
            lightSource.SetActive(false);
        }
    }

    void Use()
    {
        //Set the bool in the hand to false
        pickUpScript.useItem = false;

        //Turn the light bool on or off
        if (lightOn)
        {
            lightOn = false;
        }
        else
        {
            lightOn = true;
        }
    }
}
