using Misc;

namespace BakingGame
{
	public class ClickableTrash : ClickableInteraction
	{
		public override void OnClick(Clickable heldTool, Clickable heldIngredient)
		{
			if (heldIngredient == Clickable.None)
			{
				//Clicked trash with empty hands
				//Easteregg when??
				GameEvent.PickupTrash.Dispatch();
				GameEvent.PickupIngredient.Dispatch(Clickable.Ingredient_Trash);
				return;
			}
			
			GameEvent.TrashClicked.Dispatch();
		}
	}
}