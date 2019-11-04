using UnityEngine;
using System.Collections;

public class textAlignment : MonoBehaviour {

	public Camera cameraFacing;
	private Vector3 originalScale;
	void Start () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		float distance;
		if (Physics.Raycast (new Ray (cameraFacing.transform.position, cameraFacing.transform.rotation * Vector3.forward), out hit)) {
			distance = hit.distance;
		} else {
			distance = cameraFacing.farClipPlane * 0.95f;
		}
		transform.position = cameraFacing.transform.position + cameraFacing.transform.rotation * Vector3.forward * distance;
		if (distance < 10.0f) {
			distance *= 1 + 5 * Mathf.Exp (-distance);
		}

		transform.localScale = originalScale * distance;
	}
}
