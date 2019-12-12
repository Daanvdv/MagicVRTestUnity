using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Lock : MonoBehaviour
{
    public Transform keyinLockLocation;
    public GameObject key;

    public ButtonDoor door;

    private Key keyScript;

    /// Awake is called when the script instance is being loaded.
    /*void Awake()
    {
        if (key.TryGetComponent<Key>(out keyScript))
            this.enabled = false;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    /// OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        GameObject interactable = other.gameObject.transform.parent.gameObject;
        if (interactable  == key)
        {
            door.OpenDoor();

            Rigidbody iRigidbody = interactable.GetComponent<Rigidbody>();
            iRigidbody.useGravity = false;
            iRigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

            /*iRigidbody.constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ
            | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;*/

            iRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

            iRigidbody.position.Set(keyinLockLocation.position.x, keyinLockLocation.position.y, keyinLockLocation.position.z);

            //interactable.transform.parent = keyinLockLocation;
            //interactable.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
