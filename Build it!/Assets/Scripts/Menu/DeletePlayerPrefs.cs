using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayerPrefs : MonoBehaviour
{
    public bool ResetOn;

    public void Awake()
    {
        if(ResetOn == true)
        {
            Reset();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
