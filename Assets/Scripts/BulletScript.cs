﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BulletScript : MonoBehaviour
{
    //Audio
    public AudioClip hitSound;
    public AudioClip colliisonSound;
    public AudioMixerGroup audioMixerGroup;

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

        //Basic audio source setup
        AudioSource audioSourceComponent = this.gameObject.AddComponent<AudioSource>();
        audioSourceComponent.outputAudioMixerGroup = audioMixerGroup;
        audioSourceComponent.clip = hitSound;
        audioSourceComponent.playOnAwake = false;

        //Turning off attenuation
        audioSourceComponent.rolloffMode = AudioRolloffMode.Custom;
        audioSourceComponent.maxDistance = float.MaxValue;

        //Setting reverb off and spatial blend to full 3D
        audioSourceComponent.reverbZoneMix = 0.0f;
        audioSourceComponent.spatialBlend = 1.0f;

        timer = despawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

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
        if (!hitWall)
        {
            hitWall = true;
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }
    }
}
