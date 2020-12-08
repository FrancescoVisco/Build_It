using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MoveToLevel : MonoBehaviour
{
    public int SceneToLoad;

    public void Level()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().SceneToLoad = SceneToLoad;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Fade = true;       
    }
}
