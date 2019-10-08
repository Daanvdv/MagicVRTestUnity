using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float timer;
    private AudioClip hitSound;

    public int samplerate = 44100;
    public float frequency = 440;

    public float despawnTimer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<AudioSource>();
        hitSound = AudioClip.Create("BallonPop02", samplerate * 2, 1, samplerate, false);
        this.gameObject.GetComponent<AudioSource>().clip = hitSound;


        timer = despawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        HitSoundTest();

        if (timer < 0.0f)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(hitSound, 1.0f);
            Destroy(this.gameObject);
        }
    }

    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    void OnCollisionEnter(Collision other)
    {
        HitSoundTest();
    }

    void HitSoundTest()
    {
        GetComponent<AudioSource>().PlayOneShot(hitSound);
    }
}
