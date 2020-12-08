using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIEndLevel : MonoBehaviour
{
    public void Next()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().SceneToLoad = SceneManager.GetActiveScene().buildIndex+1;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;  
    }

    public void Restart()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().SceneToLoad = SceneManager.GetActiveScene().buildIndex;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;  
    }

    public void ExitToMenu()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().SceneToLoad = 0;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;  
    }

}
