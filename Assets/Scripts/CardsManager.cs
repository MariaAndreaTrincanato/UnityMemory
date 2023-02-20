using Assets.Scripts;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Helpers.Enums;

public class CardsManager : MonoBehaviour
{
	private List<CardPositionModel> CardPositions;
	private List<GameObject> ReferencePositions;
	private GameObject ReferenceCard;

	void Awake()
	{
		CardPositions = new();
		ReferencePositions = new();
		ReferenceCard = new();
		HideGameRefElements();
	}

	public void HideGameRefElements()
	{
		ReferencePositions = GameObject.FindGameObjectsWithTag("CardPositionReference").ToList();
		ReferenceCard = GameObject.FindGameObjectWithTag("ReferenceCardPrefab");
		SetReferenceCardTransparentColors(ReferenceCard);
	}

	public void ResetGame()
	{
		CardPositions?.Clear();
		if (ReferenceCard != null && ReferencePositions != null)
		{
			GenerateCards();
		}
	}

	private void GenerateCards()
	{	
		for (int i = 0; i < ReferencePositions.Count; i++)
		{
			var details = ComputeDetails();
			CardPositions.Add(new CardPositionModel
			{
				CardSign = details.CardSign,
				Index = i
			});

			Vector3 currentReferencePosition = ReferencePositions[i].transform.position;
			var currentCard = Instantiate(ReferenceCard);
			SetPlayableCardColor(currentCard);
			currentCard.transform.SetParent(transform);
			currentCard.transform.position = currentReferencePosition;

			if (currentCard.TryGetComponent<Card>(out var cardScriptReference)) 
			{
				cardScriptReference.CreateCardDetails(details);
			}
		}
	}

	private CardModel ComputeDetails()
	{
		CardSignsEnum? sign = CardSignsService.GetNewCardSign(CardPositions);
		if (sign == null)
		{
			return null;
		}

		return new CardModel
		{
			CardSign = sign.Value,
			Id = $"{sign.Value.GetDescription()}_{CardPositions.Where(x => x.CardSign == sign).Count()}",
			IsTurned = false,
			GameObject = null
		};
	}

	private void SetReferenceCardTransparentColors(GameObject referenceCard)
	{
		if (referenceCard != null) 
		{
			var front = referenceCard.transform.GetChild(0);
			var back = referenceCard.transform.GetChild(1);

			if (back.TryGetComponent<SpriteRenderer>(out var backSpriteRenderer))
			{
				backSpriteRenderer.color = Color.clear;
			}

			if (front.TryGetComponent<SpriteRenderer>(out var frontSpriteRenderer))
			{
				frontSpriteRenderer.color = Color.clear;
			}
		}
	}

	private void SetPlayableCardColor(GameObject card)
	{
		if (card != null)
		{
			var front = card.transform.GetChild(0);
			var back = card.transform.GetChild(1);

			if (back.TryGetComponent<SpriteRenderer>(out var backSpriteRenderer))
			{
				backSpriteRenderer.color = Color.white;
			}

			if (front.TryGetComponent<SpriteRenderer>(out var frontSpriteRenderer))
			{
				frontSpriteRenderer.color = Color.white;
			}
		}
	}
}
