using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    public bool GameIsPaused = false;
    private int currentSceneIndex;
    public GameObject PauseMenuUI;

    private Scene activeScene;

    private void Start()
    {
        activeScene = SceneManager.GetActiveScene();
    }

    void Update()
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

        if(GameIsPaused == false)
        {
            StopCoroutine(Delay());
        }
    }



    public void Resume()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        StartCoroutine(Delay());
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;
        Time.timeScale = 1f;
        StartCoroutine(RestartDelay());   
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.01f);  
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(activeScene.name); 
    }
}
