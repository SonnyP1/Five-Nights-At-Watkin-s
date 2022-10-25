using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _ccTVCam;
    [SerializeField] GameObject _cameraImage;
    [SerializeField] GameObject _cameraSelection;
    [SerializeField] CinemachineVirtualCamera[] _allCams;
    [SerializeField] Battery _battery; 

    public bool GetInTablet()
    {
        return inTablet;
    }
    private bool inTablet;


    private void SwitchToTabletView(bool val)
    {
        _ccTVCam.SetActive(val);
        _cameraImage.SetActive(val);
        _cameraSelection.SetActive(val);
    }

    public void ClickTabBtn()
    {
        if(inTablet)
        {
            inTablet = false;
            SwitchToTabletView(inTablet);
        }
        else
        {
            inTablet = true;
            SwitchToTabletView(inTablet);
        }
    }
    public void CamBtn1()
    {
        SwitchCam(_allCams[0]);
    }
    public void CamBtn2()
    {
        SwitchCam(_allCams[1]);
    }

    public void CamBtn3()
    {
        SwitchCam(_allCams[2]);
    }

    public void CamBtn4()
    {
        SwitchCam(_allCams[3]);
    }
    public void CamBtn5()
    {
        SwitchCam(_allCams[4]);
    }
    public void CamBtn6()
    {
        SwitchCam(_allCams[5]);
    }
    public void CamBtn7()
    {
        SwitchCam(_allCams[6]);
    }
    public void CamBtn8()
    {
        SwitchCam(_allCams[7]);
    }
    public void CamBtn9()
    {
        SwitchCam(_allCams[8]);
    }
    public void CamBtn10()
    {
        SwitchCam(_allCams[9]);
    }
    public void CamBtn11()
    {
        SwitchCam(_allCams[10]);
    }
    private void SwitchCam(CinemachineVirtualCamera camToSwitchTo)
    {
        foreach(CinemachineVirtualCamera cam in _allCams)
        {
            cam.Priority = -1;
        }
        camToSwitchTo.Priority = 0;
    }


    private void Update()
    {
        if (inTablet)
        {
            _battery.AddPercent(-0.00005f * Time.deltaTime);
        }
    }
}
