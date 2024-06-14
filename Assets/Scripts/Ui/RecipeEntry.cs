using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BakingGame
{
	public class RecipeEntry : MonoBehaviour
	{
		public TMP_Text Text;
		public Toggle Toggle;

		string _clearText;

		void Awake()
		{
			GameEvent.GameStart.AddListener(HandleGameStart);
		}

		void HandleGameStart()
		{
			Toggle.isOn = false;
		}

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