using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacher : MonoBehaviour {

    List<Attachment> attachments;

	// Use this for initialization
	void Start () {
        attachments = new List<Attachment>();

		for(int i = 0; i < transform.childCount; i++)
        {
            attachments.Add(transform.GetChild(i).GetComponent<Attachment>());
        }
	}

    public void Attache()
    {
        foreach(Attachment atta in attachments)
        {
            atta.Attache();
        }
    }

    public void DisAttache()
    {
        foreach (Attachment atta in attachments)
        {
            atta.DisAttache();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
