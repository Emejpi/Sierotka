using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSounderWithUser : MonoBehaviour {

    float timer;
    public float dur;
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timer)
        {
            GetComponent<SoundMaker>().Sound((int)GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().GetSoundStrenght());
            timer = Time.time + dur;
        }
	}
}
