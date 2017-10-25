using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingSeen : MonoBehaviour
{
    public Interactions inters;

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider body)
    {
        switch (body.tag)
        {
            case "interactable":
                inters.ChangeInter(body.gameObject.GetComponent<Interactable>(), gameObject);
                break;

            case "medalion trigger":
                if (GetComponent<MedalionTriggersHolder>())
                    GetComponent<MedalionTriggersHolder>().AddTrigger(body.gameObject);
                break;
        }
    }

    void OnTriggerExit(Collider body)
    {
        switch (body.tag)
        {
            case "interactable":
                if (inters.currentInteractable == body.gameObject.GetComponent<Interactable>())
                    inters.ChangeInter(null, gameObject);
                break;

            case "medalion trigger":
                if (GetComponent<MedalionTriggersHolder>())
                    GetComponent<MedalionTriggersHolder>().RemoveTrigger(body.gameObject);
                break;
        }
    }


    void OnTriggerStay(Collider body)
    {
        switch (body.tag)
        {
            case "patrol":
                switch (tag)
                {
                    case "Player":
                        body.GetComponent<Seeing>().patrol.DoISeeIt(GetComponent<CapsuleCollider>());
                        break;

                    case "monkay":
                        if (body.GetComponent<Seeing>().patrol.ai.target.tag != "Player")
                            body.GetComponent<Seeing>().patrol.DoISeeIt(GetComponent<CapsuleCollider>());
                        break;


                }
                break;
        }
    }
}
