using UnityEngine;

public class MainMenu : MonoBehaviour
{
	private GameStateManager GameStateManager;

	private GameObject AppName;
	private GameObject SettingsButton;
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
		SettingsButton = GameObject.FindGameObjectWithTag("SettingsButton");
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
		}
	}

	public void OnPlayButtonClicked()
	{
		AppName.SetActive(false);
		SettingsButton.SetActive(false);
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
		Application.Quit();
	}

	public void OnSettingsButtonClicked()
	{
		// TODO
	}

	public void OnCreditsButtonClicked()
	{
		// TODO
	}
}
