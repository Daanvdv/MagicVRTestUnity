using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    //Exit spcification
    public GameObject portalExit;

    //Check for if the portal will cause errors
    private bool issuesDetected;

    // Start is called before the first frame update
    void Start()
    {
        if (portalExit == null)
        {
            this.enabled = false;
            issuesDetected = true;
            this.gameObject.GetComponent<MeshCollider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        if (!issuesDetected)
        {
            ///May cause issues when other is scaled diffrenlty
            Vector3 exitOffsetPosition = this.gameObject.transform.position - other.transform.position;

            other.transform.position = portalExit.transform.position + exitOffsetPosition;
            other.transform.rotation = portalExit.transform.rotation;
        }
    }
}
