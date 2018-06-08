using Poker.Core.Models;
using System.Collections.Generic;

namespace Poker.Infrastructure.Workflow
{
	public interface IGameManager
	{
		/// <summary>
		/// Determines the winner(s) based on the hand hierarchy in the assignment.
		/// </summary>
		/// <param name="players">List of players to check against</param>
		/// <returns></returns>
		(List<Player> players, HandTypes handType) GetWinners(List<Player> players);
	}
}