﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMe : MonoBehaviour {


    public GameObject target;
    public float speed;
    float startSpeed;
	
    void Start()
    {
        startSpeed = speed;
    }

    public float DistanceFormTarget()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public bool StartSpeedIfOverStart()
    {
        if (startSpeed < speed)
        {
            speed = startSpeed;
            return true;
        }
        return false;
    } 

    public void Follow(GameObject target)
    {
        this.target = target;
    }

	// Update is called once per frame
	void Update () {
        if(target)
            transform.position = GetComponent<Lerper>().LerpVector3(transform.position, target.transform.position, speed);
	}
}
