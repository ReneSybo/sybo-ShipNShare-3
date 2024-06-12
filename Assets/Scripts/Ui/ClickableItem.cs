using Misc;
using UnityEngine;
using UnityEngine.UI;

namespace BakingGame
{
	public class ClickableItem : MonoBehaviour
	{
		public ClickableInteraction InteractionLogic;
		
		public RectTransform Transform;
		public EnumMap EnumMap;
		public Clickable Clickable;
		public Button Button;
		public Image Image;
		public Image FilledImage;
		
		void Awake()
		{
			Button.onClick.AddListener(HandleClicked);
			ClickableMap.Instance.Register(this);
		}

		public void ClickOn(Clickable heldTool, Clickable heldIngredient)
		{
			if (InteractionLogic != null)
			{
				InteractionLogic.OnClick(heldTool, heldIngredient);
			}
		}
		
		void HandleClicked()
		{
			GameEvent.ItemClicked.Dispatch(Clickable);
		}

		public void SetButtonState(bool enabled)
		{
			Image.raycastTarget = enabled;
		}

		void OnDestroy()
		{
			Button.onClick.RemoveListener(HandleClicked);
		}

		void OnValidate()
		{
			if (EnumMap != null)
			{
				Image.sprite = EnumMap.GetSprite(Clickable);
			}

			if (InteractionLogic == null)
			{
				InteractionLogic = GetComponent<ClickableInteraction>();
			}
		}

		public void MoveTo(Vector3 position)
		{
			Transform.position = position;
		}

		public void SetFilledIngredient(Clickable tool, Clickable ingredient)
		{
			FilledImage.enabled = ingredient != Clickable.None;
			FilledImage.sprite = EnumMap.GetSprite(tool, ingredient);
		}
	}
}