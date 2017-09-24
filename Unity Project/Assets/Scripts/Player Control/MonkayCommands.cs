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

    bool waiting;

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
            Wait(character, true);
        //character.GetComponent<CameraReferencesHolder>().character.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled = enabled;
        character.GetComponent<CameraReferencesHolder>().cameraLookAt.SetActive(enable);
    }

    void Wait(GameObject character, bool enable)
    {
        character.GetComponent<CameraReferencesHolder>().character.EnableAI(!enable);
        waiting = enable;
    }

    void LookAt(GameObject character)
    {
        GetComponent<LookAtMe>().LookAt(character.GetComponent<CameraReferencesHolder>().cameraLookAt);
    }

    void Follow(GameObject character)
    {
        GetComponent<FollowMe>().Follow(character.GetComponent<CameraReferencesHolder>().cameraPose);
    }

    // Use this for initialization
    void Start () {
        switchingChars = false;
        currentChar = orphan;

        SwitchCharacters();
        SwitchCharacters();
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
            if(waiting)
                monkay.character.m_Character.Move(new Vector3(0, 0, 0), false, false);
        }
        else
        {
            orphan.character.m_Character.Move(new Vector3(0, 0, 0), false, false);
        }
        if (currentChar == orphan)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) //WAIT
            {
                Wait(monkay.gameObject, true);
                text.text = "WAIT";
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) //TO ME
            {
                Wait(monkay.gameObject, false);
                text.text = "COME HERE";
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) //
        {
            text.text = "";
            SwitchCharacters();
        }

    }
}
