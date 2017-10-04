using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPortal : MonoBehaviour {

    public Patroling patroling;
    public MeshRenderer fieldOfView;
    public SkinnedMeshRenderer body;

    public PatrolHoler orange;

    void Start()
    {
        orange = GameObject.Find("orange").GetComponent<PatrolHoler>();
        patroling = GetComponent<Patroling>();
        orange.AddPatrol(this);
    }
}
