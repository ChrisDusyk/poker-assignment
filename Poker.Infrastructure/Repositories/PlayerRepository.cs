using Poker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Infrastructure.Repositories
{
	/// <summary>
	/// Repository for players entered in the game.
	/// </summary>
	public class PlayerRepository : IPlayerRepository
	{
		/// <summary>
		/// List of active players entered by the user.
		/// </summary>
		private List<Player> _players = new List<Player>();

		public void Add(Player player)
		{
			_players.Add(player);
		}

		public List<Player> GetAll()
		{
			return _players;
		}

		public Player GetByName(string playerName)
		{
			return _players.FirstOrDefault(player => player.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase));
		}
	}
}