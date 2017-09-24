using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkayCommands : MonoBehaviour {

    public CameraReferencesHolder orphan;
    public CameraReferencesHolder monkay;

    public CameraReferencesHolder currentChar;

    public TextMesh text;

    float timer;
    public float timerDur;
    public float switchCharactersCameraSpeed;
    public float switchCharactersCameraSpeedAcceleration;
    public float switchCharactersLookAtAcceleration;
    bool switchingChars;

    UnityStandardAssets.Characters.ThirdPerson.AICharacterControl monkayAI;

    public GameObject monkayTarget;

    public enum MonkayState
    {
        waiting,
        following,
        going,
        running
    }
    MonkayState state;

    public DirectionTarget directionTarget;

    void SwitchCharacters()
    {
        if (currentChar.GetComponent<CameraSettings>().slowCharacterSwitch)
        {
            GetComponent<LookAtMe>().speedMod = 1000;
            GetComponent<FollowMe>().speed = switchCharactersCameraSpeed;
            switchingChars = true;
            timer = Time.time + timerDur;
        }

        if (currentChar == orphan)
            ChangeToChar(monkay.gameObject);
        else
            ChangeToChar(orphan.gameObject);
    }

    void ChangeToChar(GameObject character)
    {
        EnableCharacter(currentChar.gameObject, false);
        currentChar.character.m_Character.Move(new Vector3(0, 0, 0), false, false);

        LookAt(character);
        Follow(character);

        EnableCharacter(character, true);
        currentChar = character.GetComponent<CameraReferencesHolder>();
    }

    void EnableCharacter(GameObject character, bool enable)
    {
        character.GetComponent<CameraReferencesHolder>().character.enabled = enable;
        if (character == monkay.gameObject)
            ChangeState(character, MonkayState.waiting);
        //character.GetComponent<CameraReferencesHolder>().character.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled = enabled;
        character.GetComponent<CameraReferencesHolder>().cameraLookAt.SetActive(enable);
    }

    public void ChangeState(GameObject character, MonkayState state)
    {

        character.GetComponent<CameraReferencesHolder>()
            .character.EnableAI(state == MonkayState.waiting? false : true);

        this.state = state;

        if (state != MonkayState.running)
            character.GetComponent<CameraReferencesHolder>().character.Chase(false);

        switch(state)
        {
            case MonkayState.following:
                monkayAI.target = monkayTarget.transform;
                break;

            case MonkayState.going:
                monkayAI.target = directionTarget.transform;
                break;
        }
    }

    void LookAt(GameObject character)
    {
        GetComponent<LookAtMe>().LookAt(character.GetComponent<CameraReferencesHolder>().cameraLookAt);
    }

    void Follow(GameObject character)
    {
        GetComponent<FollowMe>().Follow(character.GetComponent<CameraReferencesHolder>().cameraPose);
    }

    void PlotkaPort()
    {

    }

    // Use this for initialization
    void Start () {
        monkayAI = monkay.character.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();

        switchingChars = false;
        currentChar = orphan;

        SwitchCharacters();
        SwitchCharacters();

        ChangeState(monkay.gameObject, MonkayState.following);
    }
	
	// Update is called once per frame
	void Update () {
        if (switchingChars)
        {
            //if (timer < Time.time)
            {
                GetComponent<FollowMe>().speed += Time.deltaTime * switchCharactersCameraSpeedAcceleration;
                GetComponent<LookAtMe>().speedMod -= Time.deltaTime * switchCharactersLookAtAcceleration; ;
                if (GetComponent<FollowMe>().StartSpeedIfOverStart())
                {
                    GetComponent<LookAtMe>().speedMod = 0;
                    switchingChars = false;
                }

            }
        }

        if (currentChar == orphan)
        {
            if(state == MonkayState.waiting)
                monkay.character.m_Character.Move(new Vector3(0, 0, 0), false, false);
        }
        else
        {
            orphan.character.m_Character.Move(new Vector3(0, 0, 0), false, false);
        }
        if (currentChar == orphan)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !monkay.character.IsBeingChased()) //WAIT
            {
                ChangeState(monkay.gameObject, MonkayState.waiting);
                text.text = "WAIT";
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) //TO ME
            {
                ChangeState(monkay.gameObject, MonkayState.following);
                text.text = "COME HERE";
            }
            if (Input.GetKeyDown(KeyCode.E)) //TO ME
            {
                directionTarget.Go(transform);
                ChangeState(monkay.gameObject, MonkayState.going);
                text.text = "GO!";
            }
        }
        if ((Input.GetKeyDown(KeyCode.Alpha3) && !orphan.character.IsBeingChased())
                || (orphan.character.IsBeingChased() && currentChar != orphan)) // Switch
        {
            text.text = "";
            SwitchCharacters();
        }

    }
}
