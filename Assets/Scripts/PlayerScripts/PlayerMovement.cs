using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //The character controller
    CharacterController controller;

    private void Start()
    {
        //initialize the character controller
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Calculate the inputs
        Vector3 move = transform.right * x + transform.forward * z;

        //Use the inputs in the character controller
        controller.Move(move);
    }
}
