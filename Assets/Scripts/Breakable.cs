using UnityEngine;
using Valve.VR.InteractionSystem;

public class Breakable : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public float minMagnitudeToBreak = 1.0f;
    
    //Audio
    public AK.Wwise.Event breakEvent;

    private Interactable interactable;

    private void Start()
    {
        interactable = this.GetComponent<Interactable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (interactable != null && interactable.attachedToHand != null) //don't explode in hand
            return;

        if (collision.impulse.magnitude > minMagnitudeToBreak)
        {
            breakEvent.Post(this.gameObject);
            this.gameObject.GetComponent<MeshRenderer>();
            foreach (GameObject piece in brokenPieces)
            {
                Instantiate(piece, this.transform);
                piece.gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}