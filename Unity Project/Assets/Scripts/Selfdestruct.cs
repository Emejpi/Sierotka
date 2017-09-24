using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruct : MonoBehaviour {

    public float dur;
    float timer;

	// Use this for initialization
	void Start () {
        timer = Time.time + dur;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > timer)
        {
            Destroy(gameObject);
        }
	}
}
