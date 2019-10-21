using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GolemScript : MonoBehaviour
{
    //SteamVR input
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean trigger;
    public SteamVR_Action_Boolean menu;
    public SteamVR_Action_Boolean grip;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Checks if trigger is pressed
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
}
