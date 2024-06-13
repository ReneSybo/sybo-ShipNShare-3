using Misc;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BakingGame
{
	public class LadyCommentator : MonoBehaviour
	{
		public TMP_Text Textfield;
		public CanvasGroup CanvasGroup;
		public float FadeSpeed;
		public float ShowTime;
		
		float _showTimeRemaining = 0;
		bool _increasing = false;
		float _currentAlpha = 0f;

		void Awake()
		{
			Textfield.text = "";
			_increasing = false;
			_showTimeRemaining = 0f;
			
			GameEvent.PickupTrash.AddListener(() => SetText("Why.. Why You put your hands in there?"));
			GameEvent.AddedTrash.AddListener(() => SetText("I know your cake is trash.. But really?"));
			GameEvent.TrashClicked.AddListener(HandleTrashedItem);
			GameEvent.PickupTool.AddListener(HandlePickupTool);
			GameEvent.PickupIngredient.AddListener(HandlePickupIngredients);
			GameEvent.AddedIngredient.AddListener(HandleAddedIngredients);
			GameEvent.GameStart.AddListener(() => SetText("Okay fine, let's see if you can handle the pressure"));
			
			_increasing = false;
		}

		bool ShouldShow
		{
			get
			{
				if (_increasing)
				{
					return false;
				}
				
				if (_showTimeRemaining < -3f)
				{
					return Random.value > 0.2f;
				}
				
				return Random.value > 0.8f;
			}
		}

		void HandleAddedIngredients(Clickable ingredient)
		{
			if (ingredient == Clickable.Ingredient_Trash)
			{
				return;
			}
			
			if (ShouldShow)
			{
				SetText(RandomIngredientAdd(ingredient));
			}
		}

		string RandomIngredientAdd(Clickable ingredient)
		{
			string localizedName = RecipeText.ToLocalizedName(ingredient);
			string[] randomResponses = 
			{
				$"Are you seriously adding more {localizedName}?",
				"Oh, sure, because that’s definitely going to help",
				"Great. Another disaster in the making",
				"Congratulations, you just ruined it",
				"Just dump everything in and hope for the best, huh?",
				"This cake is so doomed...",
			};

			return RandomResponseFrom(randomResponses);
		}

		void HandlePickupIngredients(Clickable ingredient)
		{
			if (ingredient == Clickable.Ingredient_Trash)
			{
				return;
			}
			
			if (!ShouldShow)
			{
				return;
			}
			
			string localizedName = RecipeText.ToLocalizedName(ingredient);

			switch (ingredient)
			{

				case Clickable.Ingredient_Flour:
					SetText(RandomPickupFlour(localizedName));
					break;
				case Clickable.Ingredient_Sugar:
					SetText(RandomPickupSugar(localizedName));
					break;
				case Clickable.Ingredient_Eggs:
					SetText(RandomPickupEggs(localizedName));
					break;
				case Clickable.Ingredient_Milk:
					SetText(RandomPickupMilk(localizedName));
					break;
				case Clickable.Ingredient_Oil:
					SetText(RandomPickupBakingOil(localizedName));
					break;
				case Clickable.Ingredient_Cocoa:
					SetText(RandomPickupBakingCocoa(localizedName));
					break;
				case Clickable.Ingredient_Vanilla:
					SetText(RandomPickupBakingVanilla(localizedName));
					break;
				case Clickable.Ingredient_Butter:
					SetText(RandomPickupButter(localizedName));
					break;
				case Clickable.Ingredient_BakingSoda:
					SetText(RandomPickupBakingPowder(localizedName));
					break;
			}
		}

		string RandomPickupBakingOil(string localizedName)
		{
			string[] randomResponses = 
			{
				"Because this needs to be even greasier!",
				$"{localizedName} won’t fix this disaster",
				$"{localizedName}? Because your cake wasn't unhealthy enough",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupBakingCocoa(string localizedName)
		{
			string[] randomResponses = 
			{
				$"{localizedName}? Because you think you’re fancy?",
				$"{localizedName} won’t fix this disaster",
				$"Adding {localizedName}? Desperate times, huh",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupBakingVanilla(string localizedName)
		{
			string[] randomResponses = 
			{
				$"{localizedName}? What, are you trying to make this edible?",
				$"Sure, add some {localizedName}. Like that’ll help",
				$"{localizedName}? Trying to mask your mistakes, I see",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupBakingPowder(string localizedName)
		{
			string[] randomResponses = 
			{
				$"{localizedName}? Like you even know what it does",
				"Sure, add that. It’s not going to help",
				$"Oh, {localizedName}. You might as well be guessing",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupButter(string localizedName)
		{
			string[] randomResponses = 
			{
				$"{localizedName}? More like, 'Better not mess this up'",
				$"{localizedName}. Great, more fat to the fire",
				"Don’t make it even greasier than it’s going to be!",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupMilk(string localizedName)
		{
			string[] randomResponses = 
			{
				$"Adding {localizedName} now? Good luck with that",
				$"{localizedName}? Just what this disaster needed",
				"I bet the cow would be ashamed of having a part of this",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupEggs(string localizedName)
		{
			string[] randomResponses = 
			{
				"Like that’ll save your recipe",
				$"Crack those {localizedName} and probably your hopes of success too?",
				"I just know you will get shells in the mix..",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupSugar(string localizedName)
		{
			string[] randomResponses = 
			{
				$"{localizedName}? Because this cake definitely needs more sweetness",
				"Sweetening up your mistakes, are we?",
				"Try not to spill it everywhere. Oh wait, you will",
			};

			return RandomResponseFrom(randomResponses);
		}
		
		string RandomPickupFlour(string localizedName)
		{
			string[] randomResponses = 
			{
				$"{localizedName}? Let’s see how you mess this up",
				"Oh great, another potential disaster in your hands",
				"Be careful with that. Not that it’ll help",
			};

			return RandomResponseFrom(randomResponses);
		}

		void HandlePickupTool(Clickable tool)
		{
			if (tool == Clickable.Tool_Hand)
			{
				return;
			}

			if (!ShouldShow)
			{
				return;
			}
			
			switch (tool)
			{
				case Clickable.Tool_Cup_Whole:
					SetText(RandomToolPickupWhole());
					break;
				case Clickable.Tool_Cup_Half:
					SetText(RandomToolPickupHalf());
					break;
				case Clickable.Tool_Cup_Quarter:
					SetText(RandomToolPickupQuart());
					break;
				case Clickable.Tool_Teaspoon:
					SetText(RandomToolPickupTeaspoon());
					break;
			}
		}

		string RandomToolPickupTeaspoon()
		{
			string[] randomResponses = 
			{
				"Teaspoon? Oh, please. Like that’ll make a difference", 
				"A teaspoon of what, regret?",
				"So now you’re going for precision? Laughable",
			};

			return RandomResponseFrom(randomResponses);
		}
		
		string RandomToolPickupQuart()
		{
			string[] randomResponses = 
			{
				"Quarter cup? What is this, amateur hour?", 
				"You think you’re being precise with that tiny thing?",
				"Sure, use the smallest cup and pretend you know what you’re doing",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomToolPickupHalf()
		{
			string[] randomResponses = 
			{
				"You think that's precise? Think again", 
				"Half a cup of disaster coming right up",
				"Half a cup? Why not just guess..",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomToolPickupWhole()
		{
			string[] randomResponses = 
			{
				"The big one? Overconfident much?", 
				"That’s a lot of room for you to make mistakes",
				"Do you even need to take that much?",
			};

			return RandomResponseFrom(randomResponses);
		}

		void HandleTrashedItem(Clickable clickable)
		{
			if (clickable != Clickable.Ingredient_Trash)
			{
				SetText(RandomWastedResources());
			}
		}

		string RandomWastedResources()
		{
			string[] randomResponses = 
			{
				"Sure, throw that away again", 
				"No it's fine. I'll buy more later",
				"What has the environment ever done for us anyway?",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomResponseFrom(string[] responses)
		{
			return responses[(int)(responses.Length * Random.value)];
		}

		void SetText(string text)
		{
			_showTimeRemaining = ShowTime;
			Textfield.text = text;
			_increasing = true;
			_currentAlpha = 0f;
		}

		void Update()
		{
			if (_increasing)
			{
				_currentAlpha += Time.deltaTime * FadeSpeed;
			}
			else
			{
				_currentAlpha -= Time.deltaTime * FadeSpeed;
			}

			CanvasGroup.alpha = _currentAlpha;

			_showTimeRemaining -= Time.deltaTime;
			if (_showTimeRemaining <= 0)
			{
				_increasing = false;
			}
		}
	}
}