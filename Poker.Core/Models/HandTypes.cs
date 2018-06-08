using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Poker.Core.Models
{
	public enum HandTypes
	{
		[Description("High Card")]
		HighCard = 0,

		[Description("One Pair")]
		OnePair = 1,

		[Description("Three of a Kind")]
		ThreeOfAKind = 2,

		[Description("Flush")]
		Flush = 3
	};
}
