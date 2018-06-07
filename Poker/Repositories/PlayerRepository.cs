using Poker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Repositories
{
	public class PlayerRepository
	{
		private List<Player> _players = new List<Player>();

		public void AddPlayer(string input)
		{
			var parsedInput = input.Split(", ");

			Player newPlayer = new Player
			{
				PlayerName = parsedInput[0]
			};

			for (int i = 1; i <= 5; i++)
			{
				var rank = parsedInput[i].Substring(0, parsedInput[i].Length - 1).ToUpper();
				var suit = parsedInput[i].Substring(parsedInput[i].Length - 1, 1).ToUpper();

				newPlayer.Cards.Add(new Card
				{
					Rank = rank,
					RankValue = rank.GetRankValue(),
					Suit = suit
				});
			}

			_players.Add(newPlayer);
		}

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

		private List<Player> GetFlushResults()
		{
			return _players.Where(player => player.Cards.Select(card => card.Suit).Distinct().Count() == 1).ToList();
		}

		private List<Player> GetThreeOfAKindResults()
		{
			// Count the number of groups where the count of items in the grouping is 3 (1 grouping of 3 cards)
			return GetRankGroupsCount(3, 1);
		}

		private List<Player> GetOnePairResults()
		{
			// Count the number of groups where the count of items in the grouping is 2 (1 grouping of 2 cards)
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
