using Assets.Scripts.Models;
using UnityEngine;
using static Assets.Scripts.Helpers.Enums;

public class Card : MonoBehaviour
{
	private CardModel Details;
	private GameStateManager GameStateManager;

	private void Awake()
	{
		var stateReferenceGameObject = GameObject.FindGameObjectWithTag("GameStateManager");
		if (stateReferenceGameObject != null)
		{
			GameStateManager = stateReferenceGameObject.GetComponent<GameStateManager>();
		}

		CreateCardDetails(new CardModel
		{
			Id = "hearts_1",
			IsTurned = false,
			CardSign = CardSignsEnum.Hearts,
			GameObject = null
		});
	}

	public void CreateCardDetails(CardModel details)
	{
		Details = details;
	}

	public void TurnCard()
	{
		var cardItemToRotate = transform.GetChild(0);
		if (cardItemToRotate != null)
		{
			cardItemToRotate.Rotate(new Vector3(0, 1, 0), 90);
			Details.IsTurned = !Details.IsTurned;
		}
	}

	private void OnMouseDown()
	{
		if (GameStateManager != null) 
		{
			GameStateManager.PlayCardTurnSound();
			Details.GameObject = transform.gameObject;
			GameStateManager.SelectCard(Details);
			TurnCard();
		}
	}
}
