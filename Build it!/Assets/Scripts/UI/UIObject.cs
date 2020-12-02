using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIObject : MonoBehaviour
{
    public int i;
    public int[] NObject;
    public int[] MaxObject;
    public int UINumber;
    public TextMeshProUGUI Text;

    void Start()
    {
        MaxObject = GameObject.Find("Managers").GetComponent<Spawner>().MaxObjects;
        NObject = GameObject.Find("Managers").GetComponent<Spawner>().NObjects;
    }

    void Update()
    { 
        MaxObject = GameObject.Find("Managers").GetComponent<Spawner>().MaxObjects;
        NObject = GameObject.Find("Managers").GetComponent<Spawner>().NObjects;
        UINumber = MaxObject[i] - NObject[i];
        Text.SetText(UINumber.ToString());

        if(UINumber < 0)
        {
            UINumber = 0;
        }

        if(i == -1)
        {
            Text.SetText("/221E".ToString());
        }
    }
}
