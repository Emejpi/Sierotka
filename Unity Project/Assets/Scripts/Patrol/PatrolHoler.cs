using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolHoler : MonoBehaviour {

    List<PatrolPortal> patrolPortlas;

    void Start()
    {
        patrolPortlas = new List<PatrolPortal>();
    }

    public void HideFieldsOfView()
    {
        foreach (PatrolPortal port in patrolPortlas)
                port.fieldOfView.enabled = false;
    }

    public void ShowFieldsOfView(Vector3 startPoint)
    {
        HideFieldsOfView();
        RaycastHit hit = new RaycastHit();
        foreach (PatrolPortal port in patrolPortlas)
            if (port.body.isVisible )
                //&& Physics.Raycast(startPoint,  port.transform.position - startPoint, out hit)
                //&& hit.collider.gameObject == port.gameObject)
                port.fieldOfView.enabled = true;
        //print(hit.collider.name);
    }

    public void AddPatrol(PatrolPortal portal)
    {
        patrolPortlas.Add(portal);
    }
}
