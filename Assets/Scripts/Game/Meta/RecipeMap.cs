using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BakingGame
{
	[CreateAssetMenu(fileName = "RecipeMap", menuName = "KITCHEN/RecipeMap", order = 1)]
	public class RecipeMap : ScriptableObject
	{
		public Recipe[] Recipes;

		public Recipe GetRandomRecipe()
		{
			return Recipes[(int)(Random.value * Recipes.Length)];
		}
		
		public Recipe GenerateRandomRecipe()
		{
			Recipe recipe = new Recipe();

			List<Clickable> ingredients = EnumUtils.GetIngredientsForRecipe();
			int ingredientsToAdd = ingredients.Count;

			recipe.Ingredients = new IngredientAmount[ingredientsToAdd];
			for (int i = 0; i < ingredientsToAdd; i++)
			{
				Clickable randomIngredient = GetRandomIngredient(ingredients);
				int randomAmount = GetRandomAmount(randomIngredient);
				
				IngredientAmount amount = new IngredientAmount
				{
					Ingredient = randomIngredient,
					Amount = randomAmount,
				};
				recipe.Ingredients[i] = amount;
			}
			
			return recipe;
		}

		int GetRandomAmount(Clickable ingredient)
		{
			if (ingredient == Clickable.Ingredient_Eggs)
			{
				return Random.Range(1, 5);
			}
			
			if (ingredient == Clickable.Ingredient_Butter)
			{
				return Random.Range(1, 4);
			}
			
			if (ingredient == Clickable.Ingredient_Vanilla || ingredient == Clickable.Ingredient_BakingSoda)
			{
				return EnumUtils.ToolToQuantity(Clickable.Tool_Teaspoon) * Random.Range(1, 5);
			}
			
			return EnumUtils.ToolToQuantity(Clickable.Tool_Cup_Half) * Random.Range(1, 20);
		}

		Clickable GetRandomIngredient(List<Clickable> ingredients)
		{
			int index = (int)(ingredients.Count * Random.value);
			Clickable result = ingredients[index];
			ingredients.RemoveAt(index);
			
			return result;
		}
	}

	[Serializable]
	public class Recipe
	{
		public IngredientAmount[] Ingredients;
	}
	
	[Serializable]
	public class IngredientAmount
	{
		public Clickable Ingredient;
		public int Amount;
	}
}