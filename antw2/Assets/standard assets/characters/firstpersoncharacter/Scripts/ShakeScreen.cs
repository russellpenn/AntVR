using UnityEngine;
using System.Collections;

public class ShakeScreen : MonoBehaviour {

	private float rotate;
	private float rotationTime;
	public GameObject tornado;


	// Use this for initialization
	void Start () {
		rotate = 20;
		rotationTime = 2f;
		//player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
		//GameObject.FindGameObjectWithTag ("Player").GetComponent<Assets.>().
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, rotate*Time.deltaTime);
		rotate *= -1;

		if (rotationTime < 0) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			player.GetComponent<ShakeScreen> ().enabled = false;

			tornado.SetActive(true);
			player.GetComponent<Tornado>().enabled = true;
			rotationTime = 2f;
		} else {
			rotationTime -= Time.deltaTime;

		}

	
	}
}
