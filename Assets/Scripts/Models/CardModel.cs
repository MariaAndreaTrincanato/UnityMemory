using UnityEngine;
using static Assets.Scripts.Helpers.Enums;

namespace Assets.Scripts.Models
{
	public class CardModel
	{
		public string Id { get; set; }
		public CardSignsEnum CardSign { get; set; }
		public bool IsTurned { get; set; }
		public GameObject GameObject { get; set; }

	}
}
