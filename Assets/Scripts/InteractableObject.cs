using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    HandScript hand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<HandScript>(out hand);

        hand.objectColliding = this;
    }

    public void PickUpObject(GameObject other)
    {
        this.transform.parent = other.transform;
    }

    public void DropObject()
    {
        this.transform.parent = null;
    }
}
