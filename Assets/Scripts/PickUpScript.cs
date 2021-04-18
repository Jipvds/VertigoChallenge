using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{

    //Grab range
    [SerializeField] private float range = 100f;

    //Checks if there is a pickup in front of the hand
    bool objectInFront;

    //Check if there is an interactable object in front
    public bool interactableInFront;

    //This is the item that the player is holding
    public Transform heldItem;

    //This is the transform that the pickup will take
    [SerializeField] Transform slot;

    //Is this the right hand or not. Standard is the left hand
    public bool isRight;

    //Set the mousebutton that the hand uses. Standard is the left hand
    int mouseButton = 0;

    //Set the key that the hand grabs items with. Standard is the left hand
    KeyCode grabKey = KeyCode.Q;

    //This sees the item that the player is holding in this hand
    public HeldItemsScript heldItemScript;

    //The sparkle gets placed on the pickup that the player is pointing to
    GameObject selectionSparkle;

    //This bool is used by the pickups. The item is used when it is true.
    public bool useItem;

    //This bool is used by the interactable items. 
    public bool useInteract;


    void Start()
    {

        //Initialize all the things so you don't have to drag and drop everything everytime. 
        heldItemScript = transform.root.GetComponent<HeldItemsScript>();
        slot = gameObject.transform.GetChild(0);
        selectionSparkle = gameObject.transform.GetChild(1).gameObject;
        selectionSparkle.SetActive(false);

        //Set the correct inputs if this is the right hand
        if (isRight)
        {
            mouseButton = 1;
            grabKey = KeyCode.E;
        }

    }

    void Update()
    {

        //Raycast to the pickups or the interactable objects. They are on the same layer
        int layerMask = 1 << 8;

        RaycastHit hit;

        if (Physics.SphereCast(transform.parent.transform.position, 1, transform.TransformDirection(Vector3.forward), out hit, range, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            //Check if the raycast hits a pickup or an interactable item
            if (!hit.collider.CompareTag("Interactable"))
            {
                objectInFront = true;
            }
            else
            {
                interactableInFront = true;
                objectInFront = false;
            }

            //Place the sparkle on the right item and turn it on
            if (heldItem == null)
            {
                selectionSparkle.SetActive(true);
                selectionSparkle.transform.position = hit.transform.position;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            //Tell us that there is nothing in front of the hand and that we don't need the sparkle
            objectInFront = false;
            interactableInFront = false;
            selectionSparkle.SetActive(false);
        }

        //This is where we pick the item up and parent it to the hand
        if (Input.GetKeyDown(grabKey))
        {

            //Check if the hand is holding something. 
            if(heldItem == null)
            {
                //Pick the object up
                if (objectInFront)
                {
                    Debug.Log("Picked up " + hit.collider.gameObject.name);

                    //We don't need the sparkle if we are holding something
                    selectionSparkle.SetActive(false);

                    //Parent the item to the hand, place it in the right spot and make sure that it stays there. 
                    heldItem = hit.collider.transform;
                    heldItem.parent = gameObject.transform;
                    heldItem.transform.rotation = gameObject.transform.rotation;
                    heldItem.transform.position = slot.position;
                    heldItem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
                //Use the interactable
                if (interactableInFront)
                {
                    useInteract = true;
                }

            }
            //If the hand is holding something we drop it with the same key and make sure it returns to normal
            else
            {
                Debug.Log("Dropped " + heldItem);
                heldItem.GetComponent<Rigidbody>().isKinematic = false;
                heldItem.transform.parent = null;
                heldItem = null;
            }
        }

        //Feed the bool to the item. We only need to do this if the hand is holding something. Some items need to keep working until the button is released. Most items turn it of themselves.
        if (heldItem != null)
        {
            if (Input.GetMouseButtonDown(mouseButton))
            {
                useItem = true;
            }
            if (Input.GetMouseButtonUp(mouseButton))
            {
                useItem = false;
            }
        }
    }
}
