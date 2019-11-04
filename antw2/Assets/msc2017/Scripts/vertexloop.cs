using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vertexloop : MonoBehaviour {

	public Mesh Testmesh;

	//public Material mat1;
	//public Material mat2;
	float height;
	Renderer rend;
	// Use this for initialization
	void Start () {
		height = 0;
		//rend = GetComponent<Renderer> ();

		float vertexnum = 0;
		foreach (var vertex in Testmesh.vertices) {
			vertexnum += 1;
			if (vertex.z < height) {
				height = vertex.z;
			}
		}
		//Debug.Log (vertexnum);
		//Debug.Log (height);

		//set the material properties via script
		//rend.materials[0].SetFloat("_pivot", height);
		//rend.materials[1].SetFloat("_pivot", height);
	}
	
	// Update is called once per frame
	void Update () {
		
			
	}
}
