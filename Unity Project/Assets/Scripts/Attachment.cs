using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour {

    public Transform toAttache;
    public Transform toBeAttacheTo;
    Transform toAttacheOrigin;

    public void Attache()
    {
        toAttache.transform.parent = toBeAttacheTo;
    }

    public void DisAttache()
    {
        toAttache.transform.parent = toAttacheOrigin;
    }

    // Use this for initialization
    void Start () {
        toAttacheOrigin = toAttache.transform.parent;
	}

}
