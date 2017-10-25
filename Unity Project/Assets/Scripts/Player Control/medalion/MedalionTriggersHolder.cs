using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalionTriggersHolder : MonoBehaviour {

    List<GameObject> medalionTriggers;

    public Wiggle medalion;

    public void AddTrigger(GameObject trigger)
    {
        trigger.GetComponent<FloatHolder>()
            .SetValue(Vector3.Distance(transform.position, trigger.transform.position));

        medalionTriggers.Add(trigger);
    }

    public void RemoveTrigger(GameObject trigger)
    {
        medalionTriggers.Remove(trigger);
    }

    // Use this for initialization
    void Start () {
        medalionTriggers = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		if(medalionTriggers.Count > 0)
        {
            GameObject closerTrigger = medalionTriggers[0];

            for(int i = 1; i < medalionTriggers.Count; i++)
            {
                if (Vector3.Distance(closerTrigger.transform.position, transform.position) >
                    Vector3.Distance(medalionTriggers[i].transform.position, transform.position))
                {
                    closerTrigger = medalionTriggers[i];
                }
            }

            medalion.SetPercentRange(closerTrigger.GetComponent<FloatHolder>().GetValue(),
                Vector3.Distance(closerTrigger.transform.position, transform.position));

        }
	}
}
