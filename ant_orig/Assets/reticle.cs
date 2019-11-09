using UnityEngine;
using System.Collections;

public class reticle : MonoBehaviour {

	public Camera cameraFacing;
	private Vector3 originalScale;
	public float offset;
	//public float distance;

	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

		RaycastHit hit;
		float distance;
		if (Physics.Raycast (new Ray (cameraFacing.transform.position, cameraFacing.transform.rotation * Vector3.forward), out hit)) {
			distance = hit.distance;
		} else {
			distance = cameraFacing.farClipPlane * 0.95f;
		}

		transform.position = cameraFacing.transform.position + cameraFacing.transform.rotation * Vector3.forward * distance;
		transform.LookAt (cameraFacing.transform.position);
		transform.Rotate (0.0f,180.0f,0.0f);



		Vector3 temp = transform.position;
		temp.y += offset;
		transform.position = temp;

		if (distance < 10.0f) {
			distance *= 1 + 5*Mathf.Exp(-distance);
		}

		transform.localScale = originalScale * distance;
	}
}
