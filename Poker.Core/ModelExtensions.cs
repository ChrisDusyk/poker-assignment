using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Core
{
	public static class ModelExtensions
	{
		/// <summary>
		/// Get the numeric value of the card rank. Assumes Aces are high.
		/// </summary>
		/// <param name="rank">Card rank</param>
		/// <returns>Integer value of the rank</returns>
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
	}
}
