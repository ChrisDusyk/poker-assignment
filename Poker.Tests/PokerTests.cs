using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Core.Models;
using Poker.Infrastructure.Repositories;
using Poker.Infrastructure.Workflow;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Tests
{
	[TestClass]
	public class PokerTests
	{
		[TestMethod]
		public void TestFlushWins()
		{
			IPlayerRepository playerRepository = new PlayerRepository();
			IGameManager gameManager = new GameManager();

			// Create test data
			playerRepository.Add(new Player
			{
				PlayerName = "Joe",
				Cards = new List<Card>
				{
					new Card { Rank = "2", Suit = "H" },
					new Card { Rank = "4", Suit = "H" },
					new Card { Rank = "5", Suit = "H" },
					new Card { Rank = "6", Suit = "H" },
					new Card { Rank = "8", Suit = "H" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Bob",
				Cards = new List<Card>
				{
					new Card { Rank = "3", Suit = "C" },
					new Card { Rank = "3", Suit = "D" },
					new Card { Rank = "3", Suit = "S" },
					new Card { Rank = "8", Suit = "C" },
					new Card { Rank = "10", Suit = "D" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Sally",
				Cards = new List<Card>
				{
					new Card { Rank = "A", Suit = "C" },
					new Card { Rank = "10", Suit = "C" },
					new Card { Rank = "5", Suit = "C" },
					new Card { Rank = "4", Suit = "D" },
					new Card { Rank = "4", Suit = "S" }
				}
			});

			(var winners, var hand) = gameManager.GetWinners(playerRepository.GetAll());

			// Test that Joe won with a Flush
			Assert.AreEqual("Joe", winners.FirstOrDefault().PlayerName);
			Assert.AreEqual(HandTypes.Flush, hand);
		}

		[TestMethod]
		public void TestThreeOfKindWins()
		{
			IPlayerRepository playerRepository = new PlayerRepository();
			IGameManager gameManager = new GameManager();

			// Create test data
			playerRepository.Add(new Player
			{
				PlayerName = "Marge",
				Cards = new List<Card>
				{
					new Card { Rank = "2", Suit = "D" },
					new Card { Rank = "2", Suit = "S" },
					new Card { Rank = "J", Suit = "H" },
					new Card { Rank = "Q", Suit = "D" },
					new Card { Rank = "K", Suit = "D" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Bob",
				Cards = new List<Card>
				{
					new Card { Rank = "3", Suit = "C" },
					new Card { Rank = "3", Suit = "D" },
					new Card { Rank = "3", Suit = "S" },
					new Card { Rank = "8", Suit = "C" },
					new Card { Rank = "10", Suit = "D" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Sally",
				Cards = new List<Card>
				{
					new Card { Rank = "A", Suit = "C" },
					new Card { Rank = "10", Suit = "C" },
					new Card { Rank = "5", Suit = "C" },
					new Card { Rank = "4", Suit = "D" },
					new Card { Rank = "4", Suit = "S" }
				}
			});

			(var winners, var hand) = gameManager.GetWinners(playerRepository.GetAll());

			// Assert that Bob won with Three of a Kind
			Assert.AreEqual("Bob", winners.FirstOrDefault().PlayerName);
			Assert.AreEqual(HandTypes.ThreeOfAKind, hand);
		}

		[TestMethod]
		public void TestOnePairWins()
		{
			IPlayerRepository playerRepository = new PlayerRepository();
			IGameManager gameManager = new GameManager();

			// Create test data
			playerRepository.Add(new Player
			{
				PlayerName = "Devin",
				Cards = new List<Card>
				{
					new Card { Rank = "A", Suit = "D" },
					new Card { Rank = "2", Suit = "D" },
					new Card { Rank = "3", Suit = "D" },
					new Card { Rank = "7", Suit = "S" },
					new Card { Rank = "10", Suit = "H" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Maks",
				Cards = new List<Card>
				{
					new Card { Rank = "K", Suit = "H" },
					new Card { Rank = "Q", Suit = "D" },
					new Card { Rank = "10", Suit = "S" },
					new Card { Rank = "9", Suit = "D" },
					new Card { Rank = "7", Suit = "H" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Sally",
				Cards = new List<Card>
				{
					new Card { Rank = "A", Suit = "C" },
					new Card { Rank = "10", Suit = "C" },
					new Card { Rank = "5", Suit = "C" },
					new Card { Rank = "4", Suit = "D" },
					new Card { Rank = "4", Suit = "S" }
				}
			});

			(var winners, var hand) = gameManager.GetWinners(playerRepository.GetAll());

			// Assert that Sally won with One Pair
			Assert.AreEqual("Sally", winners.FirstOrDefault().PlayerName);
			Assert.AreEqual(HandTypes.OnePair, hand);
		}

		[TestMethod]
		public void TestHighCardWinner()
		{
			IPlayerRepository playerRepository = new PlayerRepository();
			IGameManager gameManager = new GameManager();

			// Create test data
			playerRepository.Add(new Player
			{
				PlayerName = "Devin",
				Cards = new List<Card>
				{
					new Card { Rank = "A", Suit = "D" },
					new Card { Rank = "2", Suit = "D" },
					new Card { Rank = "3", Suit = "D" },
					new Card { Rank = "7", Suit = "S" },
					new Card { Rank = "10", Suit = "H" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Maks",
				Cards = new List<Card>
				{
					new Card { Rank = "K", Suit = "H" },
					new Card { Rank = "Q", Suit = "D" },
					new Card { Rank = "10", Suit = "S" },
					new Card { Rank = "9", Suit = "D" },
					new Card { Rank = "7", Suit = "H" }
				}
			});

			(var winners, var hand) = gameManager.GetWinners(playerRepository.GetAll());

			// Assert that Devin won with the High Card
			Assert.AreEqual("Devin", winners.FirstOrDefault().PlayerName);
			Assert.AreEqual(HandTypes.HighCard, hand);
		}

		[TestMethod]
		public void TestMultipleOnePairWinners()
		{
			IPlayerRepository playerRepository = new PlayerRepository();
			IGameManager gameManager = new GameManager();

			// Create test data
			playerRepository.Add(new Player
			{
				PlayerName = "Devin",
				Cards = new List<Card>
				{
					new Card { Rank = "A", Suit = "D" },
					new Card { Rank = "2", Suit = "D" },
					new Card { Rank = "3", Suit = "D" },
					new Card { Rank = "7", Suit = "S" },
					new Card { Rank = "10", Suit = "H" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Maks",
				Cards = new List<Card>
				{
					new Card { Rank = "K", Suit = "H" },
					new Card { Rank = "Q", Suit = "D" },
					new Card { Rank = "10", Suit = "S" },
					new Card { Rank = "9", Suit = "D" },
					new Card { Rank = "7", Suit = "H" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Sally",
				Cards = new List<Card>
				{
					new Card { Rank = "A", Suit = "C" },
					new Card { Rank = "10", Suit = "C" },
					new Card { Rank = "5", Suit = "C" },
					new Card { Rank = "4", Suit = "D" },
					new Card { Rank = "4", Suit = "S" }
				}
			});

			playerRepository.Add(new Player
			{
				PlayerName = "Marge",
				Cards = new List<Card>
				{
					new Card { Rank = "2", Suit = "D" },
					new Card { Rank = "2", Suit = "S" },
					new Card { Rank = "J", Suit = "H" },
					new Card { Rank = "Q", Suit = "D" },
					new Card { Rank = "K", Suit = "D" }
				}
			});

			(var winners, var hand) = gameManager.GetWinners(playerRepository.GetAll());

			// Assert that Sally won with One Pair
			Assert.AreEqual(2, winners.Count);
			Assert.AreEqual(HandTypes.OnePair, hand);
		}
	}
}