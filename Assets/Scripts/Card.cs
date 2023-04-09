using Assets.Scripts.Models;
using UnityEngine;
using static Assets.Scripts.Helpers.Enums;

public class Card : MonoBehaviour
{
	private CardModel Details;
	private GameStateManager GameStateManager;
	private Transform CardContentChild;

	[SerializeField]
	private Sprite BluePlanetSprite;

	[SerializeField]
	private Sprite HeartPlanetSprite;

	[SerializeField]
	private Sprite YellowPlanetSprite;

	[SerializeField]
	private Sprite PurplePlanetSprite;

	[SerializeField]
	private Sprite SandPlanetSprite;

	[SerializeField]
	private Sprite RedPlanetSprite;


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

	private void AssignSprite(CardPlanetsEnum sign)
	{
		if (CardContentChild != null && CardContentChild.gameObject.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
		{
			switch(sign) 
			{
				case CardPlanetsEnum.BluePlanet:
					spriteRenderer.sprite = BluePlanetSprite; 
					break;
				case CardPlanetsEnum.HeartPlanet:
					spriteRenderer.sprite = HeartPlanetSprite;
					break;
				case CardPlanetsEnum.YellowPlanet:
					spriteRenderer.sprite = YellowPlanetSprite;
					break;
				case CardPlanetsEnum.PurplePlanet:
					spriteRenderer.sprite = PurplePlanetSprite;
					break;
				case CardPlanetsEnum.SandPlanet:
					spriteRenderer.sprite = SandPlanetSprite;
					break;
				case CardPlanetsEnum.RedPlanet:
					spriteRenderer.sprite = RedPlanetSprite;
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
