using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableObject : MonoBehaviour
{
    public Rigidbody rb;

    public PickUpScript pickUpScript;

    public bool isPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
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
        else
        {
            pickUpScript = null;
            isPickedUp = false;
        }
    }

    public virtual void Initialize()
    {
        pickUpScript = null;
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Use() { }
}
