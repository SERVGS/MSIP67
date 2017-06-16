using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_render : MonoBehaviour {

	public GameObject OBJ1;
	public GameObject OBJ2;
	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void LateUpdate () {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, OBJ1.transform.position);
        lineRenderer.SetPosition(1, OBJ2.transform.position);

    }
}
