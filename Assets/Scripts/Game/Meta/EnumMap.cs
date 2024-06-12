using System;
using UnityEngine;

namespace BakingGame
{
	[CreateAssetMenu(fileName = "EnumMapper", menuName = "KITCHEN/EnumMap", order = 0)]
	public class EnumMap : ScriptableObject
	{
		public Sprite MissingSprite;
		public ClickableEntry[] Tools;
		public ClickableEntry[] Ingredients;
		public ClickableEntry[] Room;
		
		public FillableEntry[] Filled;

		public Sprite GetSprite(Clickable clickable)
		{
			foreach (ClickableEntry entry in Tools)
			{
				if (entry.ClickableType == clickable)
				{
					return entry.Sprite;
				}
			}
			
			foreach (ClickableEntry entry in Ingredients)
			{
				if (entry.ClickableType == clickable)
				{
					return entry.Sprite;
				}
			}

			foreach (ClickableEntry entry in Room)
			{
				if (entry.ClickableType == clickable)
				{
					return entry.Sprite;
				}
			}

			return MissingSprite;
		}

		public Sprite GetSprite(Clickable tool, Clickable ingredient)
		{
			foreach (FillableEntry entry in Filled)
			{
				if (entry.RequiredTool == tool && entry.RequiredIngredient == ingredient)
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
	
	[Serializable]
	public class FillableEntry
	{
		public Sprite Sprite;
		public Clickable RequiredTool;
		public Clickable RequiredIngredient;
	}
}