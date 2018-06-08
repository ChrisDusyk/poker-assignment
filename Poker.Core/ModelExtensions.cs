using System;
using System.Reflection;

namespace Poker.Core
{
	public static class ModelExtensions
	{
		/// <summary>
		/// Get the numeric value of the card rank. Assumes Aces are high.
		/// </summary>
		/// <param name="rank">Card rank</param>
		/// <returns>Integer value of the rank</returns>
		public static int GetRankValue(this string rank)
		{
			if (Int32.TryParse(rank, out int result))
			{
				return result;
			}
			else
			{
				// Handle non-numeric card ranks
				switch (rank)
				{
					case "J":
						return 11;

					case "Q":
						return 12;

					case "K":
						return 13;

					case "A":
						return 14;

					default:
						return 0;
				}
			}
		}

		/// <summary>
		/// Gets the Description attribute from the enum value if it exists. Otherwise it returns the enum value as a string.
		/// </summary>
		public static string GetDescription(this Enum enumValue)
		{
			Type enumType = enumValue.GetType();
			MemberInfo[] memberInfo = enumType.GetMember(enumValue.ToString());

			if (memberInfo != null && memberInfo.Length > 0)
			{
				var attribute = memberInfo[0].GetCustomAttribute(typeof(System.ComponentModel.DescriptionAttribute), false);
				if (attribute != null)
				{
					return ((System.ComponentModel.DescriptionAttribute)attribute).Description;
				}
			}

			return enumValue.ToString();
		}
	}
}