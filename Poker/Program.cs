using Microsoft.Extensions.DependencyInjection;
using Poker.Core;
using Poker.Core.Models;
using Poker.Infrastructure.Repositories;
using Poker.Infrastructure.Workflow;
using System;
using System.Linq;

namespace Poker
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			// Setup dependency injection for services
			var serviceProvider = new ServiceCollection()
				.AddSingleton<IPlayerRepository, PlayerRepository>()
				.AddTransient<IGameManager, GameManager>()
				.BuildServiceProvider();

			var playerRepository = serviceProvider.GetService<IPlayerRepository>();
			var gameManager = serviceProvider.GetService<IGameManager>();

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
					playerRepository.Add(ParseInput(input));
				}
			} while (moreInput);

			(var winners, var hand) = gameManager.GetWinners(playerRepository.GetAll());

			Console.WriteLine();
			Console.WriteLine($"Winner{(winners.Count > 1 ? "s" : "")} with {hand.GetDescription()}:");

			foreach (var player in winners)
			{
				PrintPlayer(player);
			}

			Console.WriteLine();
			Console.Write("Press any key to continue...");
			Console.ReadKey(true);
		}

		/// <summary>
		/// Parse the input from the Console and create a new <see cref="Player"/> from the input.
		/// </summary>
		/// <param name="input">Inputted data from the Console</param>
		/// <returns>Parsed <see cref="Player"/></returns>
		private static Player ParseInput(string input)
		{
			var parsedInput = input.Split(", ");

			// First value entered should always be the player's name
			Player newPlayer = new Player
			{
				PlayerName = parsedInput[0]
			};

			// Hands will always be 5 cards
			for (int i = 1; i <= 5; i++)
			{
				// Use ToUpper to ensure consistency in data entered
				var rank = parsedInput[i].Substring(0, parsedInput[i].Length - 1).ToUpper();
				var suit = parsedInput[i].Substring(parsedInput[i].Length - 1, 1).ToUpper();

				newPlayer.Cards.Add(new Card
				{
					Rank = rank,
					Suit = suit
				});
			}

			// Order cards by rank to make it easier to read and parse
			newPlayer.Cards = newPlayer.Cards.OrderBy(card => card.RankValue).ToList();

			return newPlayer;
		}

		/// <summary>
		/// Prints the player to the <see cref="Console"/>.
		/// </summary>
		/// <param name="player">Player to print</param>
		public static void PrintPlayer(Player player)
		{
			Console.Write($"  {player.PlayerName}: ");

			string cardList = string.Join(", ", player.Cards.Select(card => $"{card.Rank}{card.Suit}"));
			Console.WriteLine(cardList);
		}
	}
}