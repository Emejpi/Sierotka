using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPortal : MonoBehaviour {

    public GameObject orange;  

    public void Freeze(bool freeze)
    {
        GetComponent<FollowMe>().target.transform.parent.GetComponent<SimpleMouseRotator>().enabled = !freeze;
    }

    // Use this for initialization
    void Start()
    {
        orange = GameObject.Find("orange");
    }
}
