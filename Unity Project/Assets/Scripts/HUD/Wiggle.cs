using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour {

    RectTransform trans;
    Lerper lerper;

    [Header("Rotation")]
    public bool rotation = true;
    public Vector2 rotationSpeedMinMax = new Vector2(0,20);
    public Vector2 rotationRangeMinMax = new Vector2(0, 50);
    float rotationSpeed = 1;
    float rotationRange = 1;
    float actualRotationRange;

    int rotateDir = 1;

    public float rotationMoveSharpnes = 5;
    public float rotationMoveSharpnesStoping = 1;

    public int wiggelsNumberAtOnes = 2;
    int actualWiggelsNumberAtOnes;
    int currentWiggle = 0;

    float timer;
    public Vector2 durationBetweenWigglesMinMax = new Vector2(0, 4);
    float durationBetweenWiggles;

    [Header("Vibration")]
    public bool vibration = true;

    public Vector2 vibrationRangeMinMax = new Vector2(0, 30);
    float moveRange;

    public Vector2 vibrationSpeedMinMax = new Vector2(0, 200);
    float moveSpeed;

    Vector2 curretnMoveDir;
    Vector2 startPose;

    float moveTimer;
    public float changeMoveDirectionTimerDur = 0.1f;

    [Range(0.0f, 100.0f)]
    public float rangeFrom;

    public void SetPercentRange(float maxDistance, float currentDistance)
    {
        rangeFrom = currentDistance * 100 / maxDistance;
    }

    void ValueFromRange(float percentRange, Vector2 minMax, out float value)
    {
        value = ((minMax.y - minMax.x) * percentRange / 100) + minMax.x;
    }

    // Use this for initialization
    void Start() {
        trans = GetComponent<RectTransform>();
        lerper = GetComponent<Lerper>();

        SetRangeValue();

        trans.GetChild(0).GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, -actualRotationRange / 2);

        startPose = trans.position;
    }

    void SetRangeValue()
    {
        ValueFromRange(100 - rangeFrom, vibrationRangeMinMax, out moveRange);
        ValueFromRange(100 - rangeFrom, vibrationSpeedMinMax, out moveSpeed);

        ValueFromRange(100 - rangeFrom, rotationRangeMinMax, out rotationRange);
        ValueFromRange(100 - rangeFrom, rotationSpeedMinMax, out rotationSpeed);
        ValueFromRange(rangeFrom, durationBetweenWigglesMinMax, out durationBetweenWiggles);

        actualRotationRange = rotationRange * 2;
        actualWiggelsNumberAtOnes = wiggelsNumberAtOnes * 2;


    }

    // Update is called once per frame
    void Update() {
        SetRangeValue();
        if (vibration)
        {
            if (moveTimer < Time.time)
            {
                moveTimer = Time.time + changeMoveDirectionTimerDur;

                curretnMoveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }

            trans.position = lerper.LerpVector3(trans.position,
                startPose + curretnMoveDir * moveRange, moveSpeed * Time.deltaTime);
        }
        trans.GetChild(0).GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, -actualRotationRange / 2);

        if (rotation)
        {
            if (timer < Time.time)
            {
                if (currentWiggle != actualWiggelsNumberAtOnes)
                {
                    trans.localEulerAngles =
                    new Vector3(0, 0, Mathf.Lerp(trans.eulerAngles.z,
                    actualRotationRange * rotateDir,
                    rotationSpeed * Time.deltaTime));

                    if (CloseEnought(trans.eulerAngles.z, actualRotationRange * rotateDir, rotationMoveSharpnes))
                    {
                        currentWiggle++;
                        rotateDir = 1 - rotateDir;
                    }
                }
                else
                {
                    trans.localEulerAngles =
                    new Vector3(0, 0, Mathf.Lerp(trans.eulerAngles.z,
                    actualRotationRange / 2,
                    rotationSpeed * Time.deltaTime));

                    if (CloseEnought(trans.eulerAngles.z, actualRotationRange / 2, rotationMoveSharpnesStoping))
                    {
                        currentWiggle = 0;
                        timer = Time.time + durationBetweenWiggles;
                    }
                }


            }
            else
            {
                trans.localEulerAngles = new Vector3(0, 0, actualRotationRange / 2);
            }
        }
    }

    bool CloseEnought(float value, float targetValue, float delta)
    {
        return value > targetValue - delta && value < targetValue + delta;
    }
}
