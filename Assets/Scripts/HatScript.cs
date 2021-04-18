using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatScript : UsableObject
{
    //The location that the hat goes to when equiped
    public Transform headSlot;


    public override void Update()
    {
        base.Update();

        if (isPickedUp)
        {
            if (transform.parent.CompareTag("Head"))
            {
                if (Input.GetMouseButtonDown(2))
                {
                    transform.parent = null;
                    rb.isKinematic = false;
                }
            }
        }
    }

    public override void Use()
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
