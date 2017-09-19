using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe : MonoBehaviour {

    public Transform target;

    public float speedMod;

    public void LookAt(GameObject target)
    {
        this.target = target.transform;
    } 
	
	// Update is called once per frame
	void Update () {
        float step = 
            (GetComponent<MonkayCommands>().currentChar.GetComponent<CameraSettings>().focusingOnPlayerSpeed
            - speedMod) 
            * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);

        //transform.LookAt(target);
    }
}
