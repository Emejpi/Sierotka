using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOption : MonoBehaviour {

    public enum SkillType
    {
        hold,
        timer,
        use
    }


    public int actionIndex;
    public CircleSelect.Action name;
    public float cost;
    public float timerDur;
    public SkillType type;
    public bool active;
    public GameObject body;
    public Vector2 pose;
    public Sprite sprite;

    public bool learned;

    public void Unlock(bool unlock)
    {
        if(learned)
            active = unlock;
    }

}
