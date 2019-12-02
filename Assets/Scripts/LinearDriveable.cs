using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LinearDriveable : MonoBehaviour
{
    public LinearMapping linearMapping;
    public Transform startPoint;
    public Transform endPoint;

    public bool onlyMoveOnMaxInput;
    public bool allowMoveBack = true;
    public float acceptanceValue = 1.0f;

    public float moveSpeed = 1.5f;

    private float internalTimer;

    // Update is called once per frame
    void Update()
    {
        if (!onlyMoveOnMaxInput)
        {
            this.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, linearMapping.value);
        }
        else
        {
            if (linearMapping.value >= acceptanceValue)
            {
                //this.transform.position = Vector3.Lerp(this.transform.position, endPoint.position, Time.deltaTime);
                this.transform.position = Vector3.MoveTowards(this.transform.position, endPoint.position, moveSpeed * Time.deltaTime);
            }
            if (linearMapping.value < 1.0f && !(this.transform.position == startPoint.position) && allowMoveBack)
            {
                //this.transform.position = Vector3.Lerp(this.transform.position, startPoint.position, Time.deltaTime);
                this.transform.position = Vector3.MoveTowards(this.transform.position, startPoint.position, moveSpeed * Time.deltaTime);
            }
        }
    }
}
