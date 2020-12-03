using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

	[Header("Panels")]
	public GameObject MainPanel;
	public GameObject LevelsPanel;
	public GameObject ControlsPanel;

	void Start()
	{
		
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
           Back();
		}
	}

	public void  Play()
	{
		MainPanel.gameObject.SetActive(false);
		LevelsPanel.gameObject.SetActive(true);
	}

	public void  Controls()
	{
		MainPanel.gameObject.SetActive(false);
		ControlsPanel.gameObject.SetActive(true);
	}
    
	public void Back()
	{
		MainPanel.gameObject.SetActive(true);
		ControlsPanel.gameObject.SetActive(false);
		LevelsPanel.gameObject.SetActive(false);
	}

	public void  Quit()
	{
		Application.Quit();
	}
}