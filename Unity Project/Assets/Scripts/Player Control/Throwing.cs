using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour {

    Interactable inHand;
    PlayerControlSettings settings;
    CameraSettings cameraSettings;

    public float maxDistance;
    public float currentDistance;

    void Start()
    {
        settings = GetComponent<CharakterPortal>().settings;
        cameraSettings = transform.parent.GetComponent<CameraSettings>();
    }

    public void Grab(Interactable inter)
    {
        if (inHand)
            return;

        Transform rightHand = GetComponent<CharakterPortal>().rightHand;
        inter.transform.position = rightHand.position;
        inter.transform.parent = rightHand;
        inter.GetComponent<SphereCollider>().enabled = false;
        inHand = inter;
    }

    float GetThrowDistance()
    {
        float value = 0;
        float cameraRot = -settings.gameObject.transform.rotation.x * 100;
        Vector2 maxRotRange = new Vector2(-cameraSettings.maxRotationAngleUp / 2, cameraSettings.maxRotationAngleDown / 2);

        print("camera " + cameraRot);

        print("cur " + (cameraRot + cameraSettings.maxRotationAngleUp / 2));

        print("max " + ((cameraSettings.maxRotationAngleUp + cameraSettings.maxRotationAngleDown) / 2));

        value = maxDistance * ((cameraRot + cameraSettings.maxRotationAngleUp / 2) )
            / ((cameraSettings.maxRotationAngleUp + cameraSettings.maxRotationAngleDown) / 2);

        return value;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(settings.interact))
        {
            currentDistance = GetThrowDistance();
        }
	}
}
