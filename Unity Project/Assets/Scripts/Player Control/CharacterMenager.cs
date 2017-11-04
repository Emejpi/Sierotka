using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenager : MonoBehaviour {
    public GameObject cameraPose;
    public GameObject cameraPoseFirstPerson;
    public GameObject cameraLookAt;
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl character;
    public GameObject monkayOnBackPoseRef;
    public Attacher attacher;

    public SkinnedMeshRenderer rend;

    float timer;
    bool actionOnTimer;

    bool hiding;

    void Start()
    {
        hiding = false;
    }

    public void PlayAnim(string name, float dur, bool activateControlWhenDone)
    {
        character.GetComponent<Animator>().CrossFade(name, dur);

        if (activateControlWhenDone)
        {
            actionOnTimer = true;
            timer = Time.time + dur + 0.5f;
        }
    }

    public void PlayAnim(float dur, bool activateControlWhenDone)
    {
        PlayAnim("Grounded", dur, activateControlWhenDone);
    }

    public void LookAt(GameObject obj)
    {
        character.GetComponent<LookAtMe>().LookAt(obj);
    }

    public void Follow(GameObject obj, bool selfDis)
    {
        if(!selfDis)
            character.GetComponent<FollowMe>().Follow(obj);
        else
            character.GetComponent<FollowMe>().GoTo(obj);
    }

    public bool IsControledByPLayer()
    {
        return character.enabled;
    }

    public void Control(bool control, bool camera)
    {
        character.enabled = control && !hiding;
        //if (character == monkay.gameObject)
        //    ChangeState(character, MonkayState.waiting);
        //character.GetComponent<CameraReferencesHolder>().character.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled = enabled;
        cameraLookAt.SetActive(camera);
    }

    public void Hide(bool hide, string anim, Transform goTo, float animDur)
    {
        hiding = hide;

        Control(false, true);

        Coliders(false);
        Follow(hide ? goTo.gameObject : null, hide);
        LookAt(hide ? goTo.gameObject : null);
        if (hide)
            PlayAnim(anim, animDur, false);
        else
            PlayAnim(animDur, true);
    }

    public void Coliders(bool enable)
    {
        character.GetComponent<CapsuleCollider>().enabled = enable;
        character.GetComponent<Rigidbody>().useGravity = false;
    }

    public bool Enabled()
    {
        return rend.gameObject.active;
    }

    public bool Visible()
    {
        return rend.isVisible;
    }

    void Update()
    {
        if(actionOnTimer && timer < Time.time)
        {
            Coliders(true);
            Control(true, true);
            actionOnTimer = false;
        }
    }
}
