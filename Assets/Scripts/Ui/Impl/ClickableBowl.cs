using Misc;

namespace BakingGame
{
	public class ClickableBowl : ClickableInteraction
	{
		public override void OnClick(Clickable heldTool, Clickable heldIngredient)
		{
			GameEvent.BowlClicked.Dispatch();
		}
	}
}