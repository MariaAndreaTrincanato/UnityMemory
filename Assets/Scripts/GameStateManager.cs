using Assets.Scripts.Helpers;
using Assets.Scripts.Models;
using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	private int Points;
	private bool IsHypeActive;
	private bool IsGameOver;
	private int RemainingCards;
	private int TotalCardsNumber;

	[HideInInspector]
	public string Message;

	[HideInInspector]
	public CardModel FirstCard;
	[HideInInspector]
	public CardModel SecondCard;
	
	[SerializeField]
	private AudioClip CardTurnedAudio;

	void Awake()
	{
		InitializeGameState();
	}

	private void InitializeGameState()
	{
		Points = 0;
		FirstCard = null;
		SecondCard = null;
		IsHypeActive = false;
		Message = string.Empty;
		IsGameOver = false;
		TotalCardsNumber = 12;
		RemainingCards = TotalCardsNumber;
	}

	private void ResetSelectedCards()
	{
		if (FirstCard.GameObject.TryGetComponent<Card>(out var firstCardScript)) 
		{
			firstCardScript.TurnCard();
			FirstCard = null;
		}

		var secondCardScript = SecondCard.GameObject.GetComponent<Card>();
		if (secondCardScript != null) 
		{ 
			secondCardScript.TurnCard();
			SecondCard = null;
		}
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

			StartCoroutine(WaitToPlayMatchParticle(1f));
			StartCoroutine(WaitToRemove(1.5f));
			return;
		}

		IsHypeActive = false;
		StartCoroutine(WaitToTurn(1f));
	}

	private bool EvaluateMatch()
	{
		return FirstCard.CardSign == SecondCard.CardSign;
	}

	public void PlayCardTurnSound()
	{
		if (transform.TryGetComponent<AudioSource>(out var audioSource)) 
		{
			audioSource.PlayOneShot(CardTurnedAudio);
		}
	}

	private void HideMatchedCards()
	{
		FirstCard.GameObject.SetActive(false);
		SecondCard.GameObject.SetActive(false);
		FirstCard = null;
		SecondCard = null;
		Mathf.Clamp(RemainingCards -= 2, 0, TotalCardsNumber);
	}

	public void UpdateWarningMessage(string message)
	{
		Message = message;
	}

	private void EvaluateGameOver()
	{
		IsGameOver = RemainingCards == 0;
		if (IsGameOver) 
		{ 
			var cardsContainer = GameObject.FindGameObjectWithTag("CardsContainer");
			if (cardsContainer != null && cardsContainer.TryGetComponent<CardsManager>(out var cardsManagerScript))
			{
				cardsManagerScript.HideGameRefElements();
			}

			//TODO: show game over screen based on points and/or timeout
			StartCoroutine(WaitToRestartGame(2));
		}
	}

	public void HideGameElements()
	{
		var cardsContainer = GameObject.FindGameObjectWithTag("CardsContainer");
		if (cardsContainer != null && cardsContainer.TryGetComponent<CardsManager>(out var cardsManagerScript))
		{
			cardsManagerScript.HideGameRefElements();
		}
	}

	public void StartGame()
	{
		InitializeGameState();
		var cardsContainer = GameObject.FindGameObjectWithTag("CardsContainer");
		if (cardsContainer != null && cardsContainer.TryGetComponent<CardsManager>(out var cardsManagerScript))
		{
			cardsManagerScript.ResetGame();
		}
	}

	public void EndGame()
	{
		InitializeGameState();
		var mainMenuObject = GameObject.FindGameObjectWithTag("MainMenu");
		if ( mainMenuObject != null && mainMenuObject.TryGetComponent<MainMenu>(out var mainMenuScript))
		{
			mainMenuScript.InitializeGameUI();
		}
	}

	#region coroutines
	public IEnumerator WaitToTurn(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		ResetSelectedCards();
		EvaluateGameOver();
	}

	public IEnumerator WaitToRemove(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		HideMatchedCards();
		EvaluateGameOver();
	}

	public IEnumerator WaitToPlayMatchParticle(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		if (SecondCard.GameObject.TryGetComponent<Card>(out var secondCardScript))
		{
			secondCardScript.ShowMatchEffect();
		}
	}

	public IEnumerator WaitToHideMessage(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		//HideMatchedCards();
	}

	public IEnumerator WaitToRestartGame(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		EndGame();
	}
	#endregion
}
