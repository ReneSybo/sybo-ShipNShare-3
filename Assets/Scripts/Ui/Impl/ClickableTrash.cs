using Misc;

namespace BakingGame
{
	public class ClickableTrash : ClickableInteraction
	{
		public override void OnClick(Clickable heldTool, Clickable heldIngredient)
		{
			if (heldIngredient == Clickable.None && heldTool == Clickable.Tool_Hand)
			{
				//Clicked trash with empty hands
				//Easteregg when??
				GameEvent.PickupIngredient.Dispatch(Clickable.Ingredient_Trash);
				return;
			}

			if (heldIngredient != Clickable.None)
			{
				GameEvent.TrashClicked.Dispatch(heldIngredient);
			}
		}
	}
}