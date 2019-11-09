using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingsun : MonoBehaviour {

    /*
     * public Gradient nightDayColor;
     * public float maxintensity = 3f;
     * public float minintensity = 0f;
     * public float minPoint = -0.2f;
    
     */ 

	//public float time;
	public float maxintensity = 1f;
	public float minintensity = 0f;
	public Vector3 speed;

	Light mainlight;

	// Use this for initialization
	void Start () {
		mainlight = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		float dot = Mathf.Clamp01 (Vector3.Dot (transform.forward, Vector3.down));
		mainlight.intensity = minintensity + (maxintensity - minintensity) * dot;
		//time += Time.deltaTime * speed;
		//transform.rotation = Quaternion.Euler (new Vector3 ((time - 21600) / 86400 * 360, 0, 0));
		transform.Rotate(speed*Time.deltaTime);
	
			
		//transform.RotateAround (Vector3.zero, Vector3.right, 10f * Time.deltaTime);
		//transform.LookAt (Vector3.zero);
		
	}
}
