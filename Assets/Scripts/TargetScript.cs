using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public GameObject moveObject;
    bool moving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            moveObject.transform.position = new Vector3 (moveObject.transform.position.x, Mathf.Lerp(moveObject.transform.position.y, -1.8f, Time.deltaTime * 2), moveObject.transform.position.z);
        }
    }

    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Target hit " + this);
        moving = true;
    }
}
