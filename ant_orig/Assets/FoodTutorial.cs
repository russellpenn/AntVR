using UnityEngine;
using System.Collections;

public class FoodTutorial : MonoBehaviour {

	public GameObject text;
	public GameObject loadingBar;
	public bool loading;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) { 
		if(other.gameObject.CompareTag("boxView"))
		{
			Debug.Log("Seeing Nest");
			text.SetActive(false);
			loading = true;
			loadingBar.SetActive(true);
		}
	}

	
	void OnTriggerExit(Collider other){
		if(other.gameObject.CompareTag("boxView"))
		{
			Debug.Log("Not Looking at Nest");
			text.SetActive(true);
			loading=false;
			Vector3 temp = loadingBar.transform.localScale;
			temp.x=0.1f;
			loadingBar.transform.localScale = temp;
			loadingBar.SetActive(false);
		}
	}
}
