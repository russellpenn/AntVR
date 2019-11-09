using UnityEngine;
using System.Collections;

public class KidnappedRobot : MonoBehaviour {

	public GameObject dust;
	private GameObject player;
	public GameObject text;
	public Material OhNo;		
	public Material CanYouBring;
	public Material IsGettingHotter;
	public Material FindHomeBefore;
	public Material TimeIsOver;
	public Material ChallengeComplete;

	private float timeLeft;
	public TextMesh clock;

	private Material[] materials;

	private bool dustInit;
	private bool startTimer;
	private bool isOverbool;

	private int experiment;

	public GameObject nest;
    private bool isDead;

    public behaviour antBehaviour;

    public GameObject compassObject;

    // Use this for initialization
    void Start () {

        //player.GetComponent<Behaviour>().enabled = false;
        //player.GetComponent<behaviour>().enabled = false;
        timeLeft = 299;
		player = GameObject.FindGameObjectWithTag ("Player");
	

		materials = new Material[6]{OhNo,CanYouBring,IsGettingHotter,FindHomeBefore,TimeIsOver,ChallengeComplete};
		dustInit = true;
		startTimer = false;
		isOverbool = false;

        isDead = false;

		experiment = 0;
	}


	
	// Update is called once per frame
	void Update () {

        if (isDead)
        {
            player.GetComponent<OVRPlayerController>().enabled = false;
        }

        if (isDead == false && isOverbool == false)
        {
            player.GetComponent<OVRPlayerController>().enabled = true;
        }

        if (dustInit) {
			if (dust.GetComponent<ParticleSystem> ().particleCount > 20) {
				dust.GetComponent<ParticleSystem> ().maxParticles -= 5;
			} else {
				dust.SetActive (false);
				dustInit = false;
				changeTexture(0);
				StartCoroutine( displayText());
				experiment = 1;
				//player.GetComponent<WriteInFile>().enabled=true;

			}
		}

		if (startTimer) {
			timeLeft -= Time.deltaTime;
			string minutes = ((int)timeLeft / 60).ToString ();
			string seconds = (timeLeft % 60).ToString ("f0");
			if ((timeLeft % 60) < 10) {
				string s = "0" + seconds;
				seconds = s;

			}
			
			clock.text = minutes + ":" + seconds;
			if(timeLeft<=0){
				startTimer=false;
				changeTexture(4);
				text.SetActive(true);
				player.transform.position = nest.transform.position;
				// Moverlo al nest // reiniciar todo
			}
		}

		int currentState = player.GetComponent<behaviour>().getCurrentState();
		if(currentState == 5 || currentState == 8 ){
			// tests 1 or 2 complete
			startTimer=false;
			clock.gameObject.SetActive(false);
			
			timeLeft = 299;
			experiment= 2;
			if(currentState == 8){
				experiment=3;
			}
		}else if(currentState == 9 || currentState == 7 ){
			// test 3 complete, game over
			startTimer=true;
			clock.gameObject.SetActive(true);
            Debug.Log("Triggered clock, time left: " + timeLeft);
        }
		else if(currentState == 11 || currentState == 4){
			// test 3 complete, game over
			changeTexture(5);
			text.SetActive(true);
			isOverbool =true;
            startTimer = false;
        }
//        clock.text = string.Format("{0:HH:mm:ss}", timeLeft);
        clock.text = string.Format("{0}",timeLeft);

        Debug.Log("Current time left (end): " + timeLeft);
    }

	void changeTexture(int i){
		Renderer rend = text.GetComponent<Renderer> ();
		if (rend != null) {
			rend.material = materials[i];
		}
	}

	public bool isOver(){
		return isOverbool;
	}

	public int getExperiment(){
		return experiment;
	}

	IEnumerator displayText(){

        antBehaviour.compass.SetActive(false);
		text.SetActive (true);
        
        yield return new WaitForSeconds(3);
		changeTexture (1);
        isDead = true;
        yield return new WaitForSeconds(3);
		text.SetActive(false);
		yield return new WaitForSeconds(2);
		changeTexture (2);
		text.SetActive(true);
        yield return new WaitForSeconds(2);
		changeTexture (3);
		yield return new WaitForSeconds(2);
		startTimer = true;
		clock.gameObject.SetActive (true);
        isDead = false;

        Debug.Log("Current time left (enum): " + timeLeft);
        text.SetActive(false);
	}


}
