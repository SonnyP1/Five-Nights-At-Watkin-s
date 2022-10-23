using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Door_Button : MonoBehaviour
{
    [SerializeField] GameObject Door;
    [SerializeField] GameObject Light;
    Battery _battery;

    public bool GetIsDoorActive()
    {
        return isDoorActive;
    }

    bool isDoorActive;
    bool isLightActive;

    private void Start()
    {
        _battery = FindObjectOfType<Battery>();
        Door.SetActive(false);
        Light.SetActive(false);
    }
    
    public void DoorBtnPressed()
    {
        if (isDoorActive)
        {
            isDoorActive = false;
            Door.SetActive(isDoorActive);
        }
        else
        {
            isDoorActive = true;
            Door.SetActive(isDoorActive);
        }
    }
    public void LightBtnPressed()
    {
        if(isLightActive)
        {
            isLightActive = false;
        }
        else
        {
            isLightActive = true;
        }
    }

    private void Update()
    {
        if(isLightActive)
        {
            Light.SetActive(isLightActive);
            _battery.AddPercent(-0.005f*Time.deltaTime);
        }
        else
        {
            Light.SetActive(isLightActive);
        }

        if(isDoorActive)
        {
            _battery.AddPercent(-0.005f*Time.deltaTime);
        }
    }
}
