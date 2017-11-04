using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsControl : MonoBehaviour {

    PlayerControlSettings settings;
    Interactions inters;

    Interactable inHand;

    MonkayCommands commands;

    void Start()
    {
        settings = GetComponent<PlayerControlSettings>();
        commands = GetComponent<MonkayCommands>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(settings.interact))
        {
            inters = commands.currentChar.character.GetComponent<Interactions>();
            Interactable inter = inters.GetCurrentInter();
            if (inter)
            {
                switch (inter.GetInteraction())
                {
                    case Interactable.Interaction.pickable:
                        if (!inHand)
                        {
                            GetComponent<MonkayCommands>().currentChar.character.GetComponent<Throwing>().Grab(inter);
                        }
                        break;

                    case Interactable.Interaction.hide:
                        bool hide = commands.currentChar.IsControledByPLayer();
                        commands.currentChar.Hide(hide, inter.anim, inter.animStartPose, 1);
                        //commands.currentChar.
                        //commands.currentChar
                        break;

                    case Interactable.Interaction.food:
                        GetComponent<CameraPortal>().orange.reputation.LoseIfSaveZone();

                        GetComponent<PlayerSkills>().hungerBar.AddFill((float)inter.count / 100.0f);
                        Destroy(inter.gameObject);
                        break;
                }
            }
        }
    }
}
