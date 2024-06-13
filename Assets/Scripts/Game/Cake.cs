using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace BakingGame
{
	public class Cake
	{
		Dictionary<Clickable, int> _recipeRequirements;

		public Cake()
		{
			_recipeRequirements = new Dictionary<Clickable, int>();
		}

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
			Debug.Log($"Used {tool} to add {ingredient}");

			if (ingredient == Clickable.Ingredient_Trash)
			{
				//Added trash to the cake!???!?!!?
				GameEvent.AddedTrash.Dispatch();
				return;
			}
			
			if (_recipeRequirements.TryGetValue(ingredient, out int remainingRequirement))
			{
				int changeAmount = EnumUtils.ToolToQuantity(tool, ingredient);
				remainingRequirement -= changeAmount;
				_recipeRequirements[ingredient] = remainingRequirement;

				if (remainingRequirement < 0)
				{
					Debug.Log("BUT IT WAS TOO MUCH!!!");
					GameEvent.AddedTooMuchIngredient.Dispatch(ingredient);
					GameEvent.GameLose.Dispatch();
					return;
				}
			}
			else
			{
				Debug.Log("BUT IT WAS NOT NEEDED!!!");
				GameEvent.AddedWrongIngredient.Dispatch(ingredient);
				GameEvent.GameLose.Dispatch();
				return;
			}
			
			GameEvent.AddedIngredient.Dispatch(ingredient);
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
	}
}