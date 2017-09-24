using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDetector : MonoBehaviour
{
    Patroling patrol;

    void Start()
    {
        patrol = GetComponent<CustomParenting>().parent.GetComponent<Patroling>();
    }


    void OnTriggerEnter(Collider body)
    {
        switch (body.tag)
        {

            case "sound":
                if (patrol.state == Patroling.State.patroling)
                {
                    patrol.Suspicious();
                }
                if (patrol.state == Patroling.State.suspicious)
                {
                    patrol.ai.target.transform.position = body.transform.position;
                }
                break;

        }
    }
}
