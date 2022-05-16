using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{
    public List<string> TagList = new List<string>();
    public List<string> SelectedTag = new List<string>();
    public GameObject TagPrefab;

    public void OnAdd()
    {

    }

    public class Tag
    {
        public string Name;
        public float Value;

        public void Rename()
        {

        }

        public void Delete()
        {

        }
    }
}
