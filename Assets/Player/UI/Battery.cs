using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Battery : MonoBehaviour
{
    [SerializeField] Image[] BatteryImages;
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] GameObject BlueLight;
    [SerializeField] AudioSource _powerTurningOff;
    [SerializeField] AudioSource _scarySound;
    [SerializeField] AudioSource _bgNoise;
    [SerializeField] AudioSource _phoneGuy;
    [SerializeField] GameObject _jumpScareObj;
    [SerializeField] VideoPlayer _jumpScareVid;
    private float percent = 1.0f;
    private GameStats _gameStats;

    private void Start()
    {
        _gameStats = FindObjectOfType<GameStats>();
    }
    public float GetBatteryPercent()
    {
        return percent;
    }
    public void AddPercent(float val)
    {
        if(percent <= 0)
        {
            return;
        }
        percent = Mathf.Clamp(percent+val*5, 0.0f, 1.0f);

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
        if(percent <= 0)
        {
            BatteryImages[0].enabled = false;
            Death();
        }

        Text.text = (percent*100).ToString("F0")+ "%";
    }

    private void Death()
    {
        _phoneGuy.Stop();
        _bgNoise.Stop();
        _powerTurningOff.Play();
        Light[] lightsInScene = FindObjectsOfType<Light>();
        foreach(Light light in lightsInScene)
        {
            light.enabled = false;
        }


        Button[] buttonsInScene = FindObjectsOfType<Button>();
        foreach(Button button in buttonsInScene)
        {
            button.enabled = false;
        }

        BlueLight.SetActive(true);
        StartCoroutine(JumpScare());
    }


    IEnumerator JumpScare()
    {
        _gameStats.StopAllAI();
        yield return new WaitForSeconds(5f);
        _scarySound.Play();
        yield return new WaitForSeconds(Random.Range(9f,14f));
        _scarySound.Stop();
        yield return new WaitForSeconds(0.2f);
        BlueLight.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        BlueLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        BlueLight.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        BlueLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        BlueLight.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        BlueLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        BlueLight.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        BlueLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        BlueLight.SetActive(false);
        yield return new WaitForSeconds(1f);
        _jumpScareObj.SetActive(true);
        _jumpScareVid.Play();
    }
}
