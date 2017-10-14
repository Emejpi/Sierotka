using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateColliderWithTimer : MonoBehaviour {

    float timer;

	// Use this for initialization
	void Start () {
        timer = Time.time + 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer < Time.time)
        {
            GetComponent<Collider>().enabled = true;
            Destroy(this);
        }
	}
}
