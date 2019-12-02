using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySoundScript : MonoBehaviour
{
    public AK.Wwise.Event sound;
    public float minRange;
    public float maxRange;

    private float timer = 0.0f;
    private float timeToPlaySound;
    private bool timerDone = false;

    // Start is called before the first frame update
    void Start()
    {
        timeToPlaySound = Random.Range(minRange, maxRange);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timeToPlaySound >= timer && !timerDone)
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        sound.Post(this.gameObject);
        timerDone = true;
    }
}
