using System;
using BakingGame;

namespace Misc
{
	public class GameEvent
	{
		public static GameEvent GameStarted = new GameEvent();
		public static GameEvent<Clickable> ItemClicked = new GameEvent<Clickable>();
		public static GameEvent<Clickable> PickupTool = new GameEvent<Clickable>();
		public static GameEvent<Clickable> PickupIngredient = new GameEvent<Clickable>();
		public static GameEvent<Clickable> AddedWrongIngredient = new GameEvent<Clickable>();
		public static GameEvent<Clickable> AddedTooMuchIngredient = new GameEvent<Clickable>();
		public static GameEvent BowlClicked = new GameEvent();
		public static GameEvent TrashClicked = new GameEvent();
		public static GameEvent PutDownTool = new GameEvent();
		public static GameEvent AddedTrash = new GameEvent();
		public static GameEvent PickupTrash = new GameEvent();

		public event Action _callbacks;

		public GameEvent()
		{
		}

		public void Dispatch()
		{
			_callbacks?.Invoke();
		}

		public void AddListener(Action callback)
		{
			_callbacks += callback;
		}

		public void RemoveListener(Action callback)
		{
			_callbacks -= callback;
		}
	}
	
	public class GameEvent<TData>
	{
		public event Action<TData> _callbacks;

		public GameEvent()
		{
		}

		public void Dispatch(TData data)
		{
			_callbacks?.Invoke(data);
		}

		public void AddListener(Action<TData> callback)
		{
			_callbacks += callback;
		}

		public void RemoveListener(Action<TData> callback)
		{
			_callbacks -= callback;
		}
	}
}