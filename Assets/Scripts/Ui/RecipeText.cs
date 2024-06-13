using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace BakingGame
{
	public class RecipeText : MonoBehaviour
	{
		public TMP_Text Textfield;

		public void SetRecipeText(Recipe recipe)
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine("Recipe:");
			foreach (IngredientAmount ingredientAmount in recipe.Ingredients)
			{
				builder.AppendLine($"{ToLocalizedName(ingredientAmount.Ingredient)}: {GetAmountFormat(ingredientAmount)}");
			}
			
			Textfield.text = builder.ToString();
		}

		string ToLocalizedName(Clickable ingredient)
		{
			switch (ingredient)
			{
				case Clickable.Ingredient_Flour:
					return "Flour";
				case Clickable.Ingredient_Sugar:
					return "Sugar";
				case Clickable.Ingredient_Eggs:
					return "Eggs";
				case Clickable.Ingredient_Milk:
					return "Milk";
				case Clickable.Ingredient_Oil:
					return "Oil";
				case Clickable.Ingredient_Cocoa:
					return "Cocoa Powder";
				case Clickable.Ingredient_Vanilla:
					return "Vanilla";
				case Clickable.Ingredient_Butter:
					return "Butter";
				case Clickable.Ingredient_BakingSoda:
					return "Baking Powder";
			}

			return string.Empty;
		}

		string GetAmountFormat(IngredientAmount ingredientAmount)
		{
			Clickable ingredient = ingredientAmount.Ingredient;
			int amount = ingredientAmount.Amount;
			if (ingredient == Clickable.Ingredient_Eggs)
			{
				return $"{amount}";
			}
			
			if (ingredient == Clickable.Ingredient_Butter)
			{
				return $"{amount} stick";
			}

			if (ingredient == Clickable.Ingredient_Milk || ingredient == Clickable.Ingredient_Oil)
			{
				return $"{amount} mL";
			}

			return $"{amount} g";
		}
	}
}