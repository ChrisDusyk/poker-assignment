using Poker.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Infrastructure.Workflow
{
	public class GameManager : IGameManager
	{
		public (List<Player> players, HandTypes handType) GetWinners(List<Player> players)
		{
			// Check for Flush as it's the winner
			var results = GetFlushResults(players);
			if (results.Count > 0)
			{
				return (results, HandTypes.Flush);
			}

			// Three of a Kind is the next highest hand to implement
			results = GetThreeOfAKindResults(players);
			if (results.Count > 0)
			{
				return (results, HandTypes.ThreeOfAKind);
			}

			// One pair is next highest hand to implement, after Three of a Kind
			results = GetOnePairResults(players);
			if (results.Count > 0)
			{
				return (results, HandTypes.OnePair);
			}

			// If previous conditions have failed, test for the player(s) with the highest card rank
			return (GetHighCardResults(players), HandTypes.HighCard);
		}

		/// <summary>
		/// Checks for a Flush in each player's hand by doing a Distinct operation on the Card suits. If there is only 1 distinct value returned, then all cards have the same suit.
		/// </summary>
		/// <returns>List of players with a Flush in their hand</returns>
		private List<Player> GetFlushResults(List<Player> players)
		{
			return players.Where(player => player.Cards.Select(card => card.Suit).Distinct().Count() == 1).ToList();
		}

		/// <summary>
		///	Check for Three of a Kind in each player's hand, by grouping by card rank and counting the number cards in each grouping.
		/// </summary>
		/// <returns>List of players with Three of a Kind in their hand</returns>
		private List<Player> GetThreeOfAKindResults(List<Player> players)
		{
			// 1 grouping of 3 cards
			return GetRankGroupsCount(players, 3, 1);
		}

		/// <summary>
		///Check for One Pair in each player's hand, by grouping by card rank and counting the number of cards in each grouping.
		/// </summary>
		/// <returns>List of players with One Pair in their hand</returns>
		private List<Player> GetOnePairResults(List<Player> players)
		{
			// 1 grouping of 2 cards
			return GetRankGroupsCount(players, 2, 1);
		}

		/// <summary>
		/// Count the number of groups where the count of items in the grouping is the cardsInGroup parameter.
		/// </summary>
		/// <param name="cardsInGroup">Cards in grouping (2, 3, 4 of a kind)</param>
		/// <param name="numberOfPairings">Number of groups to check (1 pair vs 2 pair)</param>
		/// <returns>List of players whose hands match the parameters</returns>
		private List<Player> GetRankGroupsCount(List<Player> players, int cardsInGroup, int numberOfPairings)
		{
			return players.Where(player => player.Cards.GroupBy(card => card.Rank).Count(group => group.Count() == cardsInGroup) == numberOfPairings).ToList();
		}

		/// <summary>
		/// Check for the player(s) with the highest rank card in their hand.
		/// </summary>
		/// <returns>List of players with the highest rank card</returns>
		private List<Player> GetHighCardResults(List<Player> players)
		{
			Dictionary<Player, int> playerMaxRanks = new Dictionary<Player, int>();

			foreach (var player in players)
			{
				int maxRank = player.Cards.Max(card => card.RankValue);
				playerMaxRanks.Add(player, maxRank);
			}

			var winners = playerMaxRanks.Where(map => map.Value == playerMaxRanks.Max(rank => rank.Value));
			return players.Where(player => winners.Select(winner => winner.Key).Contains(player)).ToList();
		}
	}
}