using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionFromCameraSettings : MonoBehaviour {

    public CameraSettings set;

	// Use this for initialization
	void Start () {
        transform.localPosition = new Vector3(set.position.x, set.position.y, -set.distanceFromCharacter);
        Destroy(this);
	}

}
