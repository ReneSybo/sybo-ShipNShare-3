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
				"Well, that's one way to ruin it beyond repair?",
				"Just when I thought it couldn't get worse",
				"Congratulations, you've created the world's first garbage cake",
				"Why stop there? Just throw the whole trash can in",
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
				"Congratulations, you just ruined it",
				"Just dump everything in and hope for the best, huh?",
				"This cake is so doomed...",
			};

			return RandomResponseFrom(randomResponses);
		}

		void HandlePickupIngredients(Clickable ingredient)
		{
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
				"Congratulations, you’ve officially lost it",
				"Wonderful, just add garbage to your garbage cake",
				$"{localizedName}, huh? Finally, something that matches your skills",
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
				"Perfect, just throw it all away. That’ll fix everything", 
				"Finally, something you’re good at: wasting food",
				"Out with the trash, just like your baking skills",
				"Getting rid of evidence, are we?",
				"Why not just throw the whole cake in there while you’re at it?",
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
					break;
			}
		}

		string TextFromTutorialIndex()
		{
			switch (_tutorialIndex)
			{
				case TutorialIntro: return "Oh. It's you. The so-called baker?";
				case TutorialRecipe: return "Ugh fine. Here is your recipe";
				case TutorialTools: return "Use these measuring cups or whatever";
				case TutorialTellDone: return "Let me know when you THINK are done";
				case TutorialFinal: return "Good luck, I guess..";
			}

			return string.Empty;
		}
	}
}