using System.Collections.Generic;

namespace Poker.Core.Models
{
	/// <summary>
	/// Represents a poker player with a hand of cards.
	/// </summary>
	public class Player
	{
		/// <summary>
		/// Name of the player
		/// </summary>
		public string PlayerName { get; set; }

		/// <summary>
		/// Player's hand
		/// </summary>
		public List<Card> Cards { get; set; } = new List<Card>();
	}
}