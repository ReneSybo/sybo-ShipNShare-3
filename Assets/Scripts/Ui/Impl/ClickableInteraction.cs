using System;
using UnityEngine;

namespace BakingGame
{
	public abstract class ClickableInteraction : MonoBehaviour
	{
		public ClickableItem Item;
		
		public abstract void OnClick(Clickable heldTool, Clickable heldIngredient);

		void OnValidate()
		{
			if (Item == null)
			{
				Item = GetComponentInParent<ClickableItem>();
			}
		}
	}
}