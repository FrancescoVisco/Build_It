using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITimer : MonoBehaviour {
    public Image fillImg;
    public int timeAmt = 10;
    public float time;
    public float StartTimer;
    public TextMeshProUGUI timeText;
    public bool GameOn;

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
           GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;
       }
	}

    IEnumerator Timer()
    {
       yield return new WaitForSeconds(StartTimer);
       GameOn = true;
    }
}