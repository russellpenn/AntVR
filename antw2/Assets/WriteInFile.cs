using UnityEngine;
using System.Collections;

public class WriteInFile : MonoBehaviour {

	private System.IO.TextWriter tw;
	private float step;
	public GameObject player;
	private int counter;
	private bool recordData;
	private int experiment;

	// Use this for initialization
	void Start () {

		experiment = player.GetComponent<KidnappedRobot>().getExperiment();
		tw = new System.IO.StreamWriter("C:/Users/Public/Documents/Unity Projects/antw2/test" + experiment + ".txt");
		step = 1.0f;
		tw.WriteLine("x, y, z, rot1, rot2, rot3, experiment, state");
		counter = 0;
		recordData = true;
	}

	public void setExperiment(int i){
		experiment = i;
	}
	
	// Update is called once per frame
	void Update () {
		if (recordData) {
			if (step > 0) {
				step -= Time.deltaTime;
			} else {
				Vector3 pos = player.transform.position;
				Quaternion rot = player.transform.rotation;
				string line = pos.x + "," + pos.y + "," + pos.z+ ","+ rot.x +","+ rot.y+ ","+ rot.z+ ","+ experiment+ ","+player.GetComponent<behaviour>().getCurrentState() ;
				tw.WriteLine (line);
				step = 1.0f;
				counter++;
			}
			if (player.GetComponent<behaviour> ().isOver ()) {
				tw.Close ();
				recordData = false;
			}
		}
	}
}
