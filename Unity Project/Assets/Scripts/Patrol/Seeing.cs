using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeing : MonoBehaviour {

    public Patroling patrol;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider body)
    {
        switch (body.tag)
        {
            case "Player":
                patrol.DoISeeIt(body);
                break;

            case "monkay":
                if (patrol.ai.target.tag != "Player")
                    patrol.DoISeeIt(body);
                break;

                //case "sound":
                //    if(state == State.patroling)
                //    {
                //        Suspicious();
                //    }
                //    if(state == State.suspicious)
                //    {
                //        ai.target.transform.position = body.transform.position;
                //    }
                //    break;

        }
    }
}
