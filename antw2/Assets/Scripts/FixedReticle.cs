using UnityEngine;
using System.Collections;

public class FixedReticle : MonoBehaviour {
	public Camera cameraFacing;

	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {		
		transform.position = cameraFacing.transform.position + cameraFacing.transform.rotation * Vector3.forward * 2;
		transform.LookAt (cameraFacing.transform.position);
		transform.Rotate (0.0f,180.0f,0.0f);
	
	}
}
