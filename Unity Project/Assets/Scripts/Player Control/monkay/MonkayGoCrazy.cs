using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkayGoCrazy : MonoBehaviour {

    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl user;
    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character;
    UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;

    public MonkayCommands commands;

    public bool crazy;
    public GameObject randomPositioner;
    bool checkingForNotMoveing;

    GameObject currentRandomPositioner;

    float timer;

    bool forceIn;
    MonkayCommands.MonkayState stateOnCrazy;

    void ChangeCurrentRandomPositioner(GameObject rand)
    {
        if (currentRandomPositioner)
        {
            Destroy(currentRandomPositioner);
        }
        currentRandomPositioner = rand;
    }

    // Use this for initialization
    void Start() {
        user = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>();
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        character = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
        crazy = false;
        checkingForNotMoveing = false;
        forceIn = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (commands.state != MonkayCommands.MonkayState.controled)
        {
            if ((commands.state == MonkayCommands.MonkayState.going && commands.GetDistanceFormDirectionTarget() > 20)
                || (user.IsBeingChased() && commands.state == MonkayCommands.MonkayState.waiting))
            {

                {
                    forceIn = true;
                    commands.ChangeState(gameObject.transform.parent.gameObject, MonkayCommands.MonkayState.following);
                }
            }

            if ((user.IsBeingChased() && (commands.state != MonkayCommands.MonkayState.going || character.GetSoundStrenght() < 0.1f)))
            {

                if (!crazy)
                {
                    ChangeCurrentRandomPositioner(Instantiate(randomPositioner, transform.position, Quaternion.identity));
                    ai.target = currentRandomPositioner.transform.GetChild(0);
                    commands.ChangeState(gameObject.transform.parent.gameObject, MonkayCommands.MonkayState.running);
                    crazy = true;
                }
                else if (timer < Time.time && character.GetSoundStrenght() < 0.2f)
                {
                    if (!ai.target.GetComponent<MonkayTargetChanger>())
                    {
                        crazy = false;
                    }
                    else
                    {
                        ai.target.GetComponent<MonkayTargetChanger>().Change();
                        currentRandomPositioner.transform.position = transform.position;
                        timer = Time.time + 0.15f;
                    }
                }
            }
            else
            {
                crazy = false;
            }
        }
    }
}
