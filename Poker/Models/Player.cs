using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Models
{
	public class Player
	{
		public string PlayerName { get; set; }

		public List<Card> Cards { get; set; } = new List<Card>();
	}
}
