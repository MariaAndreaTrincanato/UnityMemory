using Assets.Scripts;
using Assets.Scripts.Models;
using static Assets.Scripts.Helpers.Enums;

namespace Services.Tests
{
	public class CardSignServiceTests
	{
		[Fact]
		public void Signs_ok()
		{
			// Arrange
			List<CardModel> cards = new();
			Dictionary<CardSignsEnum, int> signsDictionary = new();
			Dictionary<int, CardSignsEnum> signIndexesDictionary = new();

			// Act
			for (int i = 0; i < 12; i++)
			{
				var ret = CardSignsService.GetNewCardSign(signsDictionary, signIndexesDictionary);
			}
			
			// Assert

		}
	}
}