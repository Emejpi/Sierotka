using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour {

    PlayerControlSettings settings;
    MonkayCommands commands;

    public Image preparedSkillIcon;
    CircleOption preperedSkill;
    bool preparedSkillActivated;

    float timer;

    public CircleSelect circleSelect;
    List<CircleOption> options;

    PatrolHoler patrolHolder;

    public Bar hungerBar;

    public void PrepareSkill(CircleOption option)
    {
        if(preperedSkill)
        {
            preperedSkill.active = true;
        }

        preparedSkillIcon.sprite = option.sprite;
        preperedSkill = option;

        preperedSkill.active = false;

    }

	// Use this for initialization
	void Start () {
        patrolHolder = GetComponent<CameraPortal>().orange.GetComponent<PatrolHoler>();
        settings = GetComponent<PlayerControlSettings>();
        commands = GetComponent<MonkayCommands>();
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (commands.IsOrphanActive())
        {
            if(preparedSkillActivated) //activated
            {
                switch (preperedSkill.name)
                {
                    case CircleSelect.Action.showFildsOfView:
                        patrolHolder.ShowFieldsOfView(transform.position);
                        break;
                }

                switch(preperedSkill.type)
                {
                    case CircleOption.SkillType.hold:
                        if (hungerBar.GetCurrentFill() == 0)
                            DisactivatePreparedSkill();

                        hungerBar.AddFill(-preperedSkill.cost * Time.deltaTime);
                        break;

                    case CircleOption.SkillType.timer:
                        if(timer < Time.time)
                        {
                            DisactivatePreparedSkill();
                        }
                        break;

                    case CircleOption.SkillType.use:
                        if (timer < Time.time)
                        {
                            DisactivatePreparedSkill();
                        }
                        break;
                }
            }

            if (Input.GetKeyDown(settings.useSkill) && preperedSkill && !preparedSkillActivated) //click
            {
                if (hungerBar.GetCurrentFill() == 0)
                    return;

                if (preperedSkill.type != CircleOption.SkillType.hold)
                {
                    if (hungerBar.GetCurrentFill() < preperedSkill.cost)
                        return;
                    hungerBar.AddFill(-preperedSkill.cost);
                }

                switch (preperedSkill.name)
                {
                     case CircleSelect.Action.showFildsOfView:
                        patrolHolder.ShowFieldsOfView(transform.position);
                        break;
                }

                timer = preperedSkill.timerDur + Time.time;

                preparedSkillIcon.color = Color.green;
                ActivatePreparedSkill(true);
            }
            else if (Input.GetKeyUp(settings.useSkill)) //release
            {
                switch (preperedSkill.type)
                {
                    case CircleOption.SkillType.hold:
                        DisactivatePreparedSkill();
                        break;
                }

            }
        }
    }

    void ActivatePreparedSkill(bool activate)
    {
        preparedSkillActivated = activate;

        circleSelect.UnclockAllOptions(!activate);
    }

    void DisactivatePreparedSkill() //disactivate
    {
        preparedSkillIcon.color = Color.white;
        ActivatePreparedSkill(false);

        switch (preperedSkill.name)
        {
            case CircleSelect.Action.showFildsOfView:
                patrolHolder.HideFieldsOfView();
                break;
        }
    }
}
