using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace BakingGame
{
	public class Cake
	{
		public static Cake CurrentCake;
		
		Dictionary<Clickable, int> _recipeRequirements;
		Dictionary<Clickable, int> _addedIngredients;

		public Cake()
		{
			CurrentCake = this;
			_recipeRequirements = new Dictionary<Clickable, int>();
			_addedIngredients = new Dictionary<Clickable, int>();
		}

		public Dictionary<Clickable, int> RecipeRequirements => _recipeRequirements;

		public void SetRecipe(Recipe recipe)
		{
			foreach (IngredientAmount ingredientAmount in recipe.Ingredients)
			{
				_recipeRequirements.TryGetValue(ingredientAmount.Ingredient, out int currentAmount);
				currentAmount += ingredientAmount.Amount;

				_recipeRequirements[ingredientAmount.Ingredient] = currentAmount;
			}
		}
		
		public void AddIngredient(Clickable tool, Clickable ingredient)
		{
			if (ingredient == Clickable.Ingredient_Trash)
			{
				AddTo(ingredient, 1);
				//Added trash to the cake!???!?!!?
				GameEvent.AddedIngredient.Dispatch(ingredient);
				return;
			}
			
			if (_recipeRequirements.TryGetValue(ingredient, out int remainingRequirement))
			{
				int changeAmount = EnumUtils.ToolToQuantity(tool, ingredient);
				remainingRequirement -= changeAmount;
				_recipeRequirements[ingredient] = remainingRequirement;
				AddTo(ingredient, changeAmount);

				if (remainingRequirement < 0)
				{
					GameEvent.AddedTooMuchIngredient.Dispatch(ingredient);
					// GameEvent.GameLose.Dispatch();
					return;
				}
			}
			else
			{
				GameEvent.AddedWrongIngredient.Dispatch(ingredient);
				// GameEvent.GameLose.Dispatch();
				return;
			}
			
			GameEvent.AddedIngredient.Dispatch(ingredient);
		}

		void AddTo(Clickable ingredient, int changeAmount)
		{
			_addedIngredients.TryGetValue(ingredient, out int amount);
			_addedIngredients[ingredient] = amount + changeAmount;
		}

		public void TryBake()
		{
			foreach (var kvPair in _recipeRequirements)
			{
				if (kvPair.Value != 0)
				{
					GameEvent.CakeWrongBake.Dispatch();
					GameEvent.GameLose.Dispatch();
					return;
				}
			}
			
			GameEvent.CakeCorrectBake.Dispatch();
			GameEvent.GameWin.Dispatch();
		}

		public Recipe CurrentAsRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Ingredients = new IngredientAmount[_addedIngredients.Count];

			int i = 0;
			foreach (var kvPair in _addedIngredients)
			{
				recipe.Ingredients[i] = new IngredientAmount
				{
					Amount = kvPair.Value,
					Ingredient = kvPair.Key,
				};

				i++;
			}
			
			return recipe;
		}
	}
}