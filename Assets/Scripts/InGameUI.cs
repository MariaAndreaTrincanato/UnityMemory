using TMPro;
using UnityEngine;

public class InGame : MonoBehaviour
{
	private TextMeshPro PointsText;
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
			PointsText = pointsObject.GetComponent<TextMeshPro>();
		}
	}

	private void OnDestroy()
	{
		GameStateManager.PointsUpdated -= UpdatePointsText;
	}

	private void UpdatePointsText(int points)
	{
		PointsText.text = $"POINTS: {points}";
	} 
}
