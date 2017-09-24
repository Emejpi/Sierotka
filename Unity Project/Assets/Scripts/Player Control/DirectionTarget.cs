using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTarget : MonoBehaviour {

    public bool going;

    public float speed;

	// Use this for initialization
	void Start () {
        going = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (going)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
	}

    public void Go(Transform parent)
    {
        transform.position = parent.position;
        transform.eulerAngles = parent.rotation.eulerAngles;
        transform.parent = null;
        going = true;
    }

    public void Stop()
    {
        going = false;
    }
}
