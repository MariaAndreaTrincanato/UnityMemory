using System.Collections.Generic;
using static Assets.Scripts.Helpers.Enums;

namespace Assets.Scripts
{
	public static class CardSignsService
	{
		public static CardSignsEnum GetNewCardSign(Dictionary<CardSignsEnum, int> usedSignsDict, Dictionary<int, CardSignsEnum> signIndexesDict)
		{
			// TODO

			// 1. Use signs with 0 cards assigned

			// 2. Use signs with 1 card assigned, ensure distance

			return CardSignsEnum.Hearts;
		}
	}
}