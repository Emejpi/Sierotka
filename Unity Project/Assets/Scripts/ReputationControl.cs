using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationControl : MonoBehaviour {

    public Bar bar;

    public float reputation;

    public float badGate;
    public float veryBadGate;

    public bool saveZone;

    public float reputationLoseOnLowBreak;

    public enum State
    {
        good,
        bad,
        veryBad
    }
    public State state;
    public State GetState() { return state; }
        
    public void LoseIfSaveZone()
    {
        if(saveZone)
            Add(-reputationLoseOnLowBreak);
    }

    void Add(float value)
    {
        reputation += value;
        bar.AddFill((float)value / 100.0f);

        if(reputation < veryBadGate)
        {
            state = State.veryBad;
        }
        else if (reputation < badGate)
        {
            state = State.bad;
        }
        else
        {
            state = State.good;
        }
    }

    public float GetReputation()
    {
        return reputation;
    }

	// Use this for initialization
	void Start () {
        bar.baseValue = reputation / 100;
        Add(0);
        //bar.SetFill
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
