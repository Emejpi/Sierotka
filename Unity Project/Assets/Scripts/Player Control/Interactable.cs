using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public enum Interaction
    {
        pickable
    }
    public Interaction inter;

    public Interaction GetInteraction()
    {
        return inter;
    }
}
