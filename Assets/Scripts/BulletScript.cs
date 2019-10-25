using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BulletScript : MonoBehaviour
{
    //Audio

    //Bullet properties
    public float despawnTimer = 1.5f;

    //For sound testing
    private bool hitWall;
    
    //Internal timer
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        hitWall = false;

        //TODO: AUDIO SETUP

        timer = despawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0.0f)
        {
            //TODO: DESTRUCTION SOUND
            Destroy(this.gameObject);
        }
    }

    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    void OnCollisionEnter(Collision other)
    {
        if (!hitWall)
        {
            hitWall = true;
            //TODO: FIRE COLLISION SOUND
        }
    }
}
