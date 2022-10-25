using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [SerializeField] string[] _timeString;
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] Door_Button LeftDoor;
    [SerializeField] Door_Button RightDoor;

    private Professor[] _allProfessor;
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

    IEnumerator StartTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            _timer += 1;
            if(_timer == 10 && _isFirstHour)
            {
                _timer = 0;
                _isFirstHour = false;
                timeCount++;
                IncreaseGameDifficulty();
                IncreaseGameDifficulty();
                IncreaseGameDifficulty();
            }
            
            if(_timer == 89)
            {
                _timer = 0;
                timeCount++;
                IncreaseGameDifficulty();
            }
            Text.text = _timeString[timeCount];
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
}
