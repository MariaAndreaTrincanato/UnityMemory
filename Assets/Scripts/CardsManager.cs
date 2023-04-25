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
		SetReferenceCardTransparentColorsAndCollider(ReferenceCard);
	}

	public void ResetGame()
	{
		RemoveCards();
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
			SetPlayableCardColorAndCollider(currentCard);
			currentCard.transform.SetParent(transform);
			currentCard.transform.position = currentReferencePosition;

			if (currentCard.TryGetComponent<Card>(out var cardScriptReference)) 
			{
				cardScriptReference.CreateCardDetails(details);
			}
		}
	}

	public void RemoveCards()
	{
		CardPositions?.Clear();
		if (transform.childCount > 1)
		{
			var count = transform.childCount;
			for (int i = count - 1; i > 0 ; i--)
			{
				var currentNode = transform.GetChild(i);
				if (currentNode != null)
				{
					Destroy(currentNode.gameObject);
				}
			}
		}
	}

	private CardModel ComputeDetails()
	{
		CardPlanetsEnum? sign = CardSignsService.GetNewCardSign(CardPositions);
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

	private void SetReferenceCardTransparentColorsAndCollider(GameObject referenceCard)
	{
		if (referenceCard != null) 
		{
			if (referenceCard.TryGetComponent<BoxCollider2D>(out var boxCollider))
			{
				boxCollider.enabled = false;
			}

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

	private void SetPlayableCardColorAndCollider(GameObject card)
	{
		if (card != null)
		{
			if (card.TryGetComponent<BoxCollider2D>(out var boxCollider))
			{
				boxCollider.enabled = true;
			}

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
