using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour
{
    public void OnApplicationPause(bool _pause)
    {
        if (_pause)
        {
            Handheld.Vibrate();
        }
    }
}
