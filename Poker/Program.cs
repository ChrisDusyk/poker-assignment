using System;
using System.Collections.Generic;
using Poker.Models;
using Poker.Repositories;

namespace Poker
{
	class Program
	{
		static void Main(string[] args)
		{
			var playerRepository = new PlayerRepository();
			bool moreInput = true;

			Console.WriteLine("Please enter players (enter q to continue):");

			do
			{
				var input = Console.ReadLine();

				if (input.Trim().Equals("q", StringComparison.OrdinalIgnoreCase))
				{
					moreInput = false;
				}
				else
				{
					playerRepository.AddPlayer(input);
				}
			} while (moreInput);

			(var winners, var reason) = playerRepository.GetWinners();

			Console.WriteLine();
			Console.WriteLine($"Winner{(winners.Count > 1 ? "s" : "")} with {reason}:");
			
			foreach (var player in winners)
			{
				player.PrintPlayer();
			}

			Console.WriteLine();
			Console.ReadKey(true);
		}
	}
}
