using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GM : MonoBehaviour
{
    public static GM instance;
    public Button YesButton;
    public AudioSource sound;
    public GameObject AchivePanel;
    public GameObject Animals;
    public GameObject AnimalsLVLText;
    public bool TimeToAchive;
    public AnimalsMovement AnimalsMovement;
    public GameObject FarmAnimals;
    public Dictionary<string, int> AnimalsCount;
    public List<int> AnimalsTimeToUnlock;
    public Dictionary<string, int> AnimalsLeftTimeToUnlock;
    public Timer timer;
    public AchivePanelManager AchivePanelManager;
    private void Awake()
    {
        instance = this;
        AnimalsCount = new Dictionary<string, int>();
        AnimalsLeftTimeToUnlock = new Dictionary<string, int>();
        Inicialize();
        Load();
        UpdateFarm();
    }

    void Save()
    {
        PlayerPrefs.SetInt("AnimalIndex", AnimalsMovement.currentAnimal);
        foreach(var i in AnimalsCount)
        {
            PlayerPrefs.SetInt(i.Key, i.Value);
            PlayerPrefs.SetInt(i.Key + "Time", AnimalsLeftTimeToUnlock[i.Key]);
        }
    }

    void Load()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("AnimalIndex"))
        {
            TimeToAchive = true;
            AnimalsMovement.currentAnimal = PlayerPrefs.GetInt("AnimalIndex");
        }
        var keys = new string[AnimalsCount.Keys.Count];
        AnimalsCount.Keys.CopyTo(keys, 0);
        foreach (var i in keys)
        {
            if (PlayerPrefs.HasKey(i))
            {
                AnimalsCount[i] = PlayerPrefs.GetInt(i);
            }
            if (PlayerPrefs.HasKey(i + "Time"))
            {
                AnimalsLeftTimeToUnlock[i] = PlayerPrefs.GetInt(i + "Time");
            }
        }
    }
    
    public void Inicialize()
    {
        for (int i = 0; i < AnimalsMovement.Animals.Count; i++)
        {
            AnimalsCount[AnimalsMovement.Animals[i].name] = 0;
            AnimalsLeftTimeToUnlock[AnimalsMovement.Animals[i].name] = AnimalsTimeToUnlock[i];
        }
    }

    public void OnTimerEnd()
    {
        sound.Stop();
        TimeToAchive = true;
        AchivePanel.SetActive(true);
        Animals.SetActive(true);
        int leftTime = AnimalsLeftTimeToUnlock[AnimalsMovement.Animals[AnimalsMovement.currentAnimal].name];
        leftTime -= (int)timer.TimeToAchieve;
        AnimalsLeftTimeToUnlock[AnimalsMovement.Animals[AnimalsMovement.currentAnimal].name] = Mathf.Clamp(leftTime, 0, leftTime);
        
        AchivePanelManager.UpdateTimeText();
        if (AnimalsLeftTimeToUnlock[AnimalsMovement.Animals[AnimalsMovement.currentAnimal].name] == 0)
        {
            AnimalsCount[AnimalsMovement.Animals[AnimalsMovement.currentAnimal].name]++;
            AnimalsLeftTimeToUnlock[AnimalsMovement.Animals[AnimalsMovement.currentAnimal].name] = AnimalsTimeToUnlock[AnimalsMovement.currentAnimal];
        }
        Save();
        UpdateFarm();
        AnimalsMovement.UpdateTimeText();
        YesButton.onClick.Invoke();
    }

    public void UpdateFarm()
    {
        foreach (Transform i in FarmAnimals.transform)
        {
            var animalLvl = AnimalsLVLText.transform.Find(i.name);
            if (AnimalsCount[i.name] > 0)
            {
                i.gameObject.SetActive(true);
                animalLvl.gameObject.SetActive(true);
                animalLvl.GetComponent<TextMeshProUGUI>().text = AnimalsCount[i.name].ToString();
            }    
            else
            {
                i.gameObject.SetActive(false);
                animalLvl.gameObject.SetActive(false);
            }
        }
    }    
}
