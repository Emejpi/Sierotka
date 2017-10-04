using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour {

    PlayerControlSettings settings;
    MonkayCommands commands;

    float timer;

	// Use this for initialization
	void Start () {
        settings = GetComponent<PlayerControlSettings>();
        commands = GetComponent<MonkayCommands>();
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (commands.IsOrphanActive())
        {
            if (Input.GetKey(settings.showFieldsOfView) && timer < Time.time)
            {
                GetComponent<CameraPortal>().orange.GetComponent<PatrolHoler>()
                    .ShowFieldsOfView(commands.orphan.character.transform.position);
                timer = Time.time + 1;
            }
            else if (Input.GetKeyUp(settings.showFieldsOfView))
            {
                GetComponent<CameraPortal>().orange.GetComponent<PatrolHoler>().HideFieldsOfView();
            }
        }
    }
}
