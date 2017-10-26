using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSelect : MonoBehaviour {

    public enum Action
    {
        wait,
        follow,
        go,
        onBack,
        switchChar,
        yell,
        showFildsOfView,
        none,
        mute,
        shadowMonkay
    }

    public int mouseKeyNum;

    List<CircleOption> options;

    public GameObject optionPref;

    public GameObject optionsHolder;
    public GameObject optionsBodysHolder;

    public float circleRadius;

    public Vector3 mosePoseOnClick;

    public MonkayCommands commands;
    PlayerSkills skills;

    List<CircleOption> queueCommands;
    float timer;
    public float timeBetweenCommands;

    public bool EmptyQueue()
    {
        return queueCommands.Count == 0;
    }
    
    public void UnclockAllOptions(bool unlock)
    {
        foreach (CircleOption option in options)
        {
            option.Unlock(unlock);
        }
    }

    public CircleOption GetOption(Action name)
    {
        foreach (CircleOption option in options)
            if (option.name == name)
                return option;

        return options[0];
    }

    // Use this for initialization
    void Start () {
        queueCommands = new List<CircleOption>();

        skills = commands.GetComponent<PlayerSkills>();

        AddOptionsFromHolder();

        PleaceOptions();
	}

    void AddOptionsFromHolder()
    {
        options = new List<CircleOption>();

        for (int i = 0; i < optionsHolder.transform.childCount; i ++)
        {
            options.Add(optionsHolder.transform.GetChild(i).GetComponent<CircleOption>());
        }
    }
	
    CircleOption ChoseOptionBasedOnPosition()
    {
        Vector2 pose = Vector2.zero;

        if (Vector2.Distance(Input.mousePosition, mosePoseOnClick) < Screen.height / 6)
            pose = new Vector2(0, 0);
        else
            pose = (-mosePoseOnClick + Input.mousePosition).normalized;

        CircleOption curOption = options[0];
        float minDistance = 10;
        float curDistance;
        for(int i = 0; i < options.Count; i++)
        {
            curDistance = Vector2.Distance(options[i].pose, pose);
            if (curDistance < minDistance)
            {
                minDistance = curDistance;
                curOption = options[i];
            }
        }

        return curOption;
    }

    void PleaceOptions()
    {
        int optionsCount = options.Count - 1;

        float step = 0;
        int indexer = 0;
        for(int i = 1; i < options.Count; i++)
        {
            CircleOption option = options[i];

            step = 2 * Mathf.PI / optionsCount * indexer;
            PleaceOption(new Vector2(Mathf.Sin(step), Mathf.Cos(step)) * circleRadius , option);
            indexer++;
        }
    }

    void PleaceOption(Vector2 pose, CircleOption option)
    {
        option.body = Instantiate(optionPref,
            optionsBodysHolder.transform.position + (Vector3)pose, Quaternion.identity);

        option.body.transform.parent = optionsBodysHolder.transform;

        option.body.GetComponent<SpriteRenderer>().sprite = option.sprite;

        option.pose = pose;
    }

    void ShowOptions(bool show)
    {
        optionsBodysHolder.SetActive(show);
    }

    void ChooseAndHide()
    {
        CircleOption option = ChoseOptionBasedOnPosition();

        if(commands.state == MonkayCommands.MonkayState.onBack 
            && option.name != Action.none 
            && option.name != Action.yell)
            queueCommands.Add(GetOption(Action.onBack));
        queueCommands.Add(option);

        ShowOptions(false);
    }

    public void ExecuiteOption(CircleOption option)
    {
        if (!option.active)
            return;

        //if()

        switch (option.name)
        {
            case Action.wait:
                commands.Wait();
                break;

            case Action.follow:
                commands.FollowMe();
                break;

            case Action.go:
                commands.GO();
                break;

            case Action.onBack:
                commands.OnBack();
                break;

            case Action.switchChar:
                commands.SwitchCharacters();
                break;

            case Action.yell:
                commands.Yell();
                break;

            default:
                skills.PrepareSkill(option);
                break;
        }
    }

    void Light(CircleOption option)
    {
        foreach (CircleOption opt in options)
        {
            if(opt.active)
                opt.body.GetComponent<SpriteRenderer>().color = Color.white;
            else
                opt.body.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        if (option.active)
            option.body.GetComponent<SpriteRenderer>().color = Color.green;
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(mouseKeyNum))
        {
            commands.GetComponent<CameraPortal>().Freeze(true);
            ShowOptions(true);
            mosePoseOnClick = Input.mousePosition;
        }
        else if(Input.GetMouseButton(mouseKeyNum))
        {
            Light(ChoseOptionBasedOnPosition());
        }
        else if (Input.GetMouseButtonUp(mouseKeyNum))
        {
            commands.GetComponent<CameraPortal>().Freeze(false);
            ChooseAndHide();
        }

        if(queueCommands.Count > 0 && timer < Time.time)
        {
            ExecuiteOption(queueCommands[0]);
            queueCommands.RemoveAt(0);
            timer = Time.time + timeBetweenCommands;
        }
	}
}
