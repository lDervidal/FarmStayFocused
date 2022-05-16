using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public bool timerIsRunning;
    public TextMeshProUGUI timeText;
    public float StartTime = 600;
    public float TimeToAchieve;

    private void Start()
    {
        timeRemaining = StartTime;
        timerIsRunning = false;
        OnChanged(timeRemaining / (5 * 60));
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                GM.instance.OnTimerEnd();
            }
        }
    }
    
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnChanged(float value)
    {
        timeText.text = (value * 5).ToString() + ":00";
        timeRemaining = value * 5 * 60;
        TimeToAchieve = timeRemaining;
    }

    public void TimeStart()
    {
        timerIsRunning = true;
    }

    public void TimeEnd(float time)
    {
        timerIsRunning = false;
        OnChanged(time);
        timeRemaining = StartTime;
    }
}