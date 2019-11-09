using UnityEngine;
using System.Collections;
using TMPro;


public class behaviour : MonoBehaviour
{

    public GameObject text;
    public GameObject startText;
    public GameObject tmp;
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
    public Material newMaterial8;
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
    public bool gameOver;
    public KidnappedRobot krobot;

    private int currentState;
    public TextMeshPro TextMeshP;
    public GameObject PlayerText;

    public GameObject PlayerCharacter;

	private bool challengeDone;
	private float angleToPosition;

	private bool antEyeRunning;

	private float lastAnglePosition;

	public Brightness BrightnessRef; 

    public GameObject secondCupcake;
    public Transform spawnPoint;

    public GameObject thirdCupcake;
    public Transform spawnPoint2;

    private Color guessColor;

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
    void Start()
    {
        displayTime = 5;
        showText = false;
        showGui = true;
        materials = new Material[8] { newMaterial1, newMaterial2, newMaterial3, newMaterial4, newMaterial5, newMaterial6, newMaterial7, newMaterial8 };
        hasFood = false;
        foodCount = 0;
        tornadoZone = false;
        currentState = 0;
        gameOver = false;
		challengeDone = false;
		antEyeRunning = false;

        
        StartCoroutine(displayTutorialText());

        Debug.Log("Current State: " + currentState);
        //gameObject.GetComponent<WriteInFile>().enabled=true;


    }

    // Update is called once per frame
    void Update()
    {




		if (challengeDone) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Rigidbody playerRigidBody = player.GetComponent<Rigidbody> ();
			OVRPlayerController controller = player.GetComponent<OVRPlayerController>();
			Vector3 targetDir = nest.transform.position - transform.position;
			targetDir = targetDir.normalized;
			float dot = Vector3.Dot (targetDir, transform.forward);
			angleToPosition = Mathf.Acos (dot) * Mathf.Rad2Deg;
			Debug.Log (angleToPosition);
			Debug.Log ("Nest Position " + nest.transform.position);
			Debug.Log ("Player position " + transform.position);
			Debug.Log ("Target Direction " + targetDir);
			StartCoroutine (displayChallengeResult ());
			challengeDone = false;
			controller.SetMoveScaleMultiplier (1f);
			playerRigidBody.freezeRotation = false;
			//antEyeRunning = true;
			//StartCoroutine (antEyeText ());

		}

		if (antEyeRunning) {

			Vector3 targetDir = nest.transform.position - transform.position;
			targetDir = targetDir.normalized;
			float dot = Vector3.Dot (targetDir, transform.forward);
			angleToPosition = Mathf.Acos (dot) * Mathf.Rad2Deg;

			/* if (angleToPosition < 10) {
				BrightnessRef.brightness = 1.6f;
			}
			if (angleToPosition > 10 & angleToPosition < 20) {
				BrightnessRef.brightness = 1.5f;
			}
			if (angleToPosition > 20 & angleToPosition < 30) {
				BrightnessRef.brightness = 1.4f;
			}
			if (angleToPosition > 30 & angleToPosition < 40) {
				BrightnessRef.brightness = 1.3f;
			}
			if (angleToPosition > 40 & angleToPosition < 50) {
				BrightnessRef.brightness = 1.2f;
			}
			if (angleToPosition > 50 & angleToPosition < 60) {
				BrightnessRef.brightness = 1.1f;
			}
			if (angleToPosition > 60 & angleToPosition < 70) {
				BrightnessRef.brightness = 1f;
			}
			if (angleToPosition > 70 & angleToPosition < 80) {
				BrightnessRef.brightness = 0.9f;
			}
			if (angleToPosition > 80 & angleToPosition < 90) {
				BrightnessRef.brightness = 0.8f;
			}
			if (angleToPosition > 90 & angleToPosition < 100) {
				BrightnessRef.brightness = 0.7f;
			}
			if (angleToPosition > 100 & angleToPosition < 110) {
				BrightnessRef.brightness = 0.6f;
			}
			if (angleToPosition > 110 & angleToPosition < 120) {
				BrightnessRef.brightness = 0.5f;
			}
			if (angleToPosition > 120 & angleToPosition < 130) {
				BrightnessRef.brightness = 0.4f;
			}
			if (angleToPosition > 130 & angleToPosition < 140) {
				BrightnessRef.brightness = 0.3f;
			}
			if (angleToPosition > 140 & angleToPosition < 150) {
				BrightnessRef.brightness = 0.2f;
			}
			if (angleToPosition > 150 & angleToPosition < 160) {
				BrightnessRef.brightness = 0.1f;
			}
			if (angleToPosition > 160 & angleToPosition < 180) {
				BrightnessRef.brightness = 0.05f;
            }*/

            /* if (angleToPosition < 10 & angleToPosition > -10) {
                BrightnessRef.brightness = 1f;
            }
            else {
                BrightnessRef.brightness = 1.5f  * (1.5f/angleToPosition);
            }  */
            BrightnessRef.brightness = 1.2f  * (1f/angleToPosition);
			/*if (angleToPosition < lastAnglePosition) {
				BrightnessRef.brightness = (1.5f * angleToPosition) / 180;
			}
			if (angleToPosition > lastAnglePosition) {
				BrightnessRef.brightness = (0.5f * angleToPosition) / 180;
			}*/


			lastAnglePosition = angleToPosition;

			Debug.Log (RenderSettings.ambientLight);
		}

        /* bn   
        
        
        
        if (isOver())
        {
            changeTexture(7);
            text.SetActive(true);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<OVRPlayerController>().enabled = false;
        }*/


        if (showGui)
        {
            if (showText)
            {
                //StartCoroutine(displayTutorial());
                //displayTutorial();
                showGui = false;
            }
            else
            {
                if (displayTime < 0)
                {
                    showText = true;
                    displayTime = 5;
                }
                else
                {
                    displayTime -= Time.deltaTime;
                }
            }
        }

    }

    public void increaseState()
    {
        currentState++;
        Debug.Log("Current State: " + currentState);
    }

    public int getCurrentState()
    {
        return currentState;
    }

    public bool isOver()
    {
        if (getCurrentState() == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator displayTutorialText()
    {
        PlayerText.SetActive(true);
		yield return new WaitUntil(() => Input.anyKey);
        PlayerText.SetActive(false);
        TextMeshP.text = "We need your help!";
        TextMeshP.color = new Color(0, 0, 0);
        TextMeshP.fontSize = 18;
        PlayerText.SetActive(true);
        yield return new WaitForSeconds(4);
        PlayerText.SetActive(false);
        TextMeshP.text = "Find some food and bring it home!";
        PlayerText.SetActive(true);
		yield return new WaitForSeconds (4);
        PlayerText.SetActive(false);
        TextMeshP.text = "Use the joystick to move forward";
        PlayerText.SetActive(true);
		yield return new WaitForSeconds (4);
        PlayerText.SetActive(false);
        TextMeshP.text = "Rotate your head to change direction";
        PlayerText.SetActive(true);
		yield return new WaitForSeconds (4);
        PlayerText.SetActive(false);
    }

    IEnumerator displayFoundFoodText()
    {
        TextMeshP.text = "You have found some food";
        PlayerText.SetActive(true);
        yield return new WaitForSeconds(4);
		PlayerText.SetActive (false);

    }

	IEnumerator displayChallangeText()
	{
		TextMeshP.text = "Now you need to take the food back!";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(4);
		PlayerText.SetActive (false);
		TextMeshP.text = "Which direction do you think home is?";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds(4);
		PlayerText.SetActive (false);
		TextMeshP.text = "Turn towards it";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds (2);
		PlayerText.SetActive (false);
		TextMeshP.text = "You have <color=red>5</color> seconds";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds (4);
		PlayerText.SetActive (false);
		TextMeshP.text = "5";
		TextMeshP.color = new Color(255, 0, 0);
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);
		TextMeshP.text = "4";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);
		TextMeshP.text = "3";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);
		TextMeshP.text = "2";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);
		TextMeshP.text = "1";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);
		challengeDone = true;

	}

	IEnumerator displayChallengeResult() {
		TextMeshP.faceColor = new Color (255, 255, 255);
        TextMeshP.color = new Color(255, 255, 255);
		TextMeshP.richText = true;
        if (angleToPosition < 45) {
            TextMeshP.text = "YOU WERE OUT BY " + "<color=green>" + angleToPosition +  "</color> DEGREES";
        }
        else if (angleToPosition < 90) {
            TextMeshP.text = "YOU WERE OUT BY " + "<color=yellow>" + angleToPosition +  "</color> DEGREES";
        }
        else if (angleToPosition > 90) {
            TextMeshP.text = "YOU WERE OUT BY " + "<color=red>" + angleToPosition +  "</color> DEGREES";
        }
		//TextMeshP.text = "YOU WERE OUT BY " + "<color=" + guessColor + ">" + angleToPosition +  "</color> DEGREES";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds (4);
		PlayerText.SetActive (false);
        TextMeshP.color = new Color(255,255,255);
		TextMeshP.text = "Ants see the world differently";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds (4);
        PlayerText.SetActive (false);
		TextMeshP.text = "They see polarised light";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds (4);
		PlayerText.SetActive (false);
        TextMeshP.text = "They use this to navigate";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds (4);
		PlayerText.SetActive (false);
		TextMeshP.text = "We are giving you an <color=red>ANT EYE</color>";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds (4);
		PlayerText.SetActive (false);
		TextMeshP.text = "Use it to find home!";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds (4);
        PlayerText.SetActive (false);
		TextMeshP.text = "Hint : look around slowly!";
		PlayerText.SetActive (true);
		yield return new WaitForSeconds (4);
        antEyeRunning = true;
        yield return new WaitForSeconds(8);
		PlayerText.SetActive (false);
        //antEyeRunning = true;
	}

    IEnumerator findMoreFoodText() {
        TextMeshP.text = "Well done!";
        PlayerText.SetActive(true);
        yield return new WaitForSeconds(2);
        PlayerText.SetActive(false);
        TextMeshP.text = "You have found some food!";
        PlayerText.SetActive(true);
        yield return new WaitForSeconds(4);
        PlayerText.SetActive(false);
        TextMeshP.text = "You need to find more!";
        PlayerText.SetActive(true);
        yield return new WaitForSeconds(4);
        PlayerText.SetActive(false);
    }

    IEnumerator lastFoodText() {
        TextMeshP.text = "Almost there!";
        PlayerText.SetActive(true);
        yield return new WaitForSeconds(4);
        PlayerText.SetActive(false);
        TextMeshP.text = "You need to find one more piece!";
        PlayerText.SetActive(true);
        yield return new WaitForSeconds(4);
        PlayerText.SetActive(false);
        TextMeshP.text = "Look around until you see it!";
        PlayerText.SetActive(true);
        yield return new WaitForSeconds(4);
        PlayerText.SetActive(false);
    }

     IEnumerator displayFinishedText()
    {
        PlayerText.SetActive(false);
        TextMeshP.text = "Congratulations!";
        TextMeshP.faceColor = new Color(255, 255, 255);
        PlayerText.SetActive(true);
        yield return new WaitForSeconds(4);
        PlayerText.SetActive(false);
        TextMeshP.text = "You have completed <color=red>the Ant Navigation Challenge!</color>";
        PlayerText.SetActive(true);
		yield return new WaitForSeconds (4);
        PlayerText.SetActive(false);
        TextMeshP.text = "Want to know more?";
        PlayerText.SetActive(true);
		yield return new WaitForSeconds (4);
        PlayerText.SetActive(false);
        TextMeshP.text = "Ask us a question!";
        PlayerText.SetActive(true);
    }

	IEnumerator countdownTimerText()
	{
		TextMeshP.text = "5";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);
		TextMeshP.text = "4";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);
		TextMeshP.text = "3";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);
		TextMeshP.text = "2";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);
		TextMeshP.text = "1";
		PlayerText.SetActive(true);
		yield return new WaitForSeconds(1);
		PlayerText.SetActive (false);

	}

    /* IEnumerator displayTutorial()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(4);
        text.SetActive(false);
        changeTexture(0); // Look for food...
        text.SetActive(true);
        yield return new WaitForSeconds(4);
        text.SetActive(false);
        changeTexture(1); // You have a Compass
        text.SetActive(true);
        compass.SetActive(true);
        yield return new WaitForSeconds(4);
        text.SetActive(false);
        changeTexture(2); // Try to find some food
        text.SetActive(true);
        yield return new WaitForSeconds(4);
        text.SetActive(false);
    }


    /* IEnumerator displayText()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(4);
        text.SetActive(false);
    }

    IEnumerator displayChageVector()
    {
        changeTexture(4);
        text.SetActive(true);
        yield return new WaitForSeconds(4);
        text.SetActive(false);
        changeTexture(5);
        text.SetActive(true);
        yield return new WaitForSeconds(4);
        text.SetActive(false);
    }*/


    /* void changeTexture(int i)
    {
        Renderer rend = text.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material = materials[i];
        }
    }*/

    public GameObject getFoodObject()
    {
        return food;
    }


    void OnTriggerEnter(Collider other)
    {

        // Collect food
        if (other.gameObject.CompareTag("Food"))
        {
            //changeTexture(3);
            food = other.gameObject;
            // cambiar texto de llevala a casa
            //food.transform.position = GameObject.FindGameObjectWithTag("NoFoodZone").GetComponent<NoFoodZone>().getInitialPositionFood().position;
            food.SetActive(false);
            // Compass 
            //StartCoroutine(displayFoundFoodText());
            hasFood = true;
            if (hasFood & currentState == 0) {
                StartCoroutine(findMoreFoodText());
                Instantiate(secondCupcake, spawnPoint.position, spawnPoint.rotation);
            }
            if (hasFood & currentState == 1) {
                StartCoroutine(lastFoodText());
                Instantiate(thirdCupcake, spawnPoint2.position, spawnPoint2.rotation);
            }
			if (hasFood & currentState == 2) {
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				Rigidbody playerRigidBody = player.GetComponent<Rigidbody> ();
				OVRPlayerController controller = player.GetComponent<OVRPlayerController>();
				controller.SetMoveScaleMultiplier (0f);
				StartCoroutine (displayChallangeText ());
				playerRigidBody.freezeRotation = true;
                //Vector3 toPosition = (nest.transform.position - transform.position);
				//float angleToPosition = Vector3.Angle (transform.forward, toPosition);
				Debug.Log(challengeDone);
				//StartCoroutine (countdownTimerText ()); 
			}

            //compass.GetComponent<Compass>().setTarget(nest.transform);
            //compass.GetComponent<Renderer>().material = GreenArrow;

            increaseState();
        }
        if (other.gameObject.CompareTag("Nest"))
        {
            if (hasFood)
            {   
                antEyeRunning = false;
                BrightnessRef.brightness = 1f;
                GameObject player = GameObject.FindGameObjectWithTag ("Player");
				Rigidbody playerRigidBody = player.GetComponent<Rigidbody> ();
				OVRPlayerController controller = player.GetComponent<OVRPlayerController>();
				controller.SetMoveScaleMultiplier (0f);
                StartCoroutine(displayFinishedText());
                //StartCoroutine(displayChageVector());
                //hasFood = false;
                compass.GetComponent<Compass>().setTarget(food.transform);
                compass.GetComponent<Renderer>().material = RedArrow;

                GameObject f = GameObject.FindGameObjectWithTag("Food");
                food.SetActive(true);
                tornadoZone = true;
                increaseState();
            }

        }
        if (other.gameObject.CompareTag("TornadoZone"))
        {
            if (tornadoZone)
            {
                if (getCurrentState() == 3 || getCurrentState() == 6 || getCurrentState() == 9)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<ShakeScreen>().enabled = true;
                    dust.SetActive(true);
                }
            }
        }


    }

}