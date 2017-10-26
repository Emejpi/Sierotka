using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe : MonoBehaviour {

    public Transform target;

    public float speedMod;

    public bool takeFormMonaky;

    public void LookAt(GameObject target)
    {
        if (target)
            this.target = target.transform;
        else
            this.target = null;
    } 
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            float step =
                (takeFormMonaky? 
                GetComponent<MonkayCommands>().currentChar.GetComponent<CameraSettings>().focusingOnPlayerSpeed- speedMod 
                : speedMod)
                * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
        }
        //transform.LookAt(target);
    }
}
