using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    private void SwitchCam(CinemachineVirtualCamera camToSwitchTo)
    {
        foreach(CinemachineVirtualCamera cam in _allCams)
        {
            cam.Priority = -1;
        }
        camToSwitchTo.Priority = 0;
    }
}
