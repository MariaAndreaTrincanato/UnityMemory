using Assets.Scripts.Models;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	private int Points;
	private CardModel FirstCard;
	private CardModel SecondCard;
	private bool IsHypeActive;

	[SerializeField]
	private AudioClip CardTurnedAudio;

	// Start is called before the first frame update
	void Start()
	{
		InitializeGameState();
	}

	private void InitializeGameState()
	{
		Points = 0;
		FirstCard = null;
		SecondCard = null;
		IsHypeActive = false;
	}

	private void ResetSelectedCards()
	{
		FirstCard = null;
		SecondCard = null;

		// TODO trigger turn
	}

	public void SelectCard(CardModel card)
	{
		if (FirstCard == null) 
		{
			SelectFirstCard(card);
			return;
		}
		SelectSecondCard(card);
	}

	public void SelectFirstCard(CardModel card)
	{
		if (FirstCard != null)
		{
			// TODO handle same card
			Debug.LogWarning("[GAME WARNING] Same card selected");
			return;
		}

		FirstCard = card;
	}

	public void SelectSecondCard(CardModel card)
	{
		SecondCard = card;

		bool isMatch = EvaluateMatch();
		if (isMatch)
		{
			Points++;
			IsHypeActive = true;
			ResetSelectedCards();
			return;
		}

		IsHypeActive = false;
		ResetSelectedCards();
	}

	private bool EvaluateMatch()
	{
		return FirstCard.CardSign == SecondCard.CardSign;
	}

	public void PlayCardTurnSound()
	{
		var audioSource = transform.GetComponent<AudioSource>();
		audioSource.PlayOneShot(CardTurnedAudio);
	}
}
