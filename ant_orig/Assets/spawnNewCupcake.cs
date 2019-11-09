using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNewCupcake : MonoBehaviour {

	public GameObject foodPrefab;
	public GameObject player;
	public Transform newObjectTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InstantiateObject ()
	{
		//Vector3 position = (player.transform.forward * 15);
		//position.y = -20;
		//Vector3 cookiePosition =(-162.02f, -24.84f, 149.251f);

		var newCupcake = Instantiate (foodPrefab, newObjectTransform.position, newObjectTransform.rotation);

	}

}
