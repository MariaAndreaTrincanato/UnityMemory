using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Assets.Scripts.Helpers
{
	public static class Enums
	{
		public enum CardSignsEnum
		{
			[Description("Spades")]
			Spades,
			[Description("Clubs")]
			Clubs,
			[Description("Hearts")]
			Hearts,
			[Description("Diamonds")]
			Diamonds,
			[Description("Circles")]
			Circles,
			[Description("Stars")]
			Stars
		}

		public enum CardPlanetsEnum
		{
			[Description("BluePlanet")]
			BluePlanet,
			[Description("HeartPlanet")]
			HeartPlanet,
			[Description("YellowPlanet")]
			YellowPlanet,
			[Description("PurplePlanet")]
			PurplePlanet,
			[Description("SandPlanet")]
			SandPlanet,
			[Description("RedPlanet")]
			RedPlanet
		}

		public static string GetDescription<T>(this T e) where T : IConvertible
		{
			if (e is Enum)
			{
				Type type = e.GetType();
				Array values = Enum.GetValues(type);

				foreach (int val in values)
				{
					if (val == e.ToInt32(CultureInfo.InvariantCulture))
					{
						var memInfo = type.GetMember(type.GetEnumName(val));
						var descriptionAttribute = memInfo[0]
							.GetCustomAttributes(typeof(DescriptionAttribute), false)
							.FirstOrDefault() as DescriptionAttribute;

						if (descriptionAttribute != null)
						{
							return descriptionAttribute.Description;
						}
					}
				}
			}
			return string.Empty;
		}
	}
}
