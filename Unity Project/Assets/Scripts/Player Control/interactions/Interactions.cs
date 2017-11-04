using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour {

    PlayerControlSettings settings;
    MonkayCommands commands;

    public Interactable currentInteractable;

    public Shader standard;
    public Shader outlined;

    public Interactable GetCurrentInter()
    {
        return currentInteractable;
    }

    // Use this for initialization
    void Start () {
        settings = Camera.main.GetComponent<PlayerControlSettings>();
        commands = Camera.main.GetComponent<MonkayCommands>();
    }

    public void ChangeInter(Interactable inter, GameObject triggerer)
    {
        if (triggerer == commands.currentChar.character.gameObject)
        {
            ChangeShader(currentInteractable, standard);
            currentInteractable = inter;
            ChangeShader(currentInteractable, outlined);
        }
    }

    void ChangeShader(Interactable obj, Shader shader)
    {
        if (obj)
        {
            obj.GetComponent<MeshRenderer>().material.shader = shader;
        }
    }

    // Update is called once per frame
}
