using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : UsableObject
{
    //The force that the rock is thrown with
    [SerializeField] float force;


    public override void Use()
    {
        Debug.Log("Throw rock");

        //Set the bool in the hand to false
        pickUpScript.useItem = false;

        //Unparent the rock and add the force. Make sure that all the things in the hand are reset
        rb.isKinematic = false;
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        pickUpScript.heldItem = null;
        pickUpScript.heldItemScript = null;
        transform.parent = null;
    }
}
