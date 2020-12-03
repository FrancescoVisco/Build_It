using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    public bool Fade = false;

    private Scene ThisScene;
    private string scene;

    void Update()
    {
       ThisScene = SceneManager.GetActiveScene();
       scene = ThisScene.name;

       if(Fade == true)
       {
          LoadNextLevel();
          transition.SetBool("Fade", true);
       }

       /*if(Input.GetKeyDown(KeyCode.L))
       {
         Fade = true;
         
         if(scene == "Last")
         {
            Fade = true;
         }
       }*/
    }

    public void LoadNextLevel()
    {
      if(scene != "0_MainMenu")
      {
         if(GameObject.Find("Timer").GetComponent<UITimer>().time < 0 || GameObject.Find("CanvasPause").GetComponent<PauseMenu>().GameIsPaused == true)
         {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
         }
         else
         {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));   
         }
      }
    }

    IEnumerator LoadLevel(int LevelIndex)
    {  
       yield return new WaitForSeconds(transitionTime);
       SceneManager.LoadScene(LevelIndex);
    }
}
