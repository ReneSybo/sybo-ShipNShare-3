﻿using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BakingGame
{
	public class LadyCommentator : MonoBehaviour
	{
		public Sprite[] LadySprites;
		public Image LadyImage;
		
		public TMP_Text Textfield;
		public CanvasGroup CanvasGroup;
		public float FadeSpeed;
		public float ShowTime;
		
		float _showTimeRemaining = 0;
		bool _increasing = false;
		float _currentAlpha = 0f;

		bool _inTutorial = false;
		int _tutorialIndex = 0;
		bool _canTutorial;

		const int TutorialIntro = 1;
		const int TutorialRecipe = 2;
		const int TutorialTools = 3;
		const int TutorialTellDone = 4;
		const int TutorialFinal = 5;

		void Awake()
		{
			_canTutorial = true;
			_currentAlpha = 0f;
			Textfield.text = "";
			_increasing = false;
			_showTimeRemaining = 0f;
			CanvasGroup.alpha = _currentAlpha;
			
			GameEvent.TrashClicked.AddListener(HandleTrashedItem);
			GameEvent.PickupTool.AddListener(HandlePickupTool);
			GameEvent.ActuallyPickupIngredient.AddListener(HandlePickupIngredients);
			GameEvent.AddedIngredient.AddListener(HandleAddedIngredients);
			
			GameEvent.GameStart.AddListener(HandleGameStart);
			GameEvent.GameReset.AddListener(HandleGameReset);
			
			_increasing = false;
		}

		void HandleGameReset()
		{
			_canTutorial = true;
		}

		void HandleGameStart()
		{
			_currentAlpha = 0f;
			Textfield.text = "";
			_increasing = false;
			_showTimeRemaining = 0f;
			CanvasGroup.alpha = _currentAlpha;
			
			_inTutorial = _canTutorial;
			_tutorialIndex = 0;
			_canTutorial = false;
		}

		bool ShouldShow
		{
			get
			{
				//Always berate, never back down. Make your enemies fear your very existence!!
				return true;
				
				// if (_increasing)
				// {
				// 	return false;
				// }
				//
				// if (_showTimeRemaining < -3f)
				// {
				// 	return Random.value > 0.2f;
				// }
				//
				// return Random.value > 0.8f;
			}
		}

		void HandleAddedIngredients(Clickable ingredient)
		{
			if (ShouldShow)
			{
				if (ingredient == Clickable.Ingredient_Trash)
				{
					SetText(RandomTrashAdd(ingredient));
				}
				else
				{
					SetText(RandomIngredientAdd(ingredient));
				}
			}
		}

		string RandomTrashAdd(Clickable ingredient)
		{
			string localizedName = RecipeText.ToLocalizedName(ingredient);
			string[] randomResponses = 
			{
				"Well, that's one way to ruin it?",
				"Just when I thought you couldn't get any worse",
				"Congratulations, you've created the world's first garbage cake",
				"Why stop there? Just throw the whole trash can in",
				"Might as well, you can't fix it anyway",
				$"Wow, just wow. You’re actually adding {localizedName}?",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomIngredientAdd(Clickable ingredient)
		{
			string localizedName = RecipeText.ToLocalizedName(ingredient);
			string[] randomResponses = 
			{
				$"Are you seriously adding more {localizedName}?",
				"Oh, sure, because that’s definitely going to help",
				"Great. Another disaster in the making",
				"Aaaaaaand it's ruined",
				"You just dump everything in and hope for the best, huh?",
				"This cake is so doomed",
				"Are you even reading the recipe or are you just guessing?",
				"Oh, you think that’s the right amount? That's hilarious",
				"Sure, add that. Not like it can get any worse",
				"Is this a baking show or a comedy sketch?",
				"Is this a cake or an experiment gone wrong?",
				"Fantastic. Just when I thought it couldn't get worse?",
			};

			return RandomResponseFrom(randomResponses);
		}

		void HandlePickupIngredients(Clickable ingredient)
		{
			if (!ShouldShow)
			{
				return;
			}
			
			_inTutorial = false;
			
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
				case Clickable.Ingredient_Trash:
					SetText(RandomPickupTrash(localizedName));
					break;
			}
		}

		string RandomPickupTrash(string localizedName)
		{
			string[] randomResponses = 
			{
				$"Seriously? You're picking up {localizedName} now",
				"Oh great, you’ve resorted to dumpster diving",
				"You’ve officially lost it",
				$"Wonderful, just add {localizedName} to your garbage cake",
				$"{localizedName}? Finally, something that matches your skills",
			};

			return RandomResponseFrom(randomResponses);
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
				$"Oh, {localizedName}. Trying to cover up the mess?",
				$"Adding {localizedName}? Desperate times, huh?",
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
				$"Ah, {localizedName}. You're definitely just guessing",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupButter(string localizedName)
		{
			string[] randomResponses = 
			{
				$"{localizedName}? More like, 'Better not mess this up'",
				"You don't look like you need more fat in your cake",
				"Ugh. You're using your hands? That is so disgusting",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupMilk(string localizedName)
		{
			string[] randomResponses = 
			{
				$"Adding {localizedName} now? Good luck with that",
				$"{localizedName}? Just what this disaster needed",
				"I bet the cow would cry over this",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupEggs(string localizedName)
		{
			string[] randomResponses = 
			{
				"Like that’ll save your recipe",
				$"Crack those {localizedName} and probably your hopes of success too?",
				"I just know you will get shells in the mix",
				"This is not an omelet, you know that right?",
			};

			return RandomResponseFrom(randomResponses);
		}

		string RandomPickupSugar(string localizedName)
		{
			string[] randomResponses = 
			{
				$"{localizedName}? Because this cake definitely needs more sweetness",
				"Sweetening up your mistakes, are we?",
				"Are you trying to give people diabetes?",
			};

			return RandomResponseFrom(randomResponses);
		}
		
		string RandomPickupFlour(string localizedName)
		{
			string[] randomResponses = 
			{
				$"{localizedName}? Let’s see how you mess this up",
				"Oh great, another potential disaster in your hands",
				"Just spilling it all over? Not like it'll save your cake anyways",
				"Better watch out for clumps, clumpsy!",
			};

			return RandomResponseFrom(randomResponses);
		}

		void HandlePickupTool(Clickable tool)
		{
			if (tool == Clickable.Tool_Hand)
			{
				return;
			}

			_inTutorial = false;

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
				"Perfect, just throw it all away. That’ll fix everything", 
				"Finally, something you’re good at: wasting food",
				"Out with the trash, just like your baking skills",
				"Getting rid of the evidence, are we?",
				"Why not just throw the whole bowl in there while you’re at it?",
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

			if (Random.value > 0.2f)
			{
				Sprite nextSprite = LadySprites[(int)(LadySprites.Length * Random.value)];
				LadyImage.sprite = nextSprite;
			}
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
				TryShowTutorial();
			}
		}

		void TryShowTutorial()
		{
			if (_inTutorial)
			{
				_tutorialIndex++;
				if (_tutorialIndex <= TutorialFinal)
				{
					SetText(TextFromTutorialIndex());
					TryShowGlow();
					
					_showTimeRemaining *= 2;
				}
			}
		}

		void TryShowGlow()
		{
			switch (_tutorialIndex)
			{
				case TutorialRecipe:
					GameEvent.ShowGlow.Dispatch(GlowType.Recipe);
					break;
				case TutorialTools:
					GameEvent.ShowGlow.Dispatch(GlowType.Cups);
					break;
				case TutorialTellDone:
					GameEvent.ShowGlow.Dispatch(GlowType.Done);
					GameEvent.ShowBakeButton.Dispatch();
					break;
			}
		}

		string TextFromTutorialIndex()
		{
			switch (_tutorialIndex)
			{
				case TutorialIntro: return "Oh, it's you. The so-called baker?";
				case TutorialRecipe: return "Ugh fine. Here is your recipe. Do you know how to read?";
				case TutorialTools: return "You can use these measuring cups, if you know how";
				case TutorialTellDone: return "Let me know when you THINK you are done";
				case TutorialFinal: return "Good luck, you're going to need it";
			}

			return string.Empty;
		}
	}
}