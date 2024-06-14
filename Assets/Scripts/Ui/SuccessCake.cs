using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BakingGame
{
	public class SuccessCake : MonoBehaviour
	{
		public Sprite[] Cakes;
		public Image Image;

		void OnEnable()
		{
			Image.sprite = Cakes[(int)(Cakes.Length * Random.value)];
		}
	}
}