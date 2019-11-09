using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public GameObject cont;
	public GameObject skip;
	public GameObject compass;

	public Material welcome; 					//0
	public Material youWillTry;					//1
	public Material lookAround;					//2
	public Material youHaveACompass;			//7
	public Material yourTaskIs;					//3
	public Material lookAroundToFindFood;		//4
	public Material Great;						//5
	public Material rotateYourself;				//6
	public Material nowBringIt;					//8
	public Material Okay;						//9
	public Material youCompassIsNow;			//10
	public Material beCareful;					//11
	public Material beAwareOfYourSurrounding;	//12
	public Material congratulations;			//13
	private int state;
	private bool changeState;
	private bool controllerActive;

	public GameObject boxView;
	public GameObject lookAtHereText;
	public GameObject triggerNest;

	private Material[] materials;

	public GameObject text;
	public GameObject antPic;

	public GameObject food;

	public Material RedArrow;
	public Material GreenArrow;

	private bool firstRound;
	private bool startGame;
	private bool init;

	public GameObject nestTrigger;
	public GameObject blackScreen;

	// Use this for initialization
	void Start () {

		init = true;
		materials = new Material[14]{welcome,youWillTry,lookAround,yourTaskIs,lookAroundToFindFood,Great,
			rotateYourself,youHaveACompass,nowBringIt,Okay,youCompassIsNow,beCareful,beAwareOfYourSurrounding,congratulations};

		StartCoroutine( startTurorial());
		changeTexture (0);
		state = 0;
		changeState = false;
		gameObject.GetComponent<OVRPlayerController>().enabled=false;

		controllerActive = true;
		firstRound = true;
		startGame = false;
	}

	IEnumerator startTurorial(){
		yield return new WaitForSeconds(3);
		text.SetActive (true);
		cont.SetActive (true);
		skip.SetActive (true);
	}

	void changeTexture(int i){
		Renderer rend = text.GetComponent<Renderer> ();
		if (rend != null) {
			rend.material = materials[i];
		}
	}
	// Update is called once per frame
	void Update () {

		if (changeState) {
			checkState (state);
			changeState = false;
		} else{
			if (OVRGamepadController.GPC_GetButtonUp (OVRGamepadController.Button.X) && controllerActive) {
				state+=1;
				Debug.Log("Entre a X" + state);
				changeState = true;
			}
		}

		if (startGame) {
			Application.LoadLevel("AntWorldHD");
		}

		if(init){
			if (OVRGamepadController.GPC_GetButtonUp (OVRGamepadController.Button.B)) {
				blackScreen.SetActive(true);
				Application.LoadLevel("AntWorldHD");
			}
		}
	}

	public void updateState(int state){
		this.state = state;
		changeState = true;

	}

	void checkState(int state){
		switch (state) {
		case 1:
			changeTexture(1); // You will try
			skip.SetActive(false);

			init = false;
			break;
		case 2: // Ant Pic
			antPic.SetActive(true);		
			text.SetActive(false);

			break;
		case 3: //Look around
			antPic.SetActive(false);		
			changeTexture(2);
			text.SetActive(true);
			controllerActive = false;
			boxView.SetActive(true);
			lookAtHereText.SetActive(true);
			cont.SetActive(false);
			StartCoroutine(dissapearTextInSeconds(5));
			break;	
		case 4: // Your Task is
			text.SetActive(true);
			controllerActive = true;
			cont.SetActive(true);
			changeTexture(3);
			boxView.SetActive(false);
			triggerNest.SetActive(false);
			break;
		case 5:// Look Around
			changeTexture(4);
			cont.SetActive(false);
			controllerActive=false;
			food.SetActive(true);
			boxView.SetActive(true);
			StartCoroutine(dissapearTextInSeconds(5));
			break;
		case 6://Great
			text.SetActive(true);
			changeTexture(5);
			controllerActive=true;
			cont.SetActive(true);
			break;
		case 7://Rotate yourself
			changeTexture(6);
			controllerActive=false;
			cont.SetActive(false);
			StartCoroutine(dissapearTextInSeconds(5));
			gameObject.GetComponent<OVRPlayerController>().enabled=true;
			break;
		case 8: // You have a compass
			controllerActive=true;
			compass.SetActive(true);
			cont.SetActive(true);
			text.SetActive(true);
			gameObject.GetComponent<OVRPlayerController>().enabled=false;
			break;
		case 9: // Use Your compass
			controllerActive=false;
			gameObject.GetComponent<OVRPlayerController>().enabled=true;
			changeTexture(8);
			cont.SetActive(false);
			StartCoroutine(dissapearTextInSeconds(7));
			break;
		case 10: // Okay
			text.SetActive(true);
			controllerActive=true;
			cont.SetActive(true);
			gameObject.GetComponent<OVRPlayerController>().enabled=false;
			nestTrigger.SetActive(false);
			break;
		case 11: // Your Compass is now pointing
			gameObject.GetComponent<OVRPlayerController>().enabled=true;
			changeTexture(10);
			controllerActive=false;
			cont.SetActive(false);
			StartCoroutine(dissapearTextInSeconds(7));
			break;
		case 12: // Be Careful
			gameObject.GetComponent<OVRPlayerController>().enabled=false;
			text.SetActive(true);
			controllerActive=true;		
			cont.SetActive(true);
			break;
		case 13: // Be Aware
			gameObject.GetComponent<OVRPlayerController>().enabled=true;
			changeTexture(12);
			controllerActive=false;
			StartCoroutine(dissapearTextInSeconds(7));
			cont.SetActive(false);
			nestTrigger.SetActive(true);
			break;
		case 14:
			startGame=true;
			blackScreen.SetActive(true);
			text.SetActive(false);
			cont.SetActive(false);
			break;
		}
	}

	IEnumerator dissapearTextInSeconds(int i){
		yield return new WaitForSeconds(i);
		text.SetActive (false);
	}

	void OnTriggerEnter(Collider other) { 
		
		// Collect food
		if (other.gameObject.CompareTag ("Cupcake")) {
			if(firstRound){
				changeTexture (7);
				updateState(8);
			}else{
				changeTexture (11);
				updateState(12);
				compass.GetComponent<Compass>().setTarget(triggerNest.transform);
			}
			food = other.gameObject;
			// cambiar texto de llevala a casa
			food.SetActive (false);
			// Compass 
		}
		if (other.gameObject.CompareTag ("Nest")) {
			if(firstRound){
				changeTexture (9);
				updateState(10);
				firstRound=false;
				food.SetActive(true);
			}else{
				changeTexture (13);
				text.SetActive(true);
				compass.SetActive(false);
				food.SetActive(false);
				cont.SetActive(true);
				nestTrigger.SetActive(false);
				controllerActive=true;
			}
			compass.GetComponent<Compass>().setTarget(food.transform);
			compass.GetComponent<Renderer>().material=RedArrow;


		}
	}

}
