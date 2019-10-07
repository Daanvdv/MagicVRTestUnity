using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Spells : MonoBehaviour
{
    private List<Vector2> gesturePoints;
    private List<Vector2> validGesturePoints;
    public float acceptableRange;
	private SteamVR_TrackedObject trackedObject;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean trigger;
    public SteamVR_Action_Boolean menu;
    public SteamVR_Action_Boolean grip;

    public GameObject spawnParent;
    private List<GameObject> spawnedObjects;

    private bool captureGesture;
    private float timer;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        spawnedObjects = new List<GameObject>();
		trackedObject = GetComponent<SteamVR_TrackedObject>();
        timer = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(GetGrip())
        {
            Debug.Log("Grab " + handType);
            DestroySpheres();
        }
        if(GetTrigger())
        {
            Debug.Log("Trigger" + handType);
            //SpawnSphere();
            captureGesture = true;
        }
        else if (!GetTrigger() && captureGesture)
        {
            captureGesture = false;
            if (CheckGesture())
            {
                Debug.Log("Spell cast!");
            }
        }
        if(GetMenu())
        {
            Debug.Log("Menu" + handType);
        }
        
        if (captureGesture && timer >= 0.2f)
        {
            Debug.Log("Point Captured at " + Time.fixedTime + "s after " + timer + "s location: " + this.gameObject.transform.position);
            gesturePoints.Add((Vector2)this.gameObject.transform.localPosition);
            timer = 0.0f;

            if (gesturePoints.Count == 8)
                if (CheckGesture())
                {
                    Debug.Log("Spell cast!");
                    for (int i = 0; i < 10; i++)
                        SpawnSphere();
                }
        }
    }

    //Checks wether trigger has been pulled
    bool GetTrigger()
    {
        return trigger.GetState(handType);
    }

    //Checks if the grip is pressed
    bool GetGrip()
    {
        return grip.GetState(handType);
    }  

    //Checks if menu is pressed
    bool GetMenu()
    {
        return menu.GetState(handType);
    }

    //Check wether a point is within acceptable range
    bool CheckPoint(Vector2 point, int pointIndex)
    {
        Vector2 relativePoint = point/gesturePoints[0];
        if (Vector2.Distance(relativePoint, validGesturePoints[pointIndex]) <= acceptableRange)
            return true;
        else
            return false;
    }

    //Checks captured gesture and sees if it matches
	bool CheckGesture()
	{
		for (int i = 0; i < validGesturePoints.Count; i++)
        {
            CheckPoint(gesturePoints[i], i);
        }
		return true;
	}

    //Destorys spawned spheres
    void DestroySpheres()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            Destroy(spawnedObjects[i]);
            spawnedObjects.RemoveAt(i);
        }
    }

    //Spawns a sphere
    void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.SetParent(spawnParent.transform);
        sphere.transform.position = transform.position;
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sphere.AddComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().AddForce(0.0f, 100.0f, 0.0f, ForceMode.Force);
        spawnedObjects.Add(sphere);
    }
}
