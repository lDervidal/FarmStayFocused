using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    //public Sprite layer_green, layer_blue;
    public GameObject timerText;
    public Stopwatch stopwatchText;
    public GameObject slider;
    public GameObject stopMusic;
    private Timer timer;
    private AudioSource sound;


    public void Start()
    {
        Application.runInBackground = true;
        sound = GM.instance.sound;
        timer = GM.instance.timer;
    }

    public void StartButton()
    {
        OnStartButton();
        slider.SetActive(!slider.activeSelf);
        sound.Play();
       
    }
    
    public void OnStart()
    {
        timer.TimeStart();
    }

    public void MusicButton()
    {
        stopMusic.SetActive(!stopMusic.activeSelf);
        sound.enabled = !sound.enabled;
    }

    public void SurrendButton()
    {
        slider.SetActive(true);
        var sliderComponent = slider.GetComponent<Slider>();
        sliderComponent.value = sliderComponent.minValue;

        timer.TimeEnd(sliderComponent.minValue);
        stopwatchText.TimeEnd();

        if (stopwatchText.gameObject.activeSelf)
        {
            stopwatchText.gameObject.SetActive(false);
            timerText.SetActive(true);
            slider.SetActive(true);
        }

        sound.Stop();
    }

    public void OnStartButton()
    {
        if (timerText.activeSelf)
        {
            timer.timerIsRunning = !timer.timerIsRunning;
            stopwatchText.timeStart = 0;
        }
        else
        {
            stopwatchText.isTimerRunning = !stopwatchText.isTimerRunning;
            timer.timeRemaining = timer.StartTime;
        }
    }

    public void StopwatchButton()
    {
        if (timerText.activeSelf)
        {
            stopwatchText.gameObject.SetActive(true);
            timerText.SetActive(false);
            slider.SetActive(false);
        }
    }

    public void TimerButton()
    {
        if (stopwatchText.gameObject.activeSelf)
        {
            stopwatchText.gameObject.SetActive(false);
            timerText.SetActive(true);
            slider.SetActive(true);
        }
    }  
}