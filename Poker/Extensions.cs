using System;
using System.Collections.Generic;
using System.Text;
using Poker.Models;

namespace Poker
{
	public static class Extensions
	{
		public static int GetRankValue(this string rank)
		{
			if (Int32.TryParse(rank, out int result))
			{
				return result;
			}
			else
			{
				// Handle non-numeric card ranks
				switch (rank)
				{
					case "J":
						return 11;
					case "Q":
						return 12;
					case "K":
						return 13;
					case "A":
						return 14;
					default:
						return 0;
				}
			}
		}

		public static void PrintPlayer(this Player player)
		{
			Console.WriteLine($"{player.PlayerName}:");
			Console.Write("  ");

			foreach (var card in player.Cards)
			{
				Console.Write($"{card.Rank}{card.Suit} ");
			}
			Console.WriteLine();

		}
	}
}
