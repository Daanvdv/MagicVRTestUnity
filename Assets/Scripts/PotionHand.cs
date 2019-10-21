using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PotionHand : MonoBehaviour
{
    //SteamVR Input
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grip;
    public SteamVR_Action_Boolean trigger;

    //Potion spawned
    private GameObject potionObject;

    //If object is spawned, stops duplication
    private bool objectSpawned;

    // Start is called before the first frame update
    void Start()
    {
        objectSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetGrip() && !objectSpawned)
        {
            CreatePotionObject();
        }
        if (GetTrigger() && objectSpawned)
        {
            DestroyPotionObject();
        }
    }


    void CreatePotionObject()
    {
        //Basic bullet creation
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        sphere.transform.parent = this.gameObject.transform;
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        //Setup rigidbody
        Rigidbody sphereRigidbody = sphere.AddComponent<Rigidbody>();
        sphereRigidbody.useGravity = false;

        //Setup collider
        SphereCollider spheresCollider = sphere.AddComponent<SphereCollider>();
        spheresCollider.radius = 0.1f;
        spheresCollider.isTrigger = true;

        sphere.tag = "Potion";

        //Set internal logic
        potionObject = sphere;
        objectSpawned = true;
    }
    public void DestroyPotionObject()
    {
        Object.Destroy(potionObject);
        objectSpawned = false;
    }

    bool GetGrip()
    {
        return grip.GetState(handType);
    }
    bool GetTrigger()
    {
        return trigger.GetState(handType);
    }
}
