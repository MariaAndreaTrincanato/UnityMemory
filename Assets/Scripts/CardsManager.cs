using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Helpers.Enums;

public class CardsManager : MonoBehaviour
{
	private Dictionary<CardSignsEnum, int> signs;    

    void Start()
    {
		signs = new()
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
		var referencesGameObjects = GameObject.FindGameObjectsWithTag("Reference").ToList();
		var referenceCard = GameObject.FindGameObjectWithTag("ReferenceCardPrefab");

		for (int i = 0; i < referencesGameObjects.Count; i++)
		{
			var currentCard = Instantiate(referenceCard);
			currentCard.transform.SetParent(transform);
		}
	}

	private CardModel ComputeDetails()
	{
		CardSignsEnum sign = CardSignsEnum.Spades;

		// TODO: choice algorithm

		var currentSignValue = signs.GetValueOrDefault(sign);

		return new CardModel
		{
			CardSign = sign,
			//Id = $"{sign.GetDescription()}_{currentSignValue}",
			IsTurned = false,
			GameObject = null
		};
	}
}
