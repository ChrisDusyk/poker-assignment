using Poker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Infrastructure.Repositories
{
	public interface IPlayerRepository
	{
		List<Player> GetAll();

		Player GetByName(string playerName);

		void Add(Player player);
	}
}
