using Misc;
using UnityEngine;

namespace BakingGame
{
	public class KitchenRoom : MonoBehaviour
	{
		Clickable _currentlyHeldTool;
		Clickable _currentlyHeldIngredient;

		Cake _currentCake;
		
		public Camera Camera;
		public Canvas Canvas;
		
		void Awake()
		{
			_currentlyHeldTool = Clickable.None;
			_currentlyHeldIngredient = Clickable.None;
			GameEvent.ItemClicked.AddListener(HandleItemClicked);
			GameEvent.PickupTool.AddListener(HandleToolPickup);
			GameEvent.PickupIngredient.AddListener(HandleIngredientPickup);
			GameEvent.PutDownTool.AddListener(HandlePutDownTool);
			GameEvent.BowlClicked.AddListener(HandleBowlClicked);
			
			_currentCake = new Cake();
		}

		void HandleBowlClicked()
		{
			_currentCake.AddIngredient(_currentlyHeldTool, _currentlyHeldIngredient);
			_currentlyHeldIngredient = Clickable.None;
			HeldClickable.SetFilledIngredient(_currentlyHeldTool, _currentlyHeldIngredient);
		}

		void HandlePutDownTool()
		{
			if (_currentlyHeldTool != Clickable.None)
			{
				//You put down a tool
				HeldClickable.SetButtonState(true);
			}
			_currentlyHeldTool = Clickable.None;
		}

		void HandleIngredientPickup(Clickable ingredient)
		{
			if (_currentlyHeldIngredient != Clickable.None)
			{
				//You tried picking Flour, when already carrying Sugar!!!!???!!
				return;
			}

			_currentlyHeldIngredient = ingredient;
			HeldClickable.SetFilledIngredient(_currentlyHeldTool, _currentlyHeldIngredient);
		}

		void HandleToolPickup(Clickable tool)
		{
			if (_currentlyHeldIngredient != Clickable.None)
			{
				//You tried picking up a tool, while you are carrying Flour!???!?!?!?
				return;
			}
			
			HandlePutDownTool();

			_currentlyHeldTool = tool;
			HeldClickable.SetButtonState(false);
			HeldClickable.Transform.SetSiblingIndex(int.MaxValue);
		}

		void HandleItemClicked(Clickable item)
		{
			ClickableMap.Instance[item].ClickOn(_currentlyHeldTool, _currentlyHeldIngredient);
		}

		ClickableItem HeldClickable => ClickableMap.Instance[_currentlyHeldTool];

		void Update()
		{
			if (_currentlyHeldTool != Clickable.None)
			{
				Vector3 screenToWorldPoint = Camera.ScreenToWorldPoint(Input.mousePosition);
				screenToWorldPoint.z = Canvas.transform.position.z;
				HeldClickable.MoveTo(screenToWorldPoint);
			}
		}
	}
}