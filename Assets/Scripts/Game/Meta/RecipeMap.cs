using System;
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