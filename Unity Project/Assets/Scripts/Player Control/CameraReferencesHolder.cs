using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReferencesHolder : MonoBehaviour {
    public GameObject cameraPose;
    public GameObject cameraPoseFirstPerson;
    public GameObject cameraLookAt;
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl character;
    public GameObject monkayOnBackPoseRef;
    public Attacher attacher;

    public SkinnedMeshRenderer rend;

    public bool Enabled()
    {
        return rend.gameObject.active;
    }

    public bool Visible()
    {
        return rend.isVisible;
    }
}
