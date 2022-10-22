using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Button : MonoBehaviour
{
    [SerializeField] GameObject Door;
    [SerializeField] GameObject Light;
    Battery _battery;

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
        if (isLightActive)
        {
            isLightActive = false;
            Light.SetActive(isLightActive);
        }
        else
        {
            isLightActive = true;
            Light.SetActive(isLightActive);
        }
    }

    private void Update()
    {
        if(isLightActive)
        {
            _battery.AddPercent(-0.005f*Time.deltaTime);
        }

        if(isDoorActive)
        {
            _battery.AddPercent(-0.005f*Time.deltaTime);
        }
    }
}
