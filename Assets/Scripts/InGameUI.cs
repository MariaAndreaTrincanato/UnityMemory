using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
	private TextMeshProUGUI PointsText;
	private GameObject TimerObject;
	
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
	}

	private void OnGameEnded(bool won)
	{
		if (PointsText != null)
		{
			PointsText.color = Color.clear;
		}
	}

	private void OnDestroy()
	{
		GameStateManager.PointsUpdated -= UpdatePointsText;
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
	}
}
