using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public enum Interaction
    {
        pickable,
        hide,
        medalionTrigger,
        food
    }
    public Interaction inter;

    public Transform animStartPose;
    public string anim;

    public int count;

    public Interaction GetInteraction()
    {
        return inter;
    }
}
