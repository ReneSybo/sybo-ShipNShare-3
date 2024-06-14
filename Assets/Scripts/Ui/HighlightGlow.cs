using System;
using Misc;
using UnityEngine;
using UnityEngine.UI;

namespace BakingGame
{
	public class HighlightGlow : MonoBehaviour
	{
		public GlowType Type;
		public Animation Animation;
		public CanvasGroup CanvasGroup;

		void Awake()
		{
			CanvasGroup.alpha = 0;
			GameEvent.ShowGlow.AddListener(HandleShowGlow);
			GameEvent.GameStart.AddListener(HandleGameStart);
		}

		void HandleGameStart()
		{
			CanvasGroup.alpha = 0;
			Animation.Stop("GlowAnimation");
		}

		void HandleShowGlow(GlowType type)
		{
			if (Type == type)
			{
				Animation.Play("GlowAnimation");
			}
		}
	}

	public enum GlowType
	{
		Recipe,
		Cups,
		Done
	}
}