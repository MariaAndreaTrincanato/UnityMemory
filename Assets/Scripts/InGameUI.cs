using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
	private TextMeshProUGUI PointsText;
	private GameObject TimerObject;
	private Transform QuitGameButton;
	private Transform RetryGameButton;
	private Transform GameOverText;

	private HudManager HudManager;

	void Start()
	{
		TimerObject = GameObject.FindGameObjectWithTag("TimerComponent");
		if (TimerObject != null)
		{
			TimerObject.SetActive(false);
		}

		var hudManagerObject = GameObject.FindGameObjectWithTag("HudManager");
		if (hudManagerObject != null)
		{
			if (hudManagerObject.TryGetComponent<HudManager>(out var hudManagerScript))
			{
				HudManager = hudManagerScript;
				GameStateManager.PointsUpdated += HudManager.UpdatePointsText;
				GameStateManager.GameStarted += HudManager.ShowInGameMenu;
			}
		}

		var pointsObject = GameObject.FindGameObjectWithTag("PointsText");
		if (pointsObject != null)
		{
			if (pointsObject.TryGetComponent<TextMeshProUGUI>(out PointsText))
			{
				PointsText.color = Color.clear;
			}
		}

		QuitGameButton = transform.GetChild(2);
		if (QuitGameButton != null)
		{
			QuitGameButton.gameObject.SetActive(false);
		}

		RetryGameButton = transform.GetChild(3);
		if (RetryGameButton != null)
		{
			RetryGameButton.gameObject.SetActive(false);
		}

		GameOverText = transform.GetChild(4);
		if (GameOverText != null)
		{
			GameOverText.gameObject.SetActive(false);
		}

		
		GameStateManager.GameEnded += OnGameEnded;
	}

	private void OnDestroy()
	{
		if (HudManager != null)
		{
			GameStateManager.PointsUpdated -= HudManager.UpdatePointsText;
			GameStateManager.GameStarted -= HudManager.ShowInGameMenu;
		}
		
		GameStateManager.GameEnded -= OnGameEnded;
	}

	public void ShowInGameUI()
	{
		QuitGameButton = transform.GetChild(2);
		if (QuitGameButton != null)
		{
			QuitGameButton.gameObject.SetActive(true);
		}

		RetryGameButton = transform.GetChild(3);
		if (RetryGameButton != null)
		{
			RetryGameButton.gameObject.SetActive(false);
		}

		GameOverText = transform.GetChild(4);
		if (GameOverText != null)
		{
			GameOverText.gameObject.SetActive(false);
		}

		if (TimerObject != null)
		{
			TimerObject.SetActive(false);
		}

		if (PointsText != null)
		{
			PointsText.color = Color.white;
		}
	}
	
	public void HideInGameUI()
	{
		QuitGameButton = transform.GetChild(2);
		if (QuitGameButton != null)
		{
			QuitGameButton.gameObject.SetActive(false);
		}

		RetryGameButton = transform.GetChild(3);
		if (RetryGameButton != null)
		{
			RetryGameButton.gameObject.SetActive(false);
		}

		GameOverText = transform.GetChild(4);
		if (GameOverText != null)
		{
			GameOverText.gameObject.SetActive(false);
		}

		if (PointsText != null)
		{
			PointsText.color = Color.clear;
		}

		HudManager.UpdatePointsText(0);

		// hide cards
	}

	public void OnQuitGameButtonClicked()
	{
		var gameStateObject = GameObject.FindGameObjectWithTag("GameStateManager");
		if (gameStateObject != null)
		{
			if (gameStateObject.TryGetComponent<GameStateManager>(out var script))
			{
				script.HideGameElements();
				script.PlayButtonSelectedSound();
				HudManager.HideGameMenu();
				HudManager.ShowMainMenu();
			}
		}
	}

	public void OnRetryGameButtonClicked()
	{
		var gameStateObject = GameObject.FindGameObjectWithTag("GameStateManager");
		if (gameStateObject != null)
		{
			if (gameStateObject.TryGetComponent<GameStateManager>(out var script))
			{
				HudManager.ShowInGameMenu();
				HudManager.UpdatePointsText(0);
				script.PlayButtonSelectedSound();
				script.StartGame();
			}
		}
	}

	private void OnGameEnded(bool won)
	{
		ShowGameOverUI();
		// TODO: play sound based on win status
	}

	public void ShowGameOverUI()
	{
		if (GameOverText != null)
		{
			GameOverText.gameObject.SetActive(true);
		}

		if (RetryGameButton != null)
		{
			RetryGameButton.gameObject.SetActive(true);
		}
	}
}
