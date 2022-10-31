using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    [SerializeField] string[] _timeString;
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] Door_Button LeftDoor;
    [SerializeField] Door_Button RightDoor;
    [SerializeField] UIManager UI;
    [SerializeField] AudioSource winSound;

    private Professor[] _allProfessor;

    public UIManager GetUIManager()
    {
        return UI;
    }
    public Door_Button GetDoorLeft()
    {
        return LeftDoor;
    }
    public Door_Button GetDoorRight()
    {
        return RightDoor;
    }

    private int timeCount;
    private float _timer;
    public bool GetIsFirstHour()
    {
        return _isFirstHour;
    }
    private bool _isFirstHour = true;
    void Start()
    {
        _allProfessor = FindObjectsOfType<Professor>();
        _timer = 0;
        StartCoroutine(StartTimer());
    }

    void StartWinSequence()
    {
        StopAllAI();
        StopAllCoroutines();
        StartCoroutine(WinSequence());
    }

    IEnumerator StartTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            _timer += 1;
            if(_timer == 89 && _isFirstHour)
            {
                _timer = 0;
                _isFirstHour = false;
                timeCount++;
                IncreaseGameDifficulty();
            }
            
            if(_timer == 90)
            {
                _timer = 0;
                timeCount++;
                IncreaseGameDifficulty();
            }
            Text.text = _timeString[timeCount];

            if(timeCount == 6)
            {
                StartWinSequence();
            }
        }
    }

    private void IncreaseGameDifficulty()
    {
        foreach(Professor professor in _allProfessor)
        {
            professor.IncreaseDifficultyLevel();
        }
    }

    public void StopAllAI()
    {
        foreach (Professor professor in _allProfessor)
        {
            professor.StopAI();
        }
    }

    IEnumerator WinSequence()
    {
        winSound.Play();
        yield return new WaitForSeconds(winSound.clip.length);
        StartCoroutine(WaitToReloadLevel());
    }


    IEnumerator WaitToReloadLevel()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
