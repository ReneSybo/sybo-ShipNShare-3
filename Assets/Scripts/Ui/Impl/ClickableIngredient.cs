using Misc;

namespace BakingGame
{
	public class ClickableIngredient : ClickableInteraction
	{
		public override void OnClick(Clickable heldTool, Clickable heldIngredient)
		{
			GameEvent.PickupIngredient.Dispatch(Item.Clickable);
		}
	}
}