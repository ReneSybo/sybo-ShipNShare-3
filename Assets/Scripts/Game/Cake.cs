using UnityEngine;

namespace BakingGame
{
	public class Cake
	{
		public void AddIngredient(Clickable tool, Clickable ingredient)
		{
			Debug.Log($"Used {tool} to add {ingredient}");
		}
	}
}