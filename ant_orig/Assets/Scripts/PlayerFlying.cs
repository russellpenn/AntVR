using UnityEngine;
using System.Collections;
using System;

public class PlayerFlying : MonoBehaviour {

	private GameObject player;
	private float trajectoryHeight = 50;
	private Quaternion tempRot;

	public Transform initialPosition;
	public Transform respawnPosition;

	public Compass compass;
	public GameObject blackScreen;

	public GameObject nest;

	private bool setRespawn;

	// Use this for initialization
	void Start () {


		setRespawn = true;
		//player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;

	}

	IEnumerator displayDarkScreen(){
		blackScreen.SetActive (true);
		yield return new WaitForSeconds(3);
		blackScreen.SetActive(false);
	}



	public void setRespawnPosition(){
		int currentState = player.GetComponent<behaviour> ().getCurrentState ();

		//Transform initialFoodPos = GameObject.FindGameObjectWithTag("NoFoodZone").GetComponent<NoFoodZone>().getInitialPositionFood();

		Debug.Log ("generando nueva position");
		switch (currentState) {
		case 3: //Experiment 1 respawn in food location (known location)
             Transform temp2 = player.GetComponent<behaviour>().getFoodObject().transform;
             Vector3 tempValues2 = temp2.position;


             tempValues2.x *= UnityEngine.Random.Range(-2, 2);
             tempValues2.z *= UnityEngine.Random.Range(-2, 2);
             temp2.position = tempValues2;
             respawnPosition = temp2;

            break;
        case 6: // Experiment 2 respawn near food location (familiar location)
			Transform temp = player.GetComponent<behaviour> ().getFoodObject ().transform;
			Vector3 tempValues = temp.position;
			
			tempValues.x/=2;
			tempValues.z/=2;
			temp.position = tempValues;
			respawnPosition = temp;
			break;
		case 9: // Experiment 3 respawn oposite of food location (unknown location)
			//Transform temp2 = player.GetComponent<behaviour> ().getFoodObject ().transform;
			//Vector3 tempValues2 = temp2.position;
			
			//tempValues2.x*=-1;
			//tempValues2.z*=-1;
			//temp2.position = tempValues2;
			//respawnPosition = temp2;
			break;
		}
	}

	// Update is called once per frame
	void Update () {

		if (setRespawn) {
			player = GameObject.FindGameObjectWithTag ("Player");
			
			initialPosition = player.transform;
			Debug.Log ("Initial Position: "+initialPosition.position);
			
			setRespawnPosition ();
			

			Debug.Log ("Respawned Position: "+respawnPosition.position);

			//player.GetComponent<behaviour>().getFoodObject().transform.position = GameObject.FindGameObjectWithTag("NoFoodZone").GetComponent<NoFoodZone>().getInitialPositionFood().position;
			player.GetComponent<OVRPlayerController>().enabled=false;
			setRespawn=false;
		}





		float distance =  respawnPosition.position.x - transform.position.x;
        Debug.Log("Distance: " + distance);
        Debug.Log("tposx: " + transform.position.x);
        Debug.Log("rposx: " + respawnPosition.position.x);

        // If posicion del jugador y del respawn estan cerca desactivar este script y activar otro de WindMovement y Respawn
        if (Math.Abs(distance) > 5) {
			Vector3 currentPos = Vector3.Lerp (initialPosition.position, respawnPosition.position, 0.02f);
			currentPos.y += trajectoryHeight * Mathf.Sin (Mathf.Clamp01 (0.02f) * Mathf.PI);
			if(trajectoryHeight<10){
				trajectoryHeight-=1f;
			}

            Debug.Log("Current positiom: " + currentPos);
            transform.position = currentPos;

			//transform.Rotate(new Vector3(30*Time.deltaTime,30*Time.deltaTime,30*Time.deltaTime));

			
		} else {
			//StartCoroutine( displayDarkScreen());
			setRespawn=true;
			player.GetComponent<PlayerFlying>().enabled=false;
			transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
			//transform.Rotate(new Vector3(0,0,-10));
			player.GetComponent<OVRPlayerController>().enabled=true;
			//player.GetComponent<behaviour> ().increaseState ();
			// New compass position
			compass.setTarget(respawnPosition);

			// Dark Screen

			GameObject.FindGameObjectWithTag("Tornado").transform.position = nest.transform.position;
			GameObject.FindGameObjectWithTag("Tornado").SetActive(false);

            Debug.Log("Distance <5 Position: ");

            //GameObject.FindGameObjectWithTag("SandStorm").SetActive(false);
            player.GetComponent<KidnappedRobot>().enabled=true;

		}

	}
}
