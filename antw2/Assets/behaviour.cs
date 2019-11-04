using UnityEngine;
using System.Collections;

public class behaviour : MonoBehaviour {

	public GameObject text;
	private float displayTime;
	private bool showText;
	private bool showGui;
	public Material newMaterial1;
	public Material newMaterial2;
	public Material newMaterial3;
	public Material newMaterial4;
	public Material newMaterial5;
	public Material newMaterial6;
	public Material newMaterial7;
	private Material[] materials;
	private bool hasFood;
	private GameObject food;
	public Material RedArrow;
	public Material GreenArrow;
	public GameObject nest;
	public GameObject compass;
	private int foodCount;
	private bool tornadoZone;
	public GameObject dust;

	private int currentState; 


	// 0 SearchFreelyForFood
	// 1 Food found and bring it back home
	// 2 Get another one
	// 3 Return home before Kidnapped Robot
	// 4 After Kidnapped Robot get back home exp 1
	// 5 Get more food using vector
	// 6 Return home using vector
	// 7 After Kidnapped Robot get back home exp 2
	// 8 Get more food using vector
	// 9 Return home using vector
	// 10 After Kidnapped Robot get back home exp 3


	// Use this for initialization
	void Start () {
		displayTime = 5;
		showText = false;
		showGui = true;
		materials = new Material[7]{newMaterial1,newMaterial2,newMaterial3,newMaterial4,newMaterial5,newMaterial6,newMaterial7};
		hasFood = false;
		foodCount = 0;
		tornadoZone = false;
		currentState = 0;
		Debug.Log ("Current State: "+currentState);
		//gameObject.GetComponent<WriteInFile>().enabled=true;
	}

	// Update is called once per frame
	void Update () {

		if (showGui) {
			if (showText) {
				StartCoroutine( displayTutorial());
//				displayTutorial();
				showGui = false;
			} else {
				if (displayTime < 0) {
					showText = true;
					displayTime = 5;
				}
				else {
					displayTime -= Time.deltaTime;
				}
			}
		}

	}

	public void increaseState(){
		currentState++;
		Debug.Log ("Current State: "+currentState);
	}

	public int getCurrentState(){
		return currentState;
	}

	public bool isOver(){
		if (getCurrentState () == 11) {
			return true;
		} else {
			return false;
		}
	}

	IEnumerator displayTutorial(){
		text.SetActive (true);
		yield return new WaitForSeconds(4);
		text.SetActive(false);
		changeTexture (0); // Look for food...
		text.SetActive (true);
		yield return new WaitForSeconds(4);
		text.SetActive(false);
		changeTexture (1); // You have a Compass
		text.SetActive (true);
		compass.SetActive (true);
		yield return new WaitForSeconds(4);
		text.SetActive(false);
		changeTexture (2); // Try to find some food
		text.SetActive (true);
		yield return new WaitForSeconds(4);
		text.SetActive(false);
	}
	
	
	IEnumerator displayText(){
		text.SetActive (true);
		yield return new WaitForSeconds(4);
		text.SetActive(false);
	}

	IEnumerator displayChageVector(){
		changeTexture(4);
		text.SetActive (true);
		yield return new WaitForSeconds(4);
		text.SetActive(false);
		changeTexture(5);
		text.SetActive (true);
		yield return new WaitForSeconds(4);
		text.SetActive(false);
	}


	void changeTexture(int i){
		Renderer rend = text.GetComponent<Renderer> ();
		if (rend != null) {
			rend.material = materials[i];
		}
	}

	public GameObject getFoodObject(){
		return food;
	}

	void OnTriggerEnter(Collider other) { 
		
		// Collect food
		if(other.gameObject.CompareTag("Food"))
		{
			changeTexture(3);
			food = other.gameObject;
			// cambiar texto de llevala a casa
			//food.transform.position = GameObject.FindGameObjectWithTag("NoFoodZone").GetComponent<NoFoodZone>().getInitialPositionFood().position;
			food.SetActive(false);
			// Compass 
			StartCoroutine( displayText());
			hasFood=true;


			compass.GetComponent<Compass>().setTarget(nest.transform);
			compass.GetComponent<Renderer>().material=GreenArrow;

			increaseState();
		}
		if(other.gameObject.CompareTag("Nest"))
		{
			if (hasFood) {
				StartCoroutine( displayChageVector());
				hasFood=false;
				compass.GetComponent<Compass>().setTarget(food.transform);
				compass.GetComponent<Renderer>().material=RedArrow;

				GameObject f= GameObject.FindGameObjectWithTag("Food");
				food.SetActive(true);
				tornadoZone = true;
				increaseState();
			}
			
		}
		if (other.gameObject.CompareTag ("TornadoZone")) {
			if(tornadoZone ){
				if(getCurrentState() == 3 || getCurrentState() == 6 || getCurrentState() == 9 ){
					GameObject player = GameObject.FindGameObjectWithTag ("Player");
					player.GetComponent<ShakeScreen> ().enabled = true;
					dust.SetActive(true);
				}
			}
		}
		

	}

}