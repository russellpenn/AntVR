using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFood : MonoBehaviour {

	public GameObject foodPrefab;
	public GameObject player;
	public Transform cookieTransform;
	private Transform InitialPositionFood;
	private bool isCreated; 

	private int maxCupcakes = 1;
	private int cupcakeCounter;

	// Use this for initialization
	void Start () {
		cupcakeCounter = 0;
		isCreated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InstantiateObject ()
	{
		//Vector3 position = (player.transform.forward * 15);
		//position.y = -20;
		//Vector3 cookiePosition =(-162.02f, -24.84f, 149.251f);


		Instantiate (foodPrefab, cookieTransform.position, cookieTransform.rotation);
		isCreated = true;
		
	}


	void OnTriggerEnter (Collider other) {
		if (!isCreated) {
		InstantiateObject();
		}
		Debug.Log("I've Triggered");
	}
}
