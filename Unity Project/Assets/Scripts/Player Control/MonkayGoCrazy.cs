using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkayGoCrazy : MonoBehaviour {

    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl user;
    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character;
    UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;

    public MonkayCommands commands;

    bool crazy;
    public GameObject randomPositioner;

    GameObject currentRandomPositioner;

    float timer;

    void ChangeCurrentRandomPositioner(GameObject rand)
    {
        if(currentRandomPositioner)
        {
            Destroy(currentRandomPositioner);
        }
        currentRandomPositioner = rand;
    }

    // Use this for initialization
    void Start () {
        user = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>();
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        character = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
        crazy = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(user.IsBeingChased())
        {
            if(!crazy)
            { 
                ChangeCurrentRandomPositioner(Instantiate(randomPositioner, transform.position, Quaternion.identity));
                ai.target = currentRandomPositioner.transform.GetChild(0);
                commands.ChangeState(gameObject.transform.parent.gameObject ,MonkayCommands.MonkayState.running);
                crazy = true;
            }
            else if(timer < Time.time && character.GetSoundStrenght() < 0.2f)
            {
                ai.target.GetComponent<MonkayTargetChanger>().Change();
                currentRandomPositioner.transform.position = transform.position;
                timer = Time.time + 0.3f;
            }
        }
        else
        {
            crazy = false;
        }
	}
}
