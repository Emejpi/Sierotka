using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParenting : MonoBehaviour {

    public Transform parent;

    public bool rotation;
    public bool position;

	// Update is called once per frame
	void Update () {
        if (parent)
        {
            if (rotation)
            {
                //transform.rotation = parent.transform.rotation;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, parent.transform.eulerAngles.y, transform.eulerAngles.z);
                //transform.Rotate(new Vector3( 0, parent.transform.rotation.y, 0));
            }
            if (position)
                transform.position = parent.transform.position;
        }
    }
}
