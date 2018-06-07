using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Core.Models
{
	/// <summary>
	/// Represents a card in the player's hand.
	/// </summary>
	public class Card
	{
		/// <summary>
		/// Card rank (2, 3, ..., Q, K, A)
		/// </summary>
		public string Rank { get; set; }

		/// <summary>
		/// Numeric value of the card rank for sorting and High Card checks
		/// </summary>
		public int RankValue { get; set; }

		/// <summary>
		/// Card suit (Heart, Diamond, Spade, Club)
		/// </summary>
		public string Suit { get; set; }
	}
}
