using Assets.Scripts;
using Assets.Scripts.Models;
using static Assets.Scripts.Helpers.Enums;

namespace Services.Tests
{
	public class CardSignServiceTests
	{
		[Fact]
		public void Card_signs_ok_12()
		{
			// Arrange
			List<CardPositionModel> models = new();

			// Act
			for (int i = 0; i < 12; i++)
			{
				var newSign = CardSignsService.GetNewCardSign(models);
				if (newSign == null)
				{
					continue;
				}

				models.Add(new CardPositionModel
				{
					 CardSign = newSign.Value,
					 Index = i
				});
			}

			// Assert
			Assert.True(models.Count == 12);
			Assert.True(models.GroupBy(x => x.CardSign).Count() == 6);
			Assert.True(models.GroupBy(x => x.CardSign).All(x => x.Count() == 2));
		}
	}
}