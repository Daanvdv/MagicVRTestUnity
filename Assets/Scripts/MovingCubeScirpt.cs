using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCubeScirpt : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public List<Vector3> positions;

    public int posNumber = 1;
    public float internalTimer;
    public int previousPos = 0;
    public bool goToStart;

    /// Awake is called when the script instance is being loaded.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        internalTimer += Time.deltaTime;

        if (this.transform.position == positions[positions.Count - 1])
        {
            goToStart = true;
        }

        if (goToStart)
        {
            if (this.transform.position == positions[0])
            {
                goToStart = false;
                posNumber = 1;
                previousPos = 0;
                internalTimer = 0.0f;
            }
            else
            {
                this.transform.position = Vector3.Lerp(positions[positions.Count - 1], positions[0], Mathf.Clamp(internalTimer * moveSpeed, 0.0f, 1.0f));
            }
        }
        else
        {
            this.transform.position = Vector3.Lerp(positions[previousPos], positions[posNumber], Mathf.Clamp(internalTimer * moveSpeed, 0.0f, 1.0f));

            if (this.transform.position == positions[posNumber])
            {
                posNumber++;
                previousPos++;
                internalTimer = 0.0f;
            }
        }
    }

    /// Callback to draw gizmos that are pickable and always drawn.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;


    }
}
