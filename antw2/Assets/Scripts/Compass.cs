using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

    public Transform target;
	private Quaternion rotation;

	void Start(){
		rotation = transform.rotation;
	}

	void Update () {
        //transform.LookAt( target.transform.position);
        transform.LookAt(target);
        transform.rotation = transform.rotation * Quaternion.Euler(90, 	0, 0);
		//rotation = transform.rotation;
		//rotation.y += 250;
		//transform.rotation = rotation;

	}

	public void setTarget(Transform newTarget){
		target = newTarget;
	}
}
