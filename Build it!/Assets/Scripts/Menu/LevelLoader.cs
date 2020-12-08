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
    public int SceneToLoad;

    private Scene ThisScene;
    private string scene;

    void Update()
    {
       ThisScene = SceneManager.GetActiveScene();
       scene = ThisScene.name;
 

      if(Fade == true)
      {
         LoadNextLevel(SceneToLoad);
         transition.SetBool("Fade", true);
      }      

      if(SceneToLoad > PlayerPrefs.GetInt("levelAt"))
      {
          PlayerPrefs.SetInt("levelAt", SceneToLoad);
      }
    }

       

    public void LoadNextLevel(int SceneToLoad)
    {
      StartCoroutine(LoadLevel(SceneToLoad));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {  
       yield return new WaitForSeconds(transitionTime);
       SceneManager.LoadScene(LevelIndex);
    }
}
