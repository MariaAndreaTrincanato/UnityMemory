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

	private void OnMouseDown()
	{
		if (GameStateManager != null && Details != null)
		{
			if (GameStateManager.FirstCard != null && GameStateManager.SecondCard != null)
			{
				return;
			}

			if (GameStateManager.FirstCard != null
					&& GameStateManager.FirstCard.CardSign == Details.CardSign
					&& GameStateManager.FirstCard.Id == Details.Id)
			{
				GameStateManager.UpdateWarningMessage("You cannot select the same card again");
				Debug.LogWarning("[GAME WARNING] Same card selected");
				return;
			}

			GameStateManager.PlayCardTurnSound();
			Details.GameObject = transform.gameObject;
			TurnCard();
			GameStateManager.SelectCard(Details);
		}
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

	public void ShowMatchEffect()
	{
		if (gameObject.TryGetComponent<ParticleSystem>(out var particleSystem)) 
		{
			particleSystem.Play();
		}
	}
}
