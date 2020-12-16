using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UITimer : MonoBehaviour {
    public Image fillImg;
    public int timeAmt = 10;
    public float time;
    public float StartTimer;
    public TextMeshProUGUI timeText;
    public GameObject PanelGameOver;
    public bool GameOn;
    public bool TimerOff = false;
    public int scene;

	void Start () 
    {
       fillImg = this.GetComponent<Image>();
       time = timeAmt;
       StartCoroutine(Timer());
       GameOn = false;
       scene = SceneManager.GetActiveScene().buildIndex;
	}
	

	void Update () 
    {
       if(time > 0 && GameOn == true && TimerOff == false && scene != 6)
       {
           time -= Time.deltaTime;
           fillImg.fillAmount = time/ timeAmt;
           timeText.SetText(time.ToString("F0"));

           if(GameObject.Find("LevelGoal").GetComponent<Goal>().TriggeredLine == false && (GameObject.Find("Managers").GetComponent<Spawner>().RObjects == 0))
           {
               StartCoroutine(Delay());  
           }
       }

       if(time < 0)
       {
           StartCoroutine(Delay()); 
       }
	}

    IEnumerator Timer()
    {
       yield return new WaitForSeconds(StartTimer);
       GameOn = true;
    }

    IEnumerator Delay()
    {
       yield return new WaitForSeconds(0.7f);
       PanelGameOver.SetActive(true);
       TimerOff = true; 
    }
}