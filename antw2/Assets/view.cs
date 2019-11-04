using UnityEngine;
using System.Collections;

public class view : MonoBehaviour {

	public GameObject camera;
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = camera.transform.rotation;
	}
}
