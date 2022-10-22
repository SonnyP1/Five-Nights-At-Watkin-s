using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    [SerializeField] Image[] BatteryImages;
    [SerializeField] TextMeshProUGUI Text;
    private float percent = 1.0f;
    public void AddPercent(float val)
    {
        percent = Mathf.Clamp(percent+val, 0.0f, 1.0f);

        if (percent < 0.75)
        {
            BatteryImages[3].enabled = false;
        }
        if(percent < .5f)
        {
            BatteryImages[2].enabled = false;
        }
        if(percent < .25)
        {
            BatteryImages[1].enabled = false;
        }
        if(percent ==0)
        {
            BatteryImages[0].enabled = false;
        }

        Text.text = (percent*100).ToString("F0")+ "%";
    }
}
