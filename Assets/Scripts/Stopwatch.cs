using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public float timeStart;
    public TextMeshProUGUI timerText;
    public bool isTimerRunning = false;
    
    void Start()
    {
        timerText.text = timeStart.ToString("F2");
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeStart += Time.deltaTime;
            timerText.text = timeStart.ToString("F2");
        }
    }

    public void ButtonTimer()
    {
        isTimerRunning = !isTimerRunning;
    }

    public void TimeEnd()
    {
        isTimerRunning = false;
        timerText.text = "0,00";
        timeStart = 0;
    }
}
