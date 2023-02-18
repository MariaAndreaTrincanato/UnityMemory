using Assets.Scripts.Models;
using UnityEngine;
using static Assets.Scripts.Helpers.Enums;

public class Card : MonoBehaviour
{
	private CardModel Details;
	private GameStateManager GameStateManager;
	private Transform CardContentChild;

	[SerializeField]
	private Sprite HeartsSprite;

	[SerializeField]
	private Sprite DiamondsSprite;

	[SerializeField]
	private Sprite CirclesSprite;

	[SerializeField]
	private Sprite SpadesSprite;

	[SerializeField]
	private Sprite ClubsSprite;

	[SerializeField]
	private Sprite StarsSprite;


	private void Awake()
	{
		var stateReferenceGameObject = GameObject.FindGameObjectWithTag("GameStateManager");
		if (stateReferenceGameObject != null)
		{
			GameStateManager = stateReferenceGameObject.GetComponent<GameStateManager>();
		}
		Details = new();
		CardContentChild = transform.GetChild(0);
	}

	public void CreateCardDetails(CardModel details)
	{
		Details = details;
		AssignSprite(details.CardSign);
	}

	public void TurnCard()
	{
		if (CardContentChild != null)
		{
			CardContentChild.Rotate(new Vector3(0, 1, 0), 90);
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

	private void AssignSprite(CardSignsEnum sign)
	{
		if (CardContentChild != null && CardContentChild.gameObject.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
		{
			switch(sign) 
			{
				case CardSignsEnum.Spades:
					spriteRenderer.sprite = SpadesSprite; 
					break;
				case CardSignsEnum.Clubs:
					spriteRenderer.sprite = ClubsSprite;
					break;
				case CardSignsEnum.Stars:
					spriteRenderer.sprite = StarsSprite;
					break;
				case CardSignsEnum.Hearts:
					spriteRenderer.sprite = HeartsSprite;
					break;
				case CardSignsEnum.Diamonds:
					spriteRenderer.sprite = DiamondsSprite;
					break;
				case CardSignsEnum.Circles:
					spriteRenderer.sprite = CirclesSprite;
					break;
				default:
					break;
			}
		}
	}
}
