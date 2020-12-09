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
    [Space]

    public bool GoalOn = false;
    public float Star1Perc;
    public float Star2Perc;
    public float Star3Perc;
    public GameObject[] Star;

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
        GoalOn = GameObject.Find("LevelGoal").GetComponent<Goal>().GoalOn;

        TimePerc = (int)((Time/MaxTime)*100);
        ObjPerc = 100 - ((100 * NObj)/MaxObj);
        PointPerc = (TimePerc + ObjPerc)/2;

        if(GoalOn == true)
        {
          if(PointPerc > Star1Perc && PointPerc < Star2Perc)
          {
            Star[0].SetActive(true);
            Star[1].SetActive(false);
            Star[2].SetActive(false);
          }

          if(PointPerc > Star2Perc && PointPerc < Star3Perc)
          {
            Star[0].SetActive(true);
            Star[1].SetActive(true);
            Star[2].SetActive(false);  
          }

          if(PointPerc > Star3Perc)
          {
            Star[0].SetActive(true);
            Star[1].SetActive(true);
            Star[2].SetActive(true);
          }
        }
    }
}
