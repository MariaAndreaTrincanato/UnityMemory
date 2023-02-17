using Assets.Scripts;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Helpers.Enums;

public class CardsManager : MonoBehaviour
{
	private Dictionary<CardSignsEnum, int> SignsDictionary;
	private Dictionary<int, CardSignsEnum> SignIndexesDictionary;

	void Start()
	{
		SignsDictionary = new()
		{
			{ CardSignsEnum.Spades, 0 },
			{ CardSignsEnum.Clubs, 0 },
			{ CardSignsEnum.Hearts, 0 },
			{ CardSignsEnum.Diamonds, 0 },
			{ CardSignsEnum.Circles, 0 },
			{ CardSignsEnum.Stars, 0 }
		};

		GenerateCards();
	}

	private void GenerateCards()
	{
		var referencesGameObjects = GameObject.FindGameObjectsWithTag("CardPositionReference").ToList();
		var referenceCard = GameObject.FindGameObjectWithTag("ReferenceCardPrefab");

		for (int i = 0; i < referencesGameObjects.Count; i++)
		{
			var details = ComputeDetails();
			SignIndexesDictionary.Add(i, details.CardSign);
			var currentCard = Instantiate(referenceCard);
			currentCard.transform.SetParent(transform);
		}
	}

	private CardModel ComputeDetails()
	{
		CardSignsEnum sign = CardSignsService.GetNewCardSign(SignsDictionary, SignIndexesDictionary);

		var currentSignValue = SignsDictionary.GetValueOrDefault(sign);

		return new CardModel
		{
			CardSign = sign,
			//Id = $"{sign.GetDescription()}_{currentSignValue}",
			IsTurned = false,
			GameObject = null
		};
	}
}
