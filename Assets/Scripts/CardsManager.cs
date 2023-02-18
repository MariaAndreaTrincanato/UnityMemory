using Assets.Scripts;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Helpers.Enums;

public class CardsManager : MonoBehaviour
{
	private List<CardPositionModel> CardPositions;

	void Start()
	{
		CardPositions = new();
		GenerateCards();
	}

	private void GenerateCards()
	{
		var referencesGameObjects = GameObject.FindGameObjectsWithTag("CardPositionReference").ToList();
		var referenceCard = GameObject.FindGameObjectWithTag("ReferenceCardPrefab");

		for (int i = 0; i < referencesGameObjects.Count; i++)
		{
			var details = ComputeDetails();
			CardPositions.Add(new CardPositionModel
			{
				CardSign = details.CardSign,
				Index = i
			});

			Vector3 currentReferencePosition = referencesGameObjects[i].transform.position;
			var currentCard = Instantiate(referenceCard);
			currentCard.transform.SetParent(transform);
			currentCard.transform.position = currentReferencePosition;

			if (currentCard.TryGetComponent<Card>(out var cardScriptReference)) 
			{
				cardScriptReference.CreateCardDetails(details);
			}
		}

		referenceCard.SetActive(false);
	}

	private CardModel ComputeDetails()
	{
		CardSignsEnum? sign = CardSignsService.GetNewCardSign(CardPositions);

		if (sign == null)
		{
			// TODO handle error
			Debug.LogWarning("[WARNING] card model is null");
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
