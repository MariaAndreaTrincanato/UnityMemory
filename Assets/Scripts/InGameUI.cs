using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
	private TextMeshProUGUI PointsText;
	private GameObject TimerObject;
	private Transform QuitGameButton;
	
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
	}

	public void OnQuitGameButtonClicked()
	{
		var gameStateObject = GameObject.FindGameObjectWithTag("GameStateManager");
		if (gameStateObject != null)
		{
			if (gameStateObject.TryGetComponent<GameStateManager>(out var script))
			{
				script.PlayButtonSelectedSound();
				script.EndGame();
			}
		}
	}

	private void OnGameEnded(bool won)
	{
		if (PointsText != null)
		{
			PointsText.color = Color.clear;
		}

		if (QuitGameButton != null)
		{
			QuitGameButton.gameObject.SetActive(false);
		}
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
	}
}
