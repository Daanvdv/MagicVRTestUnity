using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CircularDriveable : MonoBehaviour
{
    public CircularDrive circularDrive;
    public Transform startPoint;
    public Transform endPoint;

    public bool onlyMoveOnMaxInput;
    public bool allowMoveBack = true;
    public float acceptanceAngle = 180.0f;

    public float moveSpeed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (!onlyMoveOnMaxInput)
        {
            this.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, circularDrive.outAngle / 180.0f);
        }
        else
        {
            if (circularDrive.outAngle >= acceptanceAngle)
            {
                //this.transform.position = Vector3.Lerp(this.transform.position, endPoint.position, Time.deltaTime);
                this.transform.position = Vector3.MoveTowards(this.transform.position, endPoint.position, moveSpeed * Time.deltaTime);
            }
            if (circularDrive.outAngle < acceptanceAngle && !(this.transform.position == startPoint.position) && allowMoveBack)
            {
                //this.transform.position = Vector3.Lerp(this.transform.position, startPoint.position, Time.deltaTime);
                this.transform.position = Vector3.MoveTowards(this.transform.position, startPoint.position, moveSpeed * Time.deltaTime);
            }
        }
    }
}
