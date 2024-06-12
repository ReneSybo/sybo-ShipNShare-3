using System.Collections.Generic;

namespace BakingGame
{
	public class ClickableMap
	{
		public static readonly ClickableMap Instance = new ClickableMap();

		Dictionary<Clickable, ClickableItem> _clickables;

		ClickableMap()
		{
			_clickables = new Dictionary<Clickable, ClickableItem>();
		}

		public void Register(ClickableItem item)
		{
			_clickables[item.Clickable] = item;
		}

		public void Unregister(ClickableItem item)
		{
			_clickables.Remove(item.Clickable);
		}

		public ClickableItem this[Clickable item]
		{
			get
			{
				return _clickables[item];
			}
		}
	}
}