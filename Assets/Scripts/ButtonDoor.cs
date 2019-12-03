using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public bool allowMoveBack = true;
    public float acceptanceValue = 1.0f;

    public float moveSpeed = 1.5f;

    private bool openDoor;

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, endPoint.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, startPoint.position, moveSpeed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        openDoor = true;
    }

    public void CloseDoor()
    {
        openDoor = false;
    }
}
