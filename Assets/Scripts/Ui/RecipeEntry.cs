using System;
using TMPro;
using UnityEngine;

namespace BakingGame
{
	public class RecipeEntry : MonoBehaviour
	{
		public TMP_Text Text;

		string _clearText;

		public void SetText(string text)
		{
			_clearText = text;
			Text.text = text;
		}

		public void ToggleChecked(bool toggled)
		{
			if (toggled)
			{
				Text.text = "<s>" + _clearText + "</s>";
			}
			else
			{
				Text.text = _clearText;
			}
		}
	}
}