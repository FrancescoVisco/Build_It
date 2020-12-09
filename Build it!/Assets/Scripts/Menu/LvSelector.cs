using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LvSelector : MonoBehaviour
{
    public int SceneToLoad;
    public GameObject[] Stars;
    private int stars;

    public void Level()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().SceneToLoad = SceneToLoad;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;       
    }

    public void Update()
    {
        if(SceneToLoad == 1)
        {
            stars = PlayerPrefs.GetInt("Level1");
        }

        if(SceneToLoad == 2)
        {
            stars = PlayerPrefs.GetInt("Level2");
        }

        if(SceneToLoad == 3)
        {
            stars = PlayerPrefs.GetInt("Level3");
        }
          
        if(stars == 3)
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
        }

        if(stars == 2)
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
        }

        if(stars == 1)
        {
            Stars[0].SetActive(true);
        }
    }
}
