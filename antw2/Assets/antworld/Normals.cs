using UnityEngine;
using System.Collections;

public class Normals : MonoBehaviour {

    public float speed = 100.0F;

    void Start() {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] normals = mesh.normals;
        //Quaternion rotation = Quaternion.AngleAxis(Time.deltaTime * speed, Vector3.up);
        int i = 0;
        while (i < normals.Length) {
          //  normals[i] = rotation * normals[i];
            normals[i] = Vector3.zero;
            i++;
        }
        mesh.normals = normals;
    }
}