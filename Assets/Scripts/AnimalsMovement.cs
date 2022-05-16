using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class AnimalsMovement : MonoBehaviour
{
    private float minPos;
    private float maxPos;
    private float CurrentPos;
    public static int currentAnimal = -1;
    private List<Quaternion> AnimalsStart;
    public List<GameObject> Animals;
    public TextMeshProUGUI TimeLeft;

    public void Awake()
    {
        AnimalsStart = new List<Quaternion>();
        foreach (var i in Animals)
        {
            AnimalsStart.Add(i.transform.GetChild(0).rotation);
        }
        if (maxPos == 0)
        {
            minPos = Animals.Min(x => x.transform.position.x);
            maxPos = Animals.Max(x => x.transform.position.x);
            if (currentAnimal == -1)
                currentAnimal = 1;
            CurrentPos = (currentAnimal - 1) * 5;
            for (int i = 0; i < Animals.Count; i++)
            {
                Animals[i].transform.position = new Vector3(Animals[i].transform.position.x - CurrentPos, Animals[i].transform.position.y, Animals[i].transform.position.z);
                Animals[i].transform.GetChild(0).rotation = AnimalsStart[i];
            }
        }
        UpdateTimeText();
        GM.instance.AchivePanelManager.UpdateTimeText();
    }

    private void OnDisable()
    {
        for (int i = 0; i < Animals.Count; i++)
        {
            Animals[i].transform.GetChild(0).rotation = AnimalsStart[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        Animals[currentAnimal].transform.GetChild(0).Rotate(0, 1.0f, 0);
    }

    public void UpdateTimeText()
    {
        TimeLeft.text = (GM.instance.AnimalsLeftTimeToUnlock[Animals[currentAnimal].name] / 60).ToString() + ":00";
    }

    public void Jump()
    {
        Animals[currentAnimal].transform.GetChild(0).GetComponent<PlayerController>().jump = true;
    }

    public void ToLeftButton()
    {
        if (CurrentPos + 5 > maxPos)
            return;
        CurrentPos += 5;
        currentAnimal++;
        for (int i = 0; i < Animals.Count; i++)
        {
            Animals[i].transform.position = new Vector3(Animals[i].transform.position.x - 5, Animals[i].transform.position.y, Animals[i].transform.position.z);
            Animals[i].transform.GetChild(0).rotation = AnimalsStart[i];
        }
        UpdateTimeText();
        GM.instance.AchivePanelManager.UpdateTimeText();
    }

    public void ToRightButton()
    {
        if (CurrentPos - 5 < minPos)
            return;
        CurrentPos -= 5;
        currentAnimal--;
        for (int i = 0; i < Animals.Count; i++)
        {
            Animals[i].transform.position = new Vector3(Animals[i].transform.position.x + 5, Animals[i].transform.position.y, Animals[i].transform.position.z);
            Animals[i].transform.GetChild(0).rotation = AnimalsStart[i];
        }
        UpdateTimeText();
        GM.instance.AchivePanelManager.UpdateTimeText();
    }
}
