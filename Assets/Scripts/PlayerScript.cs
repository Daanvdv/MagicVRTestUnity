using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
	GameObject player;
	public float moveSpeed;

	

	// Awake is called when the script instance is being loaded.
	void Awake()
    {
		player = this.gameObject;
	}

    // Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
	}
}