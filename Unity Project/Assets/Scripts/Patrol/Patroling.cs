using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroling : MonoBehaviour
{

    public List<Transform> points;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;

    public GameObject test;
    public GameObject eyesRef;
    public GameObject suspiciousObject;

    public enum State
    {
        patroling,
        suspicious,
        chasing
    }
    public State state;

    float timer;
    public float chasingDur;
    public float suspiciousDur;

    GameObject susObj;

    void ChangeTarget(Transform target)
    {
        if (target.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>())
            target.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().Chase(false);

        ai.target = target;

        if (target.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>())
            target.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().Chase(true);
    }

    void ChangeState(State state)
    {
        if (susObj)
            Destroy(susObj);

        switch (this.state)
        {
            case State.suspicious:
                break;
        }

        this.state = state;
    }

    public void Chase(GameObject target)
    {
        ChangeState(State.chasing);
        ChangeTarget(target.transform);
        ai.moveSpeed = 1;
        timer = Time.time + chasingDur;
    }

    // Use this for initialization
    void Start()
    {
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        ChangeTarget(points[0]);
        ai.moveSpeed = 0.5f;
        state = State.patroling;
    }

    public void Suspicious()
    {
        ChangeState(State.suspicious);
        susObj = Instantiate(suspiciousObject, ai.target.transform.position, Quaternion.identity);
        ChangeTarget(susObj.transform.GetChild(0));
        timer = Time.time + suspiciousDur;
        ai.moveSpeed = 0.5f;

    }

    public void ChangePatrolPoint()
    {
        points.Add(points[0]);
        points.RemoveAt(0);
        ChangeTarget(points[0]);
        ai.moveSpeed = 0.5f;
        ChangeState(State.patroling);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < Time.time)
        {
            switch (state)
            {
                case State.patroling:
                    if (Vector3.Distance(transform.position, points[0].position) < 0.1f)
                    {
                        ChangePatrolPoint();
                    }
                    break;

                case State.chasing:
                    Suspicious();
                    break;

                case State.suspicious:
                    ChangePatrolPoint();
                    break;
            }

        }

    }

    public void DoISeeIt(Collider body)
    {
        RaycastHit ray;
        Vector3 myPose = new Vector3(eyesRef.transform.position.x, body.transform.position.y + 1, eyesRef.transform.position.z);
        if (Physics.Raycast(myPose, (body.transform.position + new Vector3(0, 1, 0) - myPose), out ray))
        {
            if (ray.collider == body)
            {
                Chase(body.gameObject);
            }
        }
    }

    void OnTriggerStay(Collider body)
    {
        switch (body.tag)
        {
            //case "Player":
            //    DoISeeIt(body);
            //    break;

            //case "monkay":
            //    if (ai.target.tag != "Player")
            //        DoISeeIt(body);
            //    break;

            case "sound":
                if (state == State.patroling)
                {
                    Suspicious();
                }
                if (state == State.suspicious)
                {
                    ai.target.transform.position = body.transform.position;
                }
                break;

        }
    }
}

