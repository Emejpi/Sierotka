using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPortal : MonoBehaviour {

    public GameObject orange;

    // Use this for initialization
    void Start()
    {
        orange = GameObject.Find("orange");
    }
}
