using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normaltransformation : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MeshFilter[] meshfilters = GetComponentsInChildren<MeshFilter>();
		foreach (MeshFilter meshf in meshfilters) {
			Mesh mesh = meshf.mesh;
			mesh.RecalculateNormals ();
		}


	}
}
