using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMachineScript : MonoBehaviour
{
    //This is where the item drops from.
    GameObject lightbulb;

    //The item that drops from the machine
    [SerializeField] GameObject vendingItem;

    //The player that is standing in front of the machine
    public GameObject player;

    //Left hand that presses the button 
    public  PickUpScript heldItemScriptLeft;

    //Right hand that presses the button
    PickUpScript heldItemScriptRight;

    void Start()
    {
        //Make sure everything is reset
        player = null;
        heldItemScriptLeft = null;
        heldItemScriptRight = null;
        lightbulb = transform.GetChild(0).gameObject;
       
    }

    void Update()
    {
        //Check if there is a player in front of the machine
        if(player != null)
        {
            //Check if the hands want to use the machine and deploy the item
            if (heldItemScriptLeft.useInteract || heldItemScriptRight.useInteract)
            {
                deployAmmo();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Get the player that's in front of the machine and their hands
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            heldItemScriptLeft = player.GetComponent<HeldItemsScript>().leftHand.GetComponent<PickUpScript>();
            heldItemScriptRight = player.GetComponent<HeldItemsScript>().rightHand.GetComponent<PickUpScript>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //reset everything if the player leaves
        if (other.CompareTag("Player"))
        {
            player = null;
            heldItemScriptLeft = null;
            heldItemScriptRight = null;
        }
    }

    void deployAmmo()
    {
        //Make sure that only 1 item is deployed
        heldItemScriptLeft.useInteract = false;
        heldItemScriptRight.useInteract = false;

        //Deploy the item
        GameObject newAmmoClip = Instantiate(vendingItem, lightbulb.transform.position, lightbulb.transform.rotation, null);
    }
}
