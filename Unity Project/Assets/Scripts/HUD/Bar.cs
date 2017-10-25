using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    public float baseValue;

    float currentValue;

    public float deltaSpeed;

    public float currentDelta;

    public float GetCurrentFill()
    {
        return currentValue;
    }

    void ChangeCurrentValue(float value)
    {
        print(1);

        //if ((currentValue == 1 && value > 0) || (currentValue == 0 && value < 0))
        //    return;

        print(2);

        currentDelta -= currentValue - value;
        GetComponent<Image>().material.SetFloat("_Delta", currentDelta);

        currentValue = value;

        if(currentValue > 1)
        {
            currentValue = 1;
        }
        else if(currentValue < 0)
        {
            currentValue = 0;
        }
    }

    public void SetFill(float value)
    {
        ChangeCurrentValue(value);
        GetComponent<Image>().material.SetFloat("_Fill", value);
    }

    public void AddFill(float value)
    {
        ChangeCurrentValue(currentValue + value);
        SetFill(currentValue);
    }

    // Use this for initialization
    void Start()
    {
        SetFill(baseValue);
        currentDelta = 0;
        GetComponent<Image>().material.SetFloat("_Delta", currentDelta);
    }
	// Update is called once per frame
	void Update () {
		if(currentDelta != 0)
        {
            //if (currentDelta < -1 + currentValue)
            //    currentDelta = -1 + currentValue;
            //else if (currentDelta > (currentValue))
            //    currentDelta = (currentValue);

            currentDelta += ( currentDelta>0?-1:1) * Time.deltaTime * deltaSpeed;
            GetComponent<Image>().material.SetFloat("_Delta", currentDelta);
        }
	}
}
