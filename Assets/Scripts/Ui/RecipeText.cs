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
				builder.AppendLine($"{ingredientAmount.Ingredient} - {GetAmountFormat(ingredientAmount)}");
			}
			
			Textfield.text = builder.ToString();
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