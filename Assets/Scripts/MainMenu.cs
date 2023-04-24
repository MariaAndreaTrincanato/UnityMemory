using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	private GameStateManager GameStateManager;

	private GameObject AppName;
	private GameObject CreditsButton;
	private GameObject PlayButton;
	private GameObject QuitButton;
	
	void Awake()
	{
		var gameStateManagerGameObject = GameObject.FindGameObjectWithTag("GameStateManager");
		if (gameStateManagerGameObject != null)
		{
			GameStateManager = gameStateManagerGameObject.GetComponent<GameStateManager>();
		}

		AppName = GameObject.FindGameObjectWithTag("AppName");
		CreditsButton = GameObject.FindGameObjectWithTag("CreditsButton");
		PlayButton = GameObject.FindGameObjectWithTag("PlayButton");
		QuitButton = GameObject.FindGameObjectWithTag("QuitButton");

		InitializeGameUI();
	}

	public void InitializeGameUI()
	{
		if (GameStateManager != null)
		{
			GameStateManager.HideGameElements();
			AppName.SetActive(true);
			CreditsButton.SetActive(true);
			PlayButton.SetActive(true);
			QuitButton.SetActive(true);
		}
	}

	public void OnPlayButtonClicked()
	{
		GameStateManager.PlayButtonSelectedSound();
		AppName.SetActive(false);
		CreditsButton.SetActive(false);
		PlayButton.SetActive(false);
		QuitButton.SetActive(false);

		if (GameStateManager != null)
		{
			GameStateManager.StartGame();
		}
	}

	public void OnQuitButtonClicked()
	{
		GameStateManager.PlayButtonSelectedSound();
		Application.Quit();
	}

	public void OnCreditsButtonClicked()
	{
		GameObject creditsRef = GameObject.FindGameObjectWithTag("CreditsCanvas");
		if (creditsRef != null)
		{
			if (creditsRef.TryGetComponent<Credits>(out var script))
			{
				GameStateManager.PlayButtonSelectedSound();
				script.ShowCreditsUI();
			}
		}
	}
}
