using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    public int MaxBomb;
    public int MaxObj;
    int NObj;
    float MaxTime;
    float Time;
    public int TimePerc;
    public int ObjPerc;
    public int PointPerc;

    void Start()
    {
        MaxObj = (GameObject.Find("Managers").GetComponent<Spawner>().SMax) - MaxBomb;
        NObj = GameObject.Find("Managers").GetComponent<Spawner>().SObjects; 
        Time = GameObject.Find("Timer").GetComponent<UITimer>().time; 
        MaxTime = GameObject.Find("Timer").GetComponent<UITimer>().timeAmt; 
    }

    
    void Update()
    {
        NObj = GameObject.Find("Managers").GetComponent<Spawner>().SObjects;  
        Time = GameObject.Find("Timer").GetComponent<UITimer>().time;

        TimePerc = (int)((Time/MaxTime)*100);
        ObjPerc = 100 - ((100 * NObj)/MaxObj);
        PointPerc = (TimePerc + ObjPerc)/2;
    }
}
