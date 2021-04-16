using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    //The force that the rock is thrown with
    [SerializeField] float force;

    //The rigidbody of the rock
    Rigidbody rb;

    //This talks to the hand
    PickUpScript pickUpScript;


    void Start()
    {
        //Initialize the things
        pickUpScript = null;
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //Get the hand that feeds us. Only when the item gets picked up
        if(transform.parent != null)
        {
            pickUpScript = transform.parent.gameObject.GetComponent<PickUpScript>();

            //See if the hand wants to use the item
            if (pickUpScript.useItem)
            {
                Use();
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
