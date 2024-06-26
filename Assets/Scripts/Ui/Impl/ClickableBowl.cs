﻿using Misc;

namespace BakingGame
{
	public class ClickableBowl : ClickableInteraction
	{
		public override void OnClick(Clickable heldTool, Clickable heldIngredient)
		{
			if (heldIngredient == Clickable.None)
			{
				//Clicked bowl with empty hands
				return;
			}
			
			GameEvent.BowlClicked.Dispatch();
		}
	}
}