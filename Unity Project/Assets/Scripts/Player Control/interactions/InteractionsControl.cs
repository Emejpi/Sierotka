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
        inters = GetComponent<Interactions>();
        commands = GetComponent<MonkayCommands>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(settings.interact))
        {
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
                        if(commands.currentChar == commands.orphan
                            && commands.orphan.Enabled()
                            && commands.monkay.Enabled())
                            commands.circleSelect.ExecuiteOption(
                                commands.circleSelect.GetOption(CircleSelect.Action.onBack));

                        commands.CharacterVisible(commands.currentChar, !commands.currentChar.Enabled(), true);
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
