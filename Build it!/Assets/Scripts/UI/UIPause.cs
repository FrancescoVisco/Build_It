using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIPause : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("LevelGoal").GetComponent<Goal>().GoalOn == false && GameObject.Find("Timer").GetComponent<UITimer>().TimerOff == false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {            
                Pause();
            }
        }
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        StartCoroutine(Delay());
        Time.timeScale = 1f;
    }

    public void Next()
    {
        Time.timeScale = 1f;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().SceneToLoad = SceneManager.GetActiveScene().buildIndex+1;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;  
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().SceneToLoad = SceneManager.GetActiveScene().buildIndex;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;  
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().SceneToLoad = 0;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;  
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.01f);  
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }
}
