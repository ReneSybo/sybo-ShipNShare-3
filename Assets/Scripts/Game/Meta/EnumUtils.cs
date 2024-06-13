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
			return clickable == Clickable.Ingredient_Eggs;
		}
	}
}