using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.Helpers.Enums;

namespace Assets.Scripts
{
	public static class CardSignsService
	{
		private static readonly IEnumerable<CardPlanetsEnum> SignsList = new List<CardPlanetsEnum>
		{
			CardPlanetsEnum.BluePlanet,
			CardPlanetsEnum.HeartPlanet,
			CardPlanetsEnum.YellowPlanet,
			CardPlanetsEnum.PurplePlanet,
			CardPlanetsEnum.SandPlanet,
			CardPlanetsEnum.RedPlanet
		};

		public static CardPlanetsEnum? GetNewCardSign(List<CardPositionModel> models)
		{
			var currentSign = GetRandomSign();
			bool hasMaxItems = models.Where(x => x.CardSign == currentSign).Count() == 2;

			while (models.Count < 12 && hasMaxItems)
			{
				currentSign = GetRandomSign();
				hasMaxItems = models.Where(x => x.CardSign == currentSign).Count() == 2;
			}

			return currentSign;
		}

		private static CardPlanetsEnum GetRandomSign()
		{
			Random rnd = new();
			int randomIndex = rnd.Next(0, 6);
			return SignsList.ElementAt(randomIndex);
		}

		public static void ShuffleMe<T>(this IList<T> list)
		{
			Random random = new();
			//int n = list.Count;

			for (int i = list.Count - 1; i > 1; i--)
			{
				int rnd = random.Next(i + 1);

				T value = list[rnd];
				list[rnd] = list[i];
				list[i] = value;
			}
		}
	}
}