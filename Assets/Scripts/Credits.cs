using UnityEngine;

public class Credits : MonoBehaviour
{
	public Transform Panel { get; set; }
	public Transform Button { get; set; }
	public Transform Text { get; set; }

	private void Awake()
	{
		Panel = transform.GetChild(0);
		Button = transform.GetChild(2);
		Text = transform.GetChild(1);

		HideCreditsUI();
	}

	public void ShowCreditsUI()
	{
		if (Panel != null)
		{
			Panel.gameObject.SetActive(true);
		}

		if (Button != null)
		{
			Button.gameObject.SetActive(true);
		}

		if (Text != null)
		{
			Text.gameObject.SetActive(true);
		}
	}

	public void HideCreditsUI()
	{
		if (Panel != null)
		{
			Panel.gameObject.SetActive(false);
		}

		if (Button != null)
		{
			Button.gameObject.SetActive(false);
		}

		if (Text != null)
		{
			Text.gameObject.SetActive(false);
		}
	}

	public void OnCloseButtonClicked()
	{
		var gameStateManagerGameObject = GameObject.FindGameObjectWithTag("GameStateManager");
		if (gameStateManagerGameObject != null)
		{
			if (gameStateManagerGameObject.TryGetComponent<GameStateManager>(out var script))
			{
				script.PlayButtonSelectedSound();
				HideCreditsUI();
			}
		}
	}
}
