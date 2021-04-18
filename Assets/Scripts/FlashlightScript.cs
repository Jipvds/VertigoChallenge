using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : UsableObject
{
    //This is the light of the flashlight
    GameObject lightSource;


    public override void Initialize()
    {
        base.Initialize();
        lightSource = transform.GetChild(0).gameObject;
    }

    public override void Use()
    {
        //Set the bool in the hand to false
        pickUpScript.useItem = false;

        //Turn the light on or off
        if (lightSource.activeInHierarchy)
        {
            lightSource.SetActive(false);
        }
        else
        {
            lightSource.SetActive(true);

        }
    }
}
