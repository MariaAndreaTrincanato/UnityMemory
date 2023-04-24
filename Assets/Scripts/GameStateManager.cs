using Assets.Scripts.Models;
using System;
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
	
	[SerializeField]
	private AudioClip CardsMatchedAudio;
	
	[SerializeField]
	private AudioClip ButtonSelectedAudio;

	public static event Action<int> PointsUpdated;
	public static event Action GameStarted;
	public static event Action<bool> GameEnded;

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

	#region card selection
	private void HideMatchedCards()
	{
		FirstCard.GameObject.SetActive(false);
		SecondCard.GameObject.SetActive(false);
		FirstCard = null;
		SecondCard = null;
		Mathf.Clamp(RemainingCards -= 2, 0, TotalCardsNumber);
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
			StartCoroutine(WaitToPlayMatchParticle(0.5f));
			StartCoroutine(WaitToRemove(1.0f));
			return;
		}

		IsHypeActive = false;
		StartCoroutine(WaitToTurn(0.8f));
	}

	private bool EvaluateMatch()
	{
		return FirstCard.CardSign == SecondCard.CardSign;
	}
	#endregion

	#region game flow
	public void StartGame()
	{
		GameStarted.Invoke();
		InitializeGameState();
		var cardsContainer = GameObject.FindGameObjectWithTag("CardsContainer");
		if (cardsContainer != null && cardsContainer.TryGetComponent<CardsManager>(out var cardsManagerScript))
		{
			cardsManagerScript.ResetGame();
		}
	}

	public void EndGame(bool won = true)
	{
		GameEnded.Invoke(won);
		//InitializeGameState();
		//var mainMenuObject = GameObject.FindGameObjectWithTag("MainMenu");
		//if (mainMenuObject != null && mainMenuObject.TryGetComponent<MainMenu>(out var mainMenuScript))
		//{
		//	mainMenuScript.InitializeGameUI();
		//}
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
		}
	}
	#endregion

	public void UpdateWarningMessage(string message)
	{
		Message = message;
	}

	public void HideGameElements()
	{
		var cardsContainer = GameObject.FindGameObjectWithTag("CardsContainer");
		if (cardsContainer != null && cardsContainer.TryGetComponent<CardsManager>(out var cardsManagerScript))
		{
			cardsManagerScript.HideGameRefElements();
		}
	}

	#region audios
	public void PlayCardTurnSound()
	{
		if (transform.TryGetComponent<AudioSource>(out var audioSource))
		{
			audioSource.PlayOneShot(CardTurnedAudio);
		}
	}

	public void PlayCardsMatchSound()
	{
		if (transform.TryGetComponent<AudioSource>(out var audioSource))
		{
			audioSource.PlayOneShot(CardsMatchedAudio);
		}
	}

	public void PlayButtonSelectedSound()
	{
		if (transform.TryGetComponent<AudioSource>(out var audioSource))
		{
			audioSource.PlayOneShot(ButtonSelectedAudio);
		}
	}
	#endregion

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
			PlayCardsMatchSound();
			Points++;
			PointsUpdated?.Invoke(Points);
		}
	}

	public IEnumerator WaitToHideMessage(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		//HideMatchedCards();
	}
	#endregion
}
