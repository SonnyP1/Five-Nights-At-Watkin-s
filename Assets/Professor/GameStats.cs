using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [SerializeField] string[] _timeString;
    [SerializeField] TextMeshProUGUI Text;
    private int timeCount;
    private float _timer;
    private bool _isFirstHour = true;
    void Start()
    {
        _timer = 0;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            _timer += 1;
            if(_timer == 90 && _isFirstHour)
            {
                _timer = 0;
                _isFirstHour = false;
                Text.text = _timeString[timeCount];
                timeCount++;
            }
            
            if(_timer == 89)
            {
                _timer = 0;
                Text.text = _timeString[timeCount];
                timeCount++;
            }
        }
    }
}
