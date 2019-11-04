using UnityEngine;
using System.Collections;

public class Nest : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform);
		transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
	}
}
