using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItemsScript : MonoBehaviour
{
    //What is the player holding in the left and righ hand
    public Transform itemLeft;
    public Transform itemRight;

    //The transform of the head
    public Transform headSlot;

    //What are the left and the right hand
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;



    private void Update()
    {
        //What are the hands holding and is it nothing
        itemLeft = leftHand.GetComponent<PickUpScript>().heldItem;
        itemRight = rightHand.GetComponent<PickUpScript>().heldItem;
    }
}
