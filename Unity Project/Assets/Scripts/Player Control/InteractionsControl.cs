using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsControl : MonoBehaviour {

    PlayerControlSettings settings;
    Interactions inters;

    Interactable inHand;

    void Start()
    {
        settings = GetComponent<PlayerControlSettings>();
        inters = GetComponent<Interactions>();
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
                }
            }
        }
    }
}
