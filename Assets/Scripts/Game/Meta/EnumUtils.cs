using System.Collections.Generic;
using UnityEngine;

namespace BakingGame
{
	public static class EnumUtils
	{
		const int LowestTool = (int)Clickable.Tool_Lower_Bound;
		const int HighestTool = (int)Clickable.Tool_Upper_Bound;
		
		const int LowestIngredient = (int)Clickable.Ingredient_Lower_Bound;
		const int HighestIngredient = (int)Clickable.Ingredient_Upper_Bound;
		
		public static bool IsTool(Clickable clickable)
		{
			int asIntValue = (int)clickable;
			return asIntValue > LowestTool && asIntValue < HighestTool;
		}
		
		public static bool IsIngredient(Clickable clickable)
		{
			int asIntValue = (int)clickable;
			return asIntValue > LowestIngredient && asIntValue < HighestIngredient;
		}
		
		public static bool CanPickupWithHands(Clickable clickable)
		{
			return clickable == Clickable.Ingredient_Eggs || clickable == Clickable.Ingredient_Butter || clickable == Clickable.Ingredient_Trash;
		}
		
		public static bool IsLiquid(Clickable clickable)
		{
			return clickable == Clickable.Ingredient_Milk || clickable == Clickable.Ingredient_Oil;
		}
		
		public const int AmountWhole = 100;
		public const int AmountHalf = 50;
		public const int AmountQuat = 25;
		public const int AmountTeaspoon = 5;

		public static int ToolToQuantity(Clickable clickable, Clickable ingredient)
		{
			if (!IsTool(clickable))
			{
				return 0;
			}

			if (CanPickupWithHands(ingredient))
			{
				return 1;
			}
			
			switch (clickable)
			{
				case Clickable.Tool_Cup_Whole: return AmountWhole;
				case Clickable.Tool_Cup_Half: return AmountHalf;
				case Clickable.Tool_Cup_Quarter: return AmountQuat;
				case Clickable.Tool_Teaspoon: return AmountTeaspoon;
			}

			if (clickable == Clickable.Ingredient_Trash)
			{
				return 1;
			}

			return 0;
		}

		public static List<Clickable> GetIngredientsForRecipe()
		{
			List<Clickable> list = new List<Clickable>()
			{
				Clickable.Ingredient_Butter,
				Clickable.Ingredient_Eggs,
				Clickable.Ingredient_Flour,
				Clickable.Ingredient_Milk,
				Clickable.Ingredient_Oil,
				Clickable.Ingredient_Sugar,
				Clickable.Ingredient_Vanilla,
				Clickable.Ingredient_BakingSoda,
			};

			if (Random.value > 0.5f)
			{
				list.Add(Clickable.Ingredient_Cocoa);
			}

			return list;
		}
	}
}