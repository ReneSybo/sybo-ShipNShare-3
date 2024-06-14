using UnityEngine;

namespace BakingGame
{
	public class RecipeText : MonoBehaviour
	{
		public RecipeEntry[] TextEntries;

		public void SetRecipeText(Recipe recipe)
		{
			for (int i = 0; i < TextEntries.Length; i++)
			{
				TextEntries[i].gameObject.SetActive(false);
			}

			for (int i = 0; i < recipe.Ingredients.Length; i++)
			{
				IngredientAmount ingredientAmount = recipe.Ingredients[i];
				TextEntries[i].gameObject.SetActive(true);
				TextEntries[i].SetText($"{ToLocalizedName(ingredientAmount.Ingredient)}: {GetAmountFormat(ingredientAmount)}");
			}
		}

		public static string ToLocalizedName(Clickable ingredient)
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
				case Clickable.Ingredient_Trash:
					return "Trash";
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
			
			if (ingredient == Clickable.Ingredient_Trash)
			{
				return $"{amount} handful";
			}

			float cups = amount / 100f;
			if (amount % EnumUtils.AmountWhole == 0)
			{
				if (amount > 1)
				{
					return $"{cups:0} cup";
				}

				return $"{cups:0} cups";
			}

			if (amount % EnumUtils.AmountHalf == 0)
			{
				return $"{cups:0.0} cups";
			}
			
			return $"{cups:0.00} cups";
		}
	}
}