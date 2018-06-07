using Poker.Models;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Repositories
{
	/// <summary>
	/// Repository for players entered in the game.
	/// </summary>
	public class PlayerRepository
	{
		/// <summary>
		/// List of active players entered by the user.
		/// </summary>
		private List<Player> _players = new List<Player>();

		/// <summary>
		/// Add player to the repository store.
		/// </summary>
		/// <param name="input">String data read from <see cref="Console"/> to parse into a <see cref="Player"/></param>
		public void AddPlayer(string input)
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
					RankValue = rank.GetRankValue(),
					Suit = suit
				});
			}

			// Order cards by rank to make it easier to read and parse
			newPlayer.Cards = newPlayer.Cards.OrderBy(card => card.RankValue).ToList();

			_players.Add(newPlayer);
		}

		/// <summary>
		/// Determines the winner(s) based on the hand hierarchy in the assignment.
		/// </summary>
		/// <returns>The List of winning players, and the hand rule that won</returns>
		public (List<Player>, string reason) GetWinners()
		{
			// Check for Flush as it's the winner
			var results = GetFlushResults();
			if (results.Count > 0)
			{
				return (results, "Flush");
			}

			// Three of a Kind is the next highest hand to implement
			results = GetThreeOfAKindResults();
			if (results.Count > 0)
			{
				return (results, "Three of a Kind");
			}

			// One pair is next highest hand to implement, after Three of a Kind
			results = GetOnePairResults();
			if (results.Count > 0)
			{
				return (results, "One Pair");
			}

			// If previous conditions have failed, test for the player(s) with the highest card rank
			return (GetHighCardResults(), "High Card");
		}

		/// <summary>
		/// Checks for a Flush in each player's hand by doing a Distinct operation on the Card suits. If there is only 1 distinct value returned, then all cards have the same suit.
		/// </summary>
		/// <returns>List of players with a Flush in their hand</returns>
		private List<Player> GetFlushResults()
		{
			return _players.Where(player => player.Cards.Select(card => card.Suit).Distinct().Count() == 1).ToList();
		}

		/// <summary>
		///	Check for Three of a Kind in each player's hand, by grouping by card rank and counting the number cards in each grouping.
		/// </summary>
		/// <returns>List of players with Three of a Kind in their hand</returns>
		private List<Player> GetThreeOfAKindResults()
		{
			// 1 grouping of 3 cards
			return GetRankGroupsCount(3, 1);
		}

		/// <summary>
		///Check for One Pair in each player's hand, by grouping by card rank and counting the number of cards in each grouping.
		/// </summary>
		/// <returns>List of players with One Pair in their hand</returns>
		private List<Player> GetOnePairResults()
		{
			// 1 grouping of 2 cards
			return GetRankGroupsCount(2, 1);
		}

		/// <summary>
		/// Count the number of groups where the count of items in the grouping is the cardsInGroup parameter.
		/// </summary>
		/// <param name="cardsInGroup">Cards in grouping (2, 3, 4 of a kind)</param>
		/// <param name="numberOfPairings">Number of groups to check (1 pair vs 2 pair)</param>
		/// <returns>List of players whose hands match the parameters</returns>
		private List<Player> GetRankGroupsCount(int cardsInGroup, int numberOfPairings)
		{
			return _players.Where(player => player.Cards.GroupBy(card => card.Rank).Count(group => group.Count() == cardsInGroup) == numberOfPairings).ToList();
		}

		/// <summary>
		/// Check for the player(s) with the highest rank card in their hand.
		/// </summary>
		/// <returns>List of players with the highest rank card</returns>
		private List<Player> GetHighCardResults()
		{
			Dictionary<Player, int> playerMaxRanks = new Dictionary<Player, int>();

			foreach (var player in _players)
			{
				int maxRank = player.Cards.Max(card => card.RankValue);
				playerMaxRanks.Add(player, maxRank);
			}

			var winners = playerMaxRanks.Where(map => map.Value == playerMaxRanks.Max(rank => rank.Value));
			return _players.Where(player => winners.Select(winner => winner.Key).Contains(player)).ToList();
		}
	}
}