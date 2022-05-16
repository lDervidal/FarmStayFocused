using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchivePanelManager : MonoBehaviour
{
    public TextMeshProUGUI RemainTime;
    public TextMeshProUGUI AchiveText;
    public GameObject RemainTimePanel;

    public void UpdateTimeText()
    {
        RemainTime.text = (GM.instance.AnimalsLeftTimeToUnlock[GM.instance.AnimalsMovement.Animals[AnimalsMovement.currentAnimal].name] / 60).ToString() + ":00";
        if (GM.instance.AnimalsLeftTimeToUnlock[GM.instance.AnimalsMovement.Animals[AnimalsMovement.currentAnimal].name] == 0)
        {
            RemainTimePanel.SetActive(false);
            AchiveText.transform.gameObject.SetActive(true);
        }
        else
        {
            AchiveText.transform.gameObject.SetActive(false);
            RemainTimePanel.SetActive(true);
        }
    }
}
