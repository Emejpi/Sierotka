﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkayCommands : MonoBehaviour {

    PlayerControlSettings settings;

    public CameraReferencesHolder orphan;
    public CameraReferencesHolder monkay;

    public CameraReferencesHolder currentChar;

    public TextMesh text;

    float timer;
    public float jumpOnBackCutSceneDuration;

    public GameObject jumpOnBackCutSceneCamera;

    public float switchCharactersCameraSpeed;
    public float switchCharactersCameraSpeedAcceleration;
    public float switchCharactersLookAtAcceleration;
    float currSChCSA;
    float currSChLAtA;
    bool switchingChars;

    public float maxDistanceFromPlayerOnBackCommand;

    UnityStandardAssets.Characters.ThirdPerson.AICharacterControl monkayAI;

    public GameObject monkayTarget;

    List<CircleOption> options;

    public enum MonkayState
    {
        waiting,
        following,
        going,
        running,
        onBack,
        controled
    }
    public MonkayState state;

    public DirectionTarget directionTarget;

    public float distanceFromTargetTriggeringBaseSpeed;

    public CircleSelect circleSelect;

    bool monkayAttachedToBack;

    float returnToNormalAfterBackTimer;

    public float GetDistanceFormDirectionTarget()
    {
        return Vector3.Distance(directionTarget.transform.position, monkay.character.transform.position);
    }

    public bool IsOrphanActive()
    {
        return orphan == currentChar;
    }

    public void SwitchCharacters()
    {
        if (currentChar.GetComponent<CameraSettings>().slowCharacterSwitch)
        {
            SlowDownCamera(switchCharactersCameraSpeed);
            //timer = Time.time + timerDur;
        }

        if (currentChar == orphan)
        {
            GetComponent<PlayerSkills>().circleSelect.UnclockAllOptions(false);
            ChangeToChar(monkay.gameObject);
            ChangeState(monkay.gameObject, MonkayState.controled);
        }
        else
        {
            GetComponent<PlayerSkills>().circleSelect.UnclockAllOptions(true);
            ChangeToChar(orphan.gameObject);
            ChangeState(monkay.gameObject, MonkayState.waiting);
        }
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
        //if (character == monkay.gameObject)
        //    ChangeState(character, MonkayState.waiting);
        //character.GetComponent<CameraReferencesHolder>().character.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled = enabled;
        character.GetComponent<CameraReferencesHolder>().cameraLookAt.SetActive(enable);
    }

    public void ChangeState(GameObject character, MonkayState state)
    {

        character.GetComponent<CameraReferencesHolder>()
            .character.EnableAI(state == MonkayState.waiting || state == MonkayState.controled? false : true);

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

    public void SlowDownCamera(float speed)
    {
        GetComponent<LookAtMe>().speedMod = 1000;
        GetComponent<FollowMe>().speed = speed;
        switchingChars = true;
    }

    // Use this for initialization
    void Start () {
        monkayAttachedToBack = false;

        settings = GetComponent<PlayerControlSettings>();

        currSChCSA = switchCharactersCameraSpeedAcceleration;
        currSChLAtA = switchCharactersLookAtAcceleration;

        monkayAI = monkay.character.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();

        switchingChars = false;
        currentChar = orphan;

        SwitchCharacters();
        SwitchCharacters();

        ChangeState(monkay.gameObject, MonkayState.following);
    }

    public void CharacterVisible(CameraReferencesHolder characterHold, bool visible, bool controlChange)
    {
        for (int i = 0; i < characterHold.character.transform.childCount; i++)
        {
            characterHold.character.transform.GetChild(i).gameObject.SetActive(visible);
        }
        characterHold.character.GetComponent<WalkSounderWithUser>().enabled = visible;

        if (characterHold == monkay)
        {
            characterHold.character.GetComponent<MonkayGoCrazy>().enabled = visible;
            characterHold.character.GetComponent<CapsuleCollider>().enabled = visible;
        }

        if(controlChange)
        {
            characterHold.character.SetActive(visible);
        }
    }

    void MonkayVisible(bool visible)
    {
        CharacterVisible(monkay, visible, false);
    }

    public void Wait()
    {
        ChangeState(monkay.gameObject, MonkayState.waiting);
        text.text = "WAIT";
    }

    public void FollowMe()
    {
        if (!monkay.Visible())
        {
            monkay.character.transform.position = transform.position - transform.forward * 5;
        }
        ChangeState(monkay.gameObject, MonkayState.following);
        text.text = "COME HERE";
    }

    public void GO()
    {
        directionTarget.Go(transform);
        ChangeState(monkay.gameObject, MonkayState.going);
        text.text = "GO!";
    }

    //public void Hide

    public void OnBack()
    {
        //jumpOnBackCutSceneCamera.SetActive(true);
        //timer = Time.time + jumpOnBackCutSceneDuration;

        if (state == MonkayState.onBack)
        {
            monkay.character.GetComponent<Animator>().CrossFade("Grounded", 1);

            ChangeState(monkay.gameObject, MonkayState.following);
            DisenableEverythingMonkay(true);

            monkay.character.GetComponent<LookAtMe>().LookAt(orphan.character.gameObject);
            monkay.character.GetComponent<FollowMe>().Follow(null);

            DetachFromBack();

            returnToNormalAfterBackTimer = Time.time + 0.5f;

            monkay.character.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else
        {
            monkay.character.GetComponent<Animator>().CrossFade("onBack", 1);
            //MonkayVisible(!monkay.character.GetComponent<CapsuleCollider>().enabled);
            ChangeState(monkay.gameObject, MonkayState.onBack);
            DisenableEverythingMonkay(false);

            monkay.character.GetComponent<LookAtMe>().LookAt(orphan.monkayOnBackPoseRef);
            monkay.character.GetComponent<FollowMe>().Follow(orphan.monkayOnBackPoseRef);

            monkay.character.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //monkay.character.m_Character.Move(new Vector3(0, 0, 0), false, false);
        }
    }

    void DisenableEverythingMonkay(bool enable)
    {
        monkay.character.EnableAI(false);
        //monkay.character.SetActive(enable);
        monkay.character.enabled = !enable;
        monkay.character.GetComponent<Rigidbody>().useGravity = enable;
        monkay.character.GetComponent<Collider>().enabled = enable;
    }

    public void Yell()
    {
        currentChar.character.GetComponent<SoundMaker>().Sound(100);
    }

    void AttachToBack()
    {
        monkayAttachedToBack = true;
        monkay.attacher.Attache();

        //monkay.character.GetComponent<LookAtMe>().LookAt(null);
        monkay.character.GetComponent<FollowMe>().Follow(null);
    }

    void DetachFromBack()
    {
        //monkayAttachedToBack = false;
        monkay.attacher.DisAttache();
    }

	// Update is called once per frame
	void Update () {

        if (switchingChars)
        {
            //if (timer < Time.time)
            {
                GetComponent<FollowMe>().speed += Time.deltaTime * currSChCSA;
                GetComponent<LookAtMe>().speedMod -= Time.deltaTime * currSChLAtA;
                if (GetComponent<FollowMe>().DistanceFormTarget() < distanceFromTargetTriggeringBaseSpeed 
                    && currentChar.character.GetSoundStrenght() > 0.2f)
                {
                    currSChCSA *= 2f;
                    currSChLAtA *= 2f;
                }
                if (GetComponent<FollowMe>().StartSpeedIfOverStart())
                {
                    currSChCSA = switchCharactersCameraSpeedAcceleration;
                    currSChLAtA = switchCharactersLookAtAcceleration;

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

        //COMMANDS
        circleSelect.UnclockAllOptions(false);

        circleSelect.GetOption(CircleSelect.Action.yell).Unlock(true);

        if (currentChar == orphan)
        {

                if (!monkay.character.IsBeingChased()) //WAIT
                {
                    circleSelect.GetOption(CircleSelect.Action.wait).Unlock(true);
                }
                //if (Input.GetKeyDown(settings.followMe)) //TO ME
                
                    circleSelect.GetOption(CircleSelect.Action.follow).Unlock(true);
                
                //if (Input.GetKeyDown(settings.go)) //GO
                
                    circleSelect.GetOption(CircleSelect.Action.go).Unlock(true);

            if (state != MonkayState.onBack)
            {

                if (monkayAttachedToBack && returnToNormalAfterBackTimer < Time.time) //RETURN TO NORMAL
                {
                    monkay.character.EnableAI(true);
                    monkay.character.GetComponent<LookAtMe>().LookAt(null);
                    monkayAttachedToBack = false;
                }
            }
            else // MONKAY ON BACK
            {
                if (!monkayAttachedToBack &&
                    Vector3.Distance(monkay.character.transform.position, orphan.monkayOnBackPoseRef.transform.position) < 0.1f)
                {
                    AttachToBack();
                }
            }

            if ((state == MonkayState.following || state == MonkayState.waiting || state == MonkayState.onBack)
                && !monkay.character.IsBeingChased() 
                && !orphan.character.IsBeingChased() 
                && Vector3.Distance(monkay.character.transform.position, orphan.character.transform.position) 
                < maxDistanceFromPlayerOnBackCommand
                && orphan.character.GetComponent<Rigidbody>().velocity.magnitude < 1f)
            {
                circleSelect.GetOption(CircleSelect.Action.onBack).Unlock(true);

                //if (state == MonkayState.following
                //    && orphan.character.GetComponent<Rigidbody>().velocity.magnitude < 1f)
                //    OnBack();
            }

            if (jumpOnBackCutSceneCamera.active && timer < Time.time)
            {
                jumpOnBackCutSceneCamera.SetActive(false);
                if(state != MonkayState.onBack)
                    MonkayVisible(!monkay.character.GetComponent<CapsuleCollider>().enabled);
            }

        }
        if (state != MonkayState.onBack
            && (!orphan.character.IsBeingChased())
                || (orphan.character.IsBeingChased() && currentChar != orphan)) // Switch
        {
            circleSelect.GetOption(CircleSelect.Action.switchChar).Unlock(true);
        }

    }
}
