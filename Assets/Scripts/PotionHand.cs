using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PotionHand : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grip;
    public SteamVR_Action_Boolean trigger;
    private GameObject potionObject;

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
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        sphere.transform.parent = this.gameObject.transform;
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sphere.AddComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().useGravity = false;
        sphere.AddComponent<SphereCollider>();
        sphere.GetComponent<SphereCollider>().radius = 0.1f;
        sphere.GetComponent<SphereCollider>().isTrigger = true;
        sphere.tag = "Potion";
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
