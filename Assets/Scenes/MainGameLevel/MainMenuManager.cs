using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button StartBtn;

    private void Start()
    {
        StartBtn.onClick.AddListener(LoadMainGameLevel);
    }


    private void LoadMainGameLevel()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
}
