using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMe : MonoBehaviour {


    public GameObject target;
    public float speed;
    float startSpeed;

    bool selfEnable;

    void Start()
    {
        selfEnable = false;
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

    public void GoTo(GameObject target)
    {
        selfEnable = true;
        Follow(target);

    }

	// Update is called once per frame
	void Update () {
        if (target)
        {
            transform.position = GetComponent<Lerper>().LerpVector3(transform.position, target.transform.position, speed);

            if (selfEnable && DistanceFormTarget() < 0.1f)
            {
                selfEnable = false;
                Follow(null);
                GetComponent<LookAtMe>().LookAt(null);
            }
        }
    }
}
