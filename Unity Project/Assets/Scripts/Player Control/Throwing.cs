using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour {

    Interactable inHand;
    PlayerControlSettings settings;
    CameraSettings cameraSettings;

    public float maxDistance;
    public float currentDistance;

    public GameObject throwingDestination;

    public float basePower;
    public float distancePowerMultiplayer;

    bool hadItInHandOnClicking = false;

    public float firstPersonSwitchSpeed;

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
        inter.GetComponent<Collider>().enabled = false;
        inHand = inter;

        inHand.GetComponent<Rigidbody>().isKinematic = true;
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
            if (inHand)
            {
                GetComponent<CharakterPortal>().settings.GetComponent<MonkayCommands>().SlowDownCamera(firstPersonSwitchSpeed);
                GetComponent<CharakterPortal>().settings.GetComponent<FollowMe>().target = transform.parent.GetComponent<CameraReferencesHolder>().cameraPoseFirstPerson;
                hadItInHandOnClicking = true;
                throwingDestination.GetComponent<MeshRenderer>().enabled = true;

                GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().moveForward = true;
            }
            else
                hadItInHandOnClicking = false;
        }
        if (hadItInHandOnClicking)
        {
            if (Input.GetKey(settings.interact))
            {
                currentDistance = GetThrowDistance();
                throwingDestination.transform.position = transform.position + transform.forward * currentDistance;
            }
            else if (Input.GetKeyUp(settings.interact))
            {
                throwingDestination.GetComponent<MeshRenderer>().enabled = false;

                GetComponent<CharakterPortal>().settings.GetComponent<FollowMe>().target = transform.parent.GetComponent<CameraReferencesHolder>().cameraPose;

                GetComponent<CharakterPortal>().settings.GetComponent<MonkayCommands>().SlowDownCamera(firstPersonSwitchSpeed/10);

                GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().moveForward = false;

                inHand.transform.parent = null;
                inHand.GetComponent<Rigidbody>().isKinematic = false;
                inHand.gameObject.AddComponent<ActivateColliderWithTimer>();
                //currentDistance * distancePowerMultiplayer + basePower * (1 - currentDistance / maxDistance)
                float initialVal = 1.0f;
                while (!ThrowBallAtTargetLocation(throwingDestination.transform.position, initialVal))
                {
                        print("va" + initialVal);
                        initialVal += 0.5f;
                }
                inHand = null;
            }
        }
    }

    public bool ThrowBallAtTargetLocation(Vector3 targetLocation, float initialVelocity)
    {
        Vector3 direction = (targetLocation - transform.position).normalized;
        float distance = Vector3.Distance(targetLocation, transform.position);

        float firingElevationAngle = FiringElevationAngle(Physics.gravity.magnitude, distance, initialVelocity);
        Vector3 elevation = Quaternion.AngleAxis(firingElevationAngle, transform.right) * transform.up;
        float directionAngle = AngleBetweenAboutAxis(transform.forward, direction, transform.up);
        Vector3 velocity = Quaternion.AngleAxis(directionAngle, transform.up) * elevation * initialVelocity;

        print(velocity);

        print(velocity.magnitude);

        // ballGameObject is object to be thrown
        if (System.Single.IsNaN(velocity.magnitude))
            return false;

        inHand.GetComponent<Rigidbody>().velocity = velocity;
        return true;
    }

    // Helper method to find angle between two points (v1 & v2) with respect to axis n
    public static float AngleBetweenAboutAxis(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    // Helper method to find angle of elevation (ballistic trajectory) required to reach distance with initialVelocity
    // Does not take wind resistance into consideration.
    private float FiringElevationAngle(float gravity, float distance, float initialVelocity)
    {
        float angle = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
        return angle;
    }
}
