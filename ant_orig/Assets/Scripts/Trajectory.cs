using UnityEngine;
using System.Collections;

public class Trajectory : MonoBehaviour {

	public GameObject player;
	public GameObject trajectory;
	private Vector3[] points;
	private float trajectoryStepTime;
	private int step;
	private bool getTrajectory;
	
	


	// Use this for initialization
	void Start () {
		trajectoryStepTime = 1f;
		points = new Vector3[50];
		step = 0;
		getTrajectory = true;
		trajectory.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (getTrajectory) {
			if (trajectoryStepTime < 0)
			{
				points[step++] = player.transform.position;
				trajectoryStepTime = 1f;	
				Debug.Log("Step: "+step);
				
				if(step == 50){
					DrawTrajectory();
					getTrajectory = false;
				}
			}
			else {
				trajectoryStepTime -= Time.deltaTime;
			}
		}
	}

	void DrawTrajectory(){
		for (int i=0; i < 50; i++) {
			trajectory.GetComponent<LineRenderer>().SetPosition(i,points[i]);
		}

		trajectory.SetActive(true);
	}
}
