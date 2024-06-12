using System;
using UnityEngine;

namespace BakingGame
{
	[CreateAssetMenu(fileName = "EnumMapper", menuName = "KITCHEN/EnumMap", order = 0)]
	public class EnumMap : ScriptableObject
	{
		public Sprite MissingSprite;
		public ClickableEntry[] Entries;

		public Sprite GetSprite(Clickable clickable)
		{
			foreach (ClickableEntry entry in Entries)
			{
				if (entry.ClickableType == clickable)
				{
					return entry.Sprite;
				}
			}

			return MissingSprite;
		}
	}

	[Serializable]
	public class ClickableEntry
	{
		public Sprite Sprite;
		public Clickable ClickableType;
	}
}