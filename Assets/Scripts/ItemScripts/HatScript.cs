using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatScript : UsableObject
{
    public override void Update()
    {
        base.Update();

        //Check if the item is parented to the head and not the hand
        if (isPickedUp)
        {
            if (transform.parent.CompareTag("Head"))
            {
                //Drop the hat and reset it
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
        if (heldItemScript.HeadSlot.childCount == 0)
        {
            //Reset the data in the hand
            pickUpScript.UseItem = false;
            pickUpScript.HeldItem = null;

            //Put the hat in the right place and parent it to the head
            transform.parent = heldItemScript.HeadSlot;
            transform.position = heldItemScript.HeadSlot.transform.position;
        }
    }
}
