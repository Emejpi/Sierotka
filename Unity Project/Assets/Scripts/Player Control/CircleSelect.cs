using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSelect : MonoBehaviour {

    public List<CircleOption> options;

    public GameObject optionPref;

    public GameObject optionsHolder;
    public GameObject optionsBodysHolder;

    public float circleRadius;

    public Vector3 mosePoseOnClick;

    MonkayCommands commands;

	// Use this for initialization
	void Start () {
        commands = GetComponent<MonkayCommands>();

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
            transform.position + (Vector3)pose + new Vector3(0, 0, 1), Quaternion.identity);

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
        if(option.active)
            ExecuiteOption(option);

        ShowOptions(false);
    }

    void ExecuiteOption(CircleOption option)
    {
        switch(option.actionIndex)
        {
            case 1:
                commands.Wait();
                break;

            case 2:
                commands.FollowMe();
                break;

            case 3:
                commands.GO();
                break;

            case 4:
                commands.OnBack();
                break;

            case 5:
                commands.SwitchCharacters();
                break;

            case 6:
                commands.Yell();
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
		if(Input.GetMouseButtonDown(1))
        {
            GetComponent<CameraPortal>().Freeze(true);
            ShowOptions(true);
            mosePoseOnClick = Input.mousePosition;
        }
        else if(Input.GetMouseButton(1))
        {
            Light(ChoseOptionBasedOnPosition());
        }
        else if (Input.GetMouseButtonUp(1))
        {
            GetComponent<CameraPortal>().Freeze(false);
            ChooseAndHide();
        }
	}
}
