using Misc;

namespace BakingGame
{
	public class ClickableTool : ClickableInteraction
	{
		public override void OnClick(Clickable heldTool, Clickable heldIngredient)
		{
			GameEvent.PickupTool.Dispatch(Item.Clickable);
		}
	}
}