using UnityEngine;
using System.Collections;

public class MovementTornado : MonoBehaviour {

	private GameObject player;
	private float speed;


	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		speed = 10f;
	}
	
	// Update is called once per frame
	void Update () {

		// Hacer que el tornado se mueve hacia el jugador
		float distance = Vector3.Distance (transform.position,player.transform.position);

		// If posicion del jugador y del tornado son estan cerca desactivar este script y activar otro de WindMovement y Respawn
		if (distance > 15) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);

		} else {


			player.GetComponent<PlayerFlying>().enabled = true;
			//player.GetComponent<PlayerFlying>().setRespawnPosition();
			player.GetComponent<Tornado>().enabled = false;

		}


	}
}
