using Misc;
using UnityEngine;

namespace BakingGame
{
	public class KitchenRoom : MonoBehaviour
	{
		Clickable _currentlyHeldTool;
		Clickable _currentlyHeldIngredient;
		
		public Camera Camera;
		public Canvas Canvas;
		
		void Awake()
		{
			_currentlyHeldTool = Clickable.None;
			_currentlyHeldIngredient = Clickable.None;
			GameEvent.ItemClicked.AddListener(HandleItemClicked);
			GameEvent.PickupTool.AddListener(HandleToolPickup);
			GameEvent.PickupIngredient.AddListener(HandleIngredientPickup);
		}

		void HandleIngredientPickup(Clickable ingredient)
		{
			if (_currentlyHeldIngredient != Clickable.None)
			{
				//You tried picking Flour, when already carrying Sugar!!!!???!!
				return;
			}

			_currentlyHeldIngredient = ingredient;
		}

		void HandleToolPickup(Clickable item)
		{
			if (_currentlyHeldIngredient != Clickable.None)
			{
				//You tried picking up a tool, while you are carrying Flour!???!?!?!?
				return;
			}
			
			if (_currentlyHeldTool != Clickable.None)
			{
				//You swap tool
				ClickableItem currentHeldItem = ClickableMap.Instance[_currentlyHeldTool];
				currentHeldItem.SetButtonState(true);
			}
			
			_currentlyHeldTool = item;
			ClickableItem newlyHeltItem = ClickableMap.Instance[_currentlyHeldTool];
			newlyHeltItem.SetButtonState(false);
			newlyHeltItem.Transform.SetSiblingIndex(int.MaxValue);
		}

		void HandleItemClicked(Clickable item)
		{
			ClickableMap.Instance[item].ClickOn(_currentlyHeldTool, _currentlyHeldIngredient);
		}

		void OnDestroy()
		{
			GameEvent.ItemClicked.RemoveListener(HandleItemClicked);
		}

		void Update()
		{
			if (_currentlyHeldTool != Clickable.None)
			{
				ClickableItem clickableItem = ClickableMap.Instance[_currentlyHeldTool];

				Vector3 screenToWorldPoint = Camera.ScreenToWorldPoint(Input.mousePosition);
				screenToWorldPoint.z = Canvas.transform.position.z;
				clickableItem.MoveTo(screenToWorldPoint);
			}
		}
	}
}