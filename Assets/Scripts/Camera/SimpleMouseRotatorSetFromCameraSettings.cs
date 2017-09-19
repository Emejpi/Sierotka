using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMouseRotatorSetFromCameraSettings : MonoBehaviour {

    public CameraSettings set;

	// Use this for initialization
	void Start () {
        GetComponent<SimpleMouseRotator>().dampingTime = set.damingTime;
        GetComponent<SimpleMouseRotator>().rotationRange = new Vector2(set.maxRotationAngleUp, set.maxRotationAngleDown);
        GetComponent<SimpleMouseRotator>().rotationSpeed = set.rotationSpeed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
