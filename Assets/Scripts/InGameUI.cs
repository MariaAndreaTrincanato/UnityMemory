using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
	private TextMeshProUGUI PointsText;
	private GameObject TimerObject;
	private Transform QuitGameButton;
	private Transform RetryGameButton;
	private Transform GameOverText;

	void Start()
	{
		GameStateManager.PointsUpdated += UpdatePointsText;
		GameStateManager.GameStarted += OnGameStarted;
		GameStateManager.GameEnded += OnGameEnded;

		TimerObject = GameObject.FindGameObjectWithTag("TimerComponent");
		if (TimerObject != null)
		{
			TimerObject.SetActive(false);
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
	}

	public void OnQuitGameButtonClicked()
	{
		var gameStateObject = GameObject.FindGameObjectWithTag("GameStateManager");
		if (gameStateObject != null)
		{
			if (gameStateObject.TryGetComponent<GameStateManager>(out var script))
			{
				if (QuitGameButton != null)
				{
					QuitGameButton.gameObject.SetActive(false);
				}
				script.PlayButtonSelectedSound();
				script.EndGame();
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
				if (RetryGameButton != null)
				{
					RetryGameButton.gameObject.SetActive(false);
				}

				if (GameOverText != null)
				{
					GameOverText.gameObject.SetActive(false);
				}

				if (QuitGameButton != null)
				{
					QuitGameButton.gameObject.SetActive(false);
				}

				script.PlayButtonSelectedSound();
				script.StartGame();
			}
		}
	}

	private void OnGameEnded(bool won)
	{
		if (PointsText != null)
		{
			PointsText.color = Color.clear;
		}

		if (GameOverText != null)
		{
			GameOverText.gameObject.SetActive(true);
		}

		if (RetryGameButton != null)
		{
			RetryGameButton.gameObject.SetActive(true);
		}

		// TODO: play sound based on win status
	}

	private void OnDestroy()
	{
		GameStateManager.PointsUpdated -= UpdatePointsText;
		GameStateManager.GameStarted -= OnGameStarted;
		GameStateManager.GameEnded -= OnGameEnded;
	}

	private void UpdatePointsText(int points)
	{
		if (PointsText != null)
		{
			PointsText.text = $"POINTS: {points}";
		}
	}

	private void OnGameStarted()
	{
		if (PointsText != null)
		{
			PointsText.color = Color.white;
		}

		if (QuitGameButton != null)
		{
			QuitGameButton.gameObject.SetActive(true);
		}

		if (GameOverText != null)
		{
			GameOverText.gameObject.SetActive(false);
		}
	}
}
