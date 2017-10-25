using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour {

    SoundMaker soundMaker;

    public float soundPower;

	// Use this for initialization
	void Start () {
        soundMaker = GetComponent<SoundMaker>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision body)
    {
        soundMaker.Sound((int)(GetComponent<Rigidbody>().velocity.magnitude * soundPower));
    }
}
