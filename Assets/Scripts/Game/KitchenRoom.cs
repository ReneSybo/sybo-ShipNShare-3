using System;
using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BakingGame
{
	public class KitchenRoom : MonoBehaviour
	{
		Clickable _currentlyHeldTool = Clickable.Tool_Hand;
		Clickable _currentlyHeldIngredient = Clickable.None;

		Cake _currentCake;
		
		public Button BakeButton;
		public RecipeMap RecipeMap;
		public Camera Camera;
		public Canvas Canvas;
		public RecipeText RecipeText;
		Vector3 _toolDelta;

		void Awake()
		{
			GameEvent.ItemClicked.AddListener(HandleItemClicked);
			GameEvent.PickupTool.AddListener(HandleToolPickup);
			GameEvent.PickupIngredient.AddListener(HandleIngredientPickup);
			GameEvent.PutDownTool.AddListener(HandlePutDownTool);
			GameEvent.BowlClicked.AddListener(HandleBowlClicked);
			GameEvent.TrashClicked.AddListener(HandleTrashClicked);
			GameEvent.GameStart.AddListener(HandleStartGame);
			GameEvent.GameReset.AddListener(HandleStartReset);
			BakeButton.onClick.AddListener(HandleBakeClicked);
		}

		void Start()
		{
			HandleStartGame();
		}

		void HandleStartReset()
		{
			HandlePutDownTool();
			
			ClickableMap.Instance[Clickable.Tool_Teaspoon].ResetPosition();
			ClickableMap.Instance[Clickable.Tool_Cup_Whole].ResetPosition();
			ClickableMap.Instance[Clickable.Tool_Cup_Half].ResetPosition();
			ClickableMap.Instance[Clickable.Tool_Cup_Quarter].ResetPosition();
		}

		void HandleStartGame()
		{
			HandlePutDownTool();
			ClickableMap.Instance[Clickable.Tool_Teaspoon].ResetPosition();
			ClickableMap.Instance[Clickable.Tool_Cup_Whole].ResetPosition();
			ClickableMap.Instance[Clickable.Tool_Cup_Half].ResetPosition();
			ClickableMap.Instance[Clickable.Tool_Cup_Quarter].ResetPosition();
			
			Recipe randomRecipe = RecipeMap.GenerateRandomRecipe();
			_currentCake = new Cake();
			_currentCake.SetRecipe(randomRecipe);
			RecipeText.SetRecipeText(randomRecipe);

			_currentlyHeldTool = Clickable.Tool_Hand;
			_currentlyHeldIngredient = Clickable.None;
		}

		void HandleBakeClicked()
		{
			_currentCake.TryBake();
		}

		void HandleTrashClicked(Clickable item)
		{
			_currentlyHeldIngredient = Clickable.None;
			HeldClickable.SetFilledIngredient(_currentlyHeldTool, _currentlyHeldIngredient);
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
			
			_toolDelta = Vector3.zero;
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
			GameEvent.ActuallyPickupIngredient.Dispatch(_currentlyHeldIngredient);
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
			
			Vector3 mouseClickPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
			Vector3 heldPosition = HeldClickable.Transform.position;

			_toolDelta = mouseClickPosition - heldPosition;
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
				screenToWorldPoint -= _toolDelta;
				screenToWorldPoint.z = Canvas.transform.position.z;
				HeldClickable.MoveTo(screenToWorldPoint);
			}
		}
	}
}