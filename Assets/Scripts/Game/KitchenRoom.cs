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
			_currentlyHeldTool = Clickable.Tool_Hand;
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
			if (_currentlyHeldTool != Clickable.Tool_Hand)
			{
				//You put down a tool
				HeldClickable.SetButtonState(true);
			}

			_currentlyHeldTool = Clickable.Tool_Hand;
			HeldClickable.SetButtonState(false);
			HeldClickable.Transform.SetSiblingIndex(int.MaxValue);
		}

		void HandleIngredientPickup(Clickable ingredient)
		{
			if (_currentlyHeldIngredient != Clickable.None)
			{
				//You tried picking Flour, when already carrying something!!!!???!!
				return;
			}

			bool canPickupWithHands = EnumUtils.CanPickupWithHands(ingredient);
			bool isUsingHands = _currentlyHeldTool == Clickable.Tool_Hand;
			if (canPickupWithHands != isUsingHands)
			{
				//You tried picking Flour with your hands!!?!?!
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

			if (_currentlyHeldTool != Clickable.Tool_Hand)
			{
				HandlePutDownTool();
			}

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