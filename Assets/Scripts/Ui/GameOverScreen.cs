using System;
using UnityEngine;

namespace BakingGame
{
	public class GameOverScreen : MonoBehaviour
	{
		public RecipeText RequiredRecipe;
		public RecipeText ActualRecipe;
		
		void OnEnable()
		{
			RequiredRecipe.SetRecipeText(RecipeMap.CurrentRecipe);
			ActualRecipe.SetRecipeText(Cake.CurrentCake.CurrentAsRecipe());
		}
	}
}