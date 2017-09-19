using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionZoomer : MonoBehaviour
{
    float distance = 16;
    float distanceCurent;
    float speed;
    public float rayDistance = 6;

    public Transform rayPoint;

    public CameraSettings set;

    // Use this for initialization
    void Start()
    {
        distance = set.distanceFromCharacter;
        speed = set.zoomingSpeed;
        distanceCurent = distance;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayPoint.position + transform.forward * 3, transform.TransformDirection(-Vector3.forward), out hit, rayDistance))
        {
            distanceCurent = hit.distance + 5;
        }
        else
        {
            distanceCurent = distance;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Lerp(transform.localPosition.z, - distanceCurent, speed * Time.deltaTime));
    }
}
