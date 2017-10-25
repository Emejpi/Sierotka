using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkayTargetChanger : MonoBehaviour {

    float timer;
    public Vector2 timerDur;
    public float distance;

    public bool halfCircel;

	// Use this for initialization
	void Start () {
		
	}
	
    public void Change()
    {
        float value = Random.Range(-Mathf.PI, halfCircel? 0 : Mathf.PI);
        transform.position = transform.parent.position + new Vector3(Mathf.Cos(value), 0, Mathf.Sin(value)) * distance;

        timer = Time.time + Random.Range(timerDur.x, timerDur.y);
    }

	// Update is called once per frame
	void Update () {
		if(timer < Time.time)
        {
            Change();
        }
	}
}
