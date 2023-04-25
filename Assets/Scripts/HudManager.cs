using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
	private MainMenu MainMenuScript;
	private InGameUI InGameUIScript;

	private TextMeshProUGUI PointsText;

	void Awake()
	{
		var mainMenuObject = GameObject.FindGameObjectWithTag("MainMenu");
		if (mainMenuObject.TryGetComponent<MainMenu>(out var mainMenuScript))
		{
			MainMenuScript = mainMenuScript;
		}

		var inGameUIObject = GameObject.FindGameObjectWithTag("InGameUI");
		if (inGameUIObject.TryGetComponent<InGameUI>(out var inGameUIScript))
		{
			InGameUIScript = inGameUIScript;
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

	public void ShowMainMenu()
	{
		if (MainMenuScript == null)
		{
			return;
		}

		MainMenuScript.InitializeGameUI();
	}

	public void HideMainMenu()
	{
		if (MainMenuScript == null)
		{
			return;
		}

		MainMenuScript.HideMainMenuUI();
	}

	public void ShowInGameMenu()
	{
		if (InGameUIScript == null)
		{
			return;
		}

		InGameUIScript.ShowInGameUI();
	}

	public void HideGameMenu()
	{
		if (InGameUIScript == null)
		{
			return;
		}

		InGameUIScript.HideInGameUI();
	}

	public void UpdatePointsText(int points = 0)
	{
		if (PointsText != null)
		{
			PointsText.text = $"POINTS: {points}";
		}
	}
}
