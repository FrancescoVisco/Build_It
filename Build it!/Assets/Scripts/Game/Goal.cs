using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Goal : MonoBehaviour
{
    public float timeAmt;
    public float time;
    public float delay;
    public TextMeshProUGUI timeText;
    public GameObject CountdownText;
    public bool TriggeredLine = false;
    public bool CoroutineOn = false;
    public GameObject PanelGoal;
    public bool GoalOn = false;
    
    void Start()
    {
       time = timeAmt;
    }

    void Update()
    {

        if(time > 0 && CoroutineOn == true)
        {
           time -= Time.deltaTime;
           timeText.SetText(time.ToString("F0"));
        }

        if(time < 0)
        {
           StartCoroutine(WaitPanel());
           GameObject.Find("Timer").GetComponent<UITimer>().TimerOff = true;
           GoalOn = true;
           CountdownText.SetActive(false);
         }

        
        if(TriggeredLine == true)
        {
            StartCoroutine(Wait());
        }
        else
        {
            StopCoroutine(Wait());
            CoroutineOn = false;
            CountdownText.SetActive(false);
            time = timeAmt; 
        }

        if(CoroutineOn == true && GoalOn == false)
        {
            CountdownText.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
       if(other.tag == "Block")
       {
          TriggeredLine = true;  
       }
    }

    void OnTriggerExit(Collider other)
    {
       if(other.tag == "Block")
       {
          TriggeredLine = false;  
       }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        CoroutineOn = true;
    }

    IEnumerator WaitPanel()
    {
        yield return new WaitForSeconds(1.6f);
        PanelGoal.SetActive(true);
    }
}
