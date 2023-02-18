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

	void Start()
	{
		ReferencePositions = GameObject.FindGameObjectsWithTag("CardPositionReference").ToList();
		ReferenceCard = GameObject.FindGameObjectWithTag("ReferenceCardPrefab");
	}

	public void ResetGame()
	{
		CardPositions.Clear();
		ReferenceCard.SetActive(true);
		ReferencePositions.ForEach(o => o.SetActive(true));
		GenerateCards();
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
			currentCard.transform.SetParent(transform);
			currentCard.transform.position = currentReferencePosition;

			if (currentCard.TryGetComponent<Card>(out var cardScriptReference)) 
			{
				cardScriptReference.CreateCardDetails(details);
			}
		}

		ReferenceCard.SetActive(false);
		ReferencePositions.ForEach(o => o.SetActive(false));
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
}
