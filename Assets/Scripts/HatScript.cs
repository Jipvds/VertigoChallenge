using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatScript : MonoBehaviour
{
    //The location that the hat goes to when equiped
    public Transform headSlot;

    //The rigidbody of the rock
    Rigidbody rb;

    //This talkes to the hand
    PickUpScript pickUpScript;


    // Start is called before the first frame update
    void Start()
    {
        //Initialize the things
        pickUpScript = null;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.parent != null)
        {
            //Check if the parent of the hat is not the head already
            if (!transform.parent.CompareTag("Head"))
            {
                //Get the hand that feeds us. Only when the item gets picked up
                pickUpScript = transform.parent.gameObject.GetComponent<PickUpScript>();

                //See if the hand wants to use the item
                if (pickUpScript.useItem)
                {
                    Use();
                }
            }
            //If we are already wearing a hat we can unequip it and return it to normal
            else
            {
                if (Input.GetMouseButtonDown(2))
                {
                    transform.parent = null;
                    rb.isKinematic = false;
                }
            }
        }
        //If there is no hand to feed us we set this to null
        else
        {
            pickUpScript = null;
        }
    }

    void Use()
    {
        //We need to know where the head is. The HeldItemsScript knows this
        HeldItemsScript heldItemScript;
        heldItemScript = transform.parent.root.GetComponent<HeldItemsScript>();

        //Check if we are not already wearing a hat
        if (heldItemScript.headSlot.childCount == 0)
        {
            //Reset the data in the hand
            pickUpScript.useItem = false;
            pickUpScript.heldItem = null;

            //Put the hat in the right place and parent it to the head
            transform.parent = heldItemScript.headSlot;
            transform.position = heldItemScript.headSlot.transform.position;
        }

    }
}
