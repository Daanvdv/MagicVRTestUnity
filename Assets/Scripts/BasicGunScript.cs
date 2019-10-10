using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Audio;

public class BasicGunScript : MonoBehaviour
{
    //VR Input
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean trigger;
    public SteamVR_Action_Boolean menu;
    public SteamVR_Action_Boolean grip;

    //Audio
    public AudioClip fireSound;
    public AudioClip emptySound;
    public AudioClip reloadSound;
    public AudioMixerGroup audioMixerGroup;
    public AudioClip bulletCollisionSound;
    public AudioClip bulletHitSound;
    public AudioMixerGroup bulletAudioMixerGroup;

    //Timers
    public float reloadTime;
    public float shotDelay;
    private float reloadTimer;
    private float shotTimer;

    //Ammo
    public int maxAmmo;
    private int currentAmmo;

    //Bullet Settings
    public float bulletDespawntimer;

    //Force
    public float bulletForceMultipleier;

    //Debug boolean
    public bool debugMode;

    //Interal boolean for checking wether the tigger has been activated
    //May be redudent
    private bool triggerActive;
    
    //Boolean for alternative firing mode
    private bool cubeFiringMode;

    // Start is called before the first frame update
    void Start()
    {
        triggerActive = false;

        currentAmmo = maxAmmo;

        //Initalise timers
        shotTimer = 0.0f;
        reloadTimer = 0.0f;
    }

    /// Awake is called when the script instance is being loaded.
    void Awake()
    {
        //Setup rigidbody
        this.gameObject.AddComponent<Rigidbody>();
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;

        //Setup sphere collider
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.GetComponent<SphereCollider>().radius = 0.08f;
        this.gameObject.GetComponent<SphereCollider>().isTrigger = true;

        //Setup audio source
        this.gameObject.AddComponent<AudioSource>();
        this.gameObject.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerGroup;
    }

    // Update is called once per frame
    void Update()
    {
        reloadTimer -= Time.deltaTime;
        shotTimer -= Time.deltaTime;
        if (GetGrip() || (Input.GetKeyDown("r")  && debugMode))
        {
            //Debug.Log("Grab " + handType);
            Reload();
        }

        if (GetTrigger() || (Input.GetMouseButtonDown(0) && debugMode))
        {
            //Debug.Log("Trigger" + handType);
            triggerActive = true;
        }
        else
            triggerActive = false;
        if (GetMenu())
        {
            Debug.Log("Menu" + handType);
            cubeFiringMode = !cubeFiringMode;
        }

        if (triggerActive && shotTimer < 0.0f)
        {
            FireProjectile();
            triggerActive = false;
            shotTimer = shotDelay;
        }

        if (Input.GetKeyDown("z"))
        {
            Debug.Log("Menu" + handType);
            this.transform.Rotate(0, 15, 0);
        }
        if (Input.GetKeyDown("x"))
        {
            Debug.Log("Menu" + handType);
            this.transform.Rotate(0, -15, 0);
        }

    }

    void FireProjectile()
    {
        if (!(currentAmmo == 0) && reloadTimer < 0.0f)
        {
            if (!cubeFiringMode)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = transform.position;
                sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                sphere.AddComponent<Rigidbody>();
                sphere.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * bulletForceMultipleier, ForceMode.Force);
                BulletScript bulletScript = sphere.AddComponent<BulletScript>();
                bulletScript.despawnTimer = bulletDespawntimer;
                bulletScript.audioMixerGroup = bulletAudioMixerGroup;
                bulletScript.colliisonSound = bulletCollisionSound;
                bulletScript.hitSound = bulletHitSound;
                GetComponent<AudioSource>().PlayOneShot(fireSound);
                currentAmmo--;
            }
            else
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = transform.position;
                cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                cube.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.0f, 0.5f, 1.0f));
                cube.AddComponent<Rigidbody>();
                cube.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * bulletForceMultipleier, ForceMode.Force);
                BulletScript bulletScript = cube.AddComponent<BulletScript>();
                bulletScript.despawnTimer = bulletDespawntimer;
                bulletScript.audioMixerGroup = bulletAudioMixerGroup;
                bulletScript.colliisonSound = bulletCollisionSound;
                bulletScript.hitSound = bulletHitSound;
                GetComponent<AudioSource>().PlayOneShot(fireSound);
                currentAmmo--;
            }
        }
        else if (reloadTimer < 0.0f)
        {
            GetComponent<AudioSource>().PlayOneShot(emptySound);
        }
    }

    void FireProjectileVolley()
    {
        for (int i = 0; i < 15; i++)
        {
            if (!(currentAmmo == 0) && reloadTimer < 0.0f)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = transform.position;
                sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                sphere.AddComponent<SphereCollider>();
                sphere.GetComponent<SphereCollider>().radius = 0.1f;
                sphere.AddComponent<Rigidbody>();
                sphere.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * bulletForceMultipleier, ForceMode.Force);
                sphere.AddComponent<BulletScript>();
            }
        }
        if (reloadTimer < 0.0f)
        {
            GetComponent<AudioSource>().PlayOneShot(emptySound);
        }
        currentAmmo--;
        GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    void Reload()
    {
        if (!(currentAmmo == maxAmmo) && reloadTimer < 0.0f)
        {
            currentAmmo = maxAmmo;
            GetComponent<AudioSource>().PlayOneShot(reloadSound);
            reloadTimer = reloadTime;

        }
    }


    /// OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Potion")
        {
            cubeFiringMode = true;
            other.transform.parent.GetComponent<PotionHand>().DestroyPotionObject();
        }
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
