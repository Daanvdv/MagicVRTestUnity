using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWarp : MonoBehaviour
{
    public GameObject warpPoint;
    public GameObject cam;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("CollisionDetected");
        if (other.gameObject == cam)
        {
            Debug.Log("CameraCollision");

            //Debug.Log("Other: " + other.gameObject + " pos: " + other.gameObject.transform.position + " warppoint pos: " + warpPoint.gameObject.transform.position);
            other.gameObject.transform.position = warpPoint.gameObject.transform.position;


            //Debug.Log("Other: " + other.gameObject + "altered pos: " + other.gameObject.transform.position + " warppoint pos: " + warpPoint.gameObject.transform.position);
            

            //player.transform.position = warpPoint.transform.position;
        }
    }
}
