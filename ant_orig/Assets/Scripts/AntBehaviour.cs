using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AntBehaviour : MonoBehaviour {

    public GameObject textGoHome;
    public Text countText;
	public GameObject compass;
	public GameObject sandStorm;
    private float displayTime;  // Time for text to show on screen
    private bool showText;      // Boolean for showing text
    private bool hasFood;       // Boolean for collecting food
    private int count;          // How many food (cookies) has collected
	private GameObject food;	// Reference for when player finds food
	private float kiddnaped;
	private bool sandStormFlag;
	//public GameObject NoFoodZoneSphere;

	// Use this for initialization
	void Start () 
    {
        displayTime = 3f;
        showText = false;
        hasFood = false;
		sandStormFlag = false;
        count = 0;
		kiddnaped = 5f;

	}

	// Manages collisions with triggers and player
    void OnTriggerEnter(Collider other) { 

		// Collect food
        if(other.gameObject.CompareTag("Cookie"))
        {
			textGoHome.GetComponent<Text>().text = "Bring the cookie home!";
            other.gameObject.SetActive(false);
            textGoHome.SetActive(true);
            showText = true;
            hasFood = true;

			// Compass 
			compass.SetActive(true);
			//compass.GetComponent<Compass>().setTarget(GameObject.FindGameObjectWithTag("Nest"));	// Assign target as Nest when player grabs food
			food = other.gameObject;
        }

        if (other.gameObject.CompareTag("Nest"))
        {
            if (hasFood) {
				//Debug.Log("hasFood");
                count++;
                SetCountText();
				food.SetActive(true);

				//compass.GetComponent<Compass>().setTarget(food);	
				textGoHome.GetComponent<Text>().text = "Go get more cookies at the same place!";
				textGoHome.SetActive(true);
				showText = true;
				//script.InstantiateObject();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (showText) {
            if (displayTime < 0)
            {
                textGoHome.SetActive(false);
                showText = false;
                displayTime = 3f;
            }
            else {
                displayTime -= Time.deltaTime;
            }
        }
		if (count == 1) {
			//Contar 3 segundos
			if (kiddnaped < 0)
			{
				count=-1; // avoids entering again to the if statement
				KidnappedRobot();

			}
			else {
				kiddnaped -= Time.deltaTime;
			}
			if(!sandStormFlag){
				sandStorm.SetActive (true);
				sandStormFlag = true;
			}
		}
	}

    void SetCountText()
    {
        countText.text = "Cookies: " + count.ToString();
        hasFood = false;
    }

	void KidnappedRobot(){
		Debug.Log("Inicia Kidnapped Robot");

		// Inmovilisar al player 

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		//player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;

		// Mover la camara
		player.GetComponent<ShakeScreen> ().enabled = true;

		// Wind animation


		// Respawm in random location (agregar viento y particulas de polvo de Standard Assets)


		// Cambiar el vector por signo de interrogacion
	}
}
