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

    public int activeScene;
    private bool CoroutineEnd;

    private void Start()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
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

        if(CoroutineEnd == true)
        {
            StopCoroutine(RestartExitDelay(0)); 
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
        //StartCoroutine(RestartExitDelay(activeScene));  
        SceneManager.LoadScene(activeScene); 
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
        //StartCoroutine(RestartExitDelay(0)); 
        SceneManager.LoadScene(0);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.01f);  
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    IEnumerator RestartExitDelay(int SceneToLoad)
    {
        //GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneToLoad); 
        CoroutineEnd = true;
    }
}
