using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
	private TextMeshProUGUI PointsText;
	private GameObject TimerObject;
	
	void Start()
	{
		GameStateManager.PointsUpdated += UpdatePointsText;

		TimerObject = GameObject.FindGameObjectWithTag("TimerComponent");
		if (TimerObject != null)
		{
			TimerObject.SetActive(false);
		}

		var pointsObject = GameObject.FindGameObjectWithTag("PointsText");
		if (pointsObject != null)
		{
			PointsText = pointsObject.GetComponent<TextMeshProUGUI>();
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
}
