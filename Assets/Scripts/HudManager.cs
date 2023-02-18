using UnityEngine;

public class HudManager : MonoBehaviour
{
	private CardsManager CardsManager;

	void Start()
    {
		var cardsContainerGameObject = GameObject.FindGameObjectWithTag("CardsContainer");
		if (cardsContainerGameObject != null)
		{
			if (cardsContainerGameObject.TryGetComponent<CardsManager>(out var script))
			{
				CardsManager = script;
			}
		}
	}

	private void NewGame()
	{
		if (CardsManager != null)
		{
			CardsManager.ResetGame();
		}
	}

	private void QuitGame()
	{
		Application.Quit();
	}

	// TODO settings page

	// TODO credits page
}
