using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public Vector3 rotationAxis;
    public float rotationsPerSecond;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    transform.RotateAround(transform.position, rotationAxis, Time.deltaTime * 360 / rotationsPerSecond);	
	}
}
