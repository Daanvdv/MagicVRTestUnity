using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandScript : MonoBehaviour
{
    public SteamVR_Action_Boolean pickUp;
    public Hand hand;
    public InteractableObject objectColliding;

    void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

        if (hand == null)
            Debug.LogError("<b>[SteamVR Interactions]</b> Grip action not assigned");

        pickUp.AddOnChangeListener(HandPickupDrop, hand.handType);
    }

    void HandPickupDrop(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            objectColliding.PickUpObject(this.gameObject);
        }
        else if (!(objectColliding == null))
        {
            objectColliding.DropObject();
            objectColliding = null;
        }
    }
}
