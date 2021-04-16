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
    GameObject player;

    //The hand that presses the button 
    HeldItemsScript heldItemScript;


    public bool isPressed; 


    void Start()
    {
        //Make sure everything is reset
        player = null;
        heldItemScript = null;       
        lightbulb = transform.GetChild(0).gameObject;
       
    }

    void Update()
    {
        //Check if there is a player in front of the machine
        if(player != null)
        {
            //Check what hand presses the button and if it is already holding anything
            if(heldItemScript.itemLeft == null && Input.GetKeyDown(KeyCode.Q))
            {
                isPressed = true;
            }           
            if(heldItemScript.itemRight == null && Input.GetKeyDown(KeyCode.E))
            {

                isPressed = true;            
            }
        }

        //Deploy the item if the button is pressed
        if (isPressed)
        {
            deployAmmo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Get the player that's in front of the machine and their hands
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            heldItemScript = player.GetComponent<HeldItemsScript>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //reset everything if the player leaves
        if (other.CompareTag("Player"))
        {
            player = null;
            heldItemScript = null;
        }
    }

    void deployAmmo()
    {
        //Make sure that only 1 item is deployed
        isPressed = false;

        //Deploy the item
        GameObject newAmmoClip = Instantiate(vendingItem, lightbulb.transform.position, lightbulb.transform.rotation, null);
    }
}
