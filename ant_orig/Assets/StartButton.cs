using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	public GameObject sphere;
	private Vector3 scale;
	private bool start;
	private float time;
	public GameObject text;
	public GameObject blackScreen;
	public GameObject reticle;
	// Use this for initialization
	void Start () {
		scale = sphere.transform.localScale;
		start = false;
		time = 1;
	}

	void OnTriggerEnter(Collider other) { 
		if(other.gameObject.CompareTag("boxView"))
		{
			Debug.Log("Entro a Start");
			text.SetActive(false);
		}
	}

	void OnTriggerStay(Collider other){

		if (scale.x < 1) {
			if (other.gameObject.CompareTag ("boxView")) {
				Debug.Log ("Stay a Start");
				scale.x += 0.01f;
				sphere.transform.localScale = scale;
			}	
		} else {
			Debug.Log("Starting Game");
			start = true;
			blackScreen.SetActive(true);
			reticle.SetActive(false);
		}
	}

	
	void OnTriggerExit(Collider other){
		if(other.gameObject.CompareTag("boxView"))
		{
			Debug.Log("Salio a Start");
			scale.x=0;
			sphere.transform.localScale = scale;
			start=false;
			text.SetActive(true);

		}
	}

	
	// Update is called once per frame
	void Update () {
		if (start) {
			if(time<0){
				SceneManager.LoadScene("Antworld");
			}else{
				time-=Time.deltaTime;
			}
		}	
	}
}
