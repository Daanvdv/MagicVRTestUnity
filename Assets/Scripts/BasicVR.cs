using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace BasicVR
{
    public class BasicVRInput
    {
        public static SteamVR_Input_Sources handType;
        public static SteamVR_Action_Boolean trigger;
        public static SteamVR_Action_Boolean menu;
        public static SteamVR_Action_Boolean grip;

        public static bool GetTrigger()
        {
            return trigger.GetState(handType);
        }

        //Checks if the grip is pressed
        public static bool GetGrip()
        {
            return grip.GetState(handType);
        }  

        //Checks if menu is pressed
        public static bool GetMenu()
        {
            return menu.GetState(handType);
        }
    }

    public class InputReciver
    {
        
    }
}    
