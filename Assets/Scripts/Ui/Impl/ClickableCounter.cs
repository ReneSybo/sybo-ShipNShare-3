using Misc;

namespace BakingGame
{
	public class ClickableCounter : ClickableInteraction
	{
		public override void OnClick(Clickable heldTool, Clickable heldIngredient)
		{
			if (heldTool == Clickable.None)
			{
				return;
			}

			if (heldIngredient != Clickable.None)
			{
				//Trying to put down a full tool!?!???!?!
				return;
			}
			
			GameEvent.PutDownTool.Dispatch();
		}
	}
}