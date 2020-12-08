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

	void Start () 
    {
       fillImg = this.GetComponent<Image>();
       time = timeAmt;
       StartCoroutine(Timer());
       GameOn = false;
	}
	

	void Update () 
    {
       if(time > 0 && GameOn == true)
       {
           time -= Time.deltaTime;
           fillImg.fillAmount = time/ timeAmt;
           timeText.SetText(time.ToString("F0"));
       }

       if(time < 0)
       {
           PanelGameOver.SetActive(true);
           TimerOff = true;
       }
	}

    IEnumerator Timer()
    {
       yield return new WaitForSeconds(StartTimer);
       GameOn = true;
    }
}