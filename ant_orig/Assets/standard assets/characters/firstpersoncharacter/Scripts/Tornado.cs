using UnityEngine;
using System.Collections;

public class Tornado : MonoBehaviour {

	public GameObject tornado;
	public GameObject positionNest;
	// Use this for initialization
	void Start () {
		tornado.gameObject.SetActive (true);
		Instantiate (tornado, transform.position + (transform.forward * 50) + (transform.up * 15), transform.rotation);
		Instantiate (tornado, positionNest.transform.position, transform.rotation);

		tornado.transform.position = positionNest.transform.position;
        //positionNest.transform.position = tornado.transform.position;

    }


}
