using UnityEngine;
using System.Collections;

public class boxViewTutorial : MonoBehaviour {

	public GameObject text;
	public GameObject player;
	public GameObject loadingBar;

	private bool loading;

	public GameObject textFood;
	private bool loadingFood;
	public GameObject loadingBarFood;
	public GameObject triggerFood;


	// Use this for initialization
	void Start () {
		loading = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (loading) {
			Vector3 temp = loadingBar.transform.localScale;
			temp.x+=0.05f;
			if(temp.x>2){
				loading = false;
				player.GetComponent<Tutorial>().updateState(4);
				loadingBar.SetActive(false);

			}else{
				loadingBar.transform.localScale = temp;
			}
		}
		if (loadingFood) {
			Vector3 temp = loadingBarFood.transform.localScale;
			temp.x+=0.05f;
			if(temp.x>2){
				loadingFood = false;
				player.GetComponent<Tutorial>().updateState(6);
				loadingBarFood.SetActive(false);
				triggerFood.SetActive(false);
				
			}else{
				loadingBarFood.transform.localScale = temp;
			}
		}

	}

	void OnTriggerEnter(Collider other) { 
		if(other.gameObject.CompareTag("boxView"))
		{
			Debug.Log("Seeing Nest");
			text.SetActive(false);
			loading = true;
			loadingBar.SetActive(true);
		}
		if(other.gameObject.CompareTag("Food"))
		{
			Debug.Log("Seeing Food");
			textFood.SetActive(false);
			loadingFood = true;
			loadingBarFood.SetActive(true);
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
		if(other.gameObject.CompareTag("Food"))
		{
			Debug.Log("Not Looking at Food");
			textFood.SetActive(true);
			loadingFood=false;
			Vector3 temp = loadingBarFood.transform.localScale;
			temp.x=0.1f;
			loadingBarFood.transform.localScale = temp;
			loadingBarFood.SetActive(false);
		}
	}
}
