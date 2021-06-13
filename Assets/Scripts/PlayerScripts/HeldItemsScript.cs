using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItemsScript : MonoBehaviour
{
    //What is the player holding in the left and right hand
    Transform itemLeft;
    public Transform ItemLeft
    {
        get
        {
            return itemLeft;
        }
        set
        {
            itemLeft = value;
        }
    }

    Transform itemRight;

    public Transform ItemRight
    {
        get
        {
            return itemRight;
        }
        set
        {
            itemRight = value;
        }
    }

    //The transform of the head
    [SerializeField] Transform headSlot;

    public Transform HeadSlot
    {
        get
        {
            return headSlot;
        }
    }

    //What are the left and the right hand
    [SerializeField] GameObject leftHand;

    public GameObject LeftHand
    {
        get
        {
            return leftHand;
        }
    }

    [SerializeField] GameObject rightHand;

    public GameObject RightHand
    {
        get
        {
            return rightHand;
        }
    }



    private void Update()
    {
        //What are the hands holding and is it nothing
        itemLeft = leftHand.GetComponent<PickUpScript>().HeldItem;
        itemRight = rightHand.GetComponent<PickUpScript>().HeldItem;
    }
}
