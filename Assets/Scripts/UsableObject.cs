using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableObject : MonoBehaviour
{
    //Rigidbody of the pickup
    public Rigidbody rb;

    //The pick up script of the hand that is holding the pickup
    public PickUpScript pickUpScript;

    //Checks if the item is picked up 
    public bool isPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the things that all pickups share
        Initialize();
    }

    //Check if the pickup is parented to a hand and if the pickup needs to do something
    public virtual void Update()
    {
     if(transform.parent != null)
        {
            if (transform.parent.CompareTag("Hand"))
            {
                pickUpScript = transform.parent.gameObject.GetComponent<PickUpScript>();
                isPickedUp = true;

                if (pickUpScript.useItem)
                {
                    Use();
                }
            }
        }
     // Reset the pickup if it is dropped
        else
        {
            pickUpScript = null;
            isPickedUp = false;
        }
    }

    //Initialize the things that all pickups share
    public virtual void Initialize()
    {
        pickUpScript = null;
        rb = GetComponent<Rigidbody>();
    }

    //Empty Use() that the pickups can override 
    public virtual void Use() { }
}
