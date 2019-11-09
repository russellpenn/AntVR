using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WelcomeScript : MonoBehaviour {

    private TextMeshPro textMesh;

	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMeshPro>();

        //StartCoroutine(displayTutorial());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator displayTutorial()
    {
        textMesh.SetText("Ant Navigation Challenge");
        yield return new WaitForSeconds(5);
        textMesh.SetText("You've got it");
            
    }
}
