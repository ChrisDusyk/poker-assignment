using Poker.Core.Models;
using System.Collections.Generic;

namespace Poker.Infrastructure.Repositories
{
	public interface IPlayerRepository
	{
		/// <summary>
		/// Get all <see cref="Player"/>s from the store.
		/// </summary>
		List<Player> GetAll();

		/// <summary>
		/// Get <see cref="Player"/> by their name.
		/// </summary>
		/// <param name="playerName">Name to retrieve</param>
		Player GetByName(string playerName);

		/// <summary>
		/// Add player to the repository store.
		/// </summary>
		/// <param name="player"><see cref="Player"/> to add to the store</param>
		void Add(Player player);
	}
}