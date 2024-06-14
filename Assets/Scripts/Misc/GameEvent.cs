using System;
using BakingGame;

namespace Misc
{
	public class GameEvent
	{
		public static GameEvent<Clickable> ItemClicked = new GameEvent<Clickable>();
		public static GameEvent<Clickable> PickupTool = new GameEvent<Clickable>();
		public static GameEvent<Clickable> PickupIngredient = new GameEvent<Clickable>();
		public static GameEvent<Clickable> ActuallyPickupIngredient = new GameEvent<Clickable>();
		public static GameEvent<Clickable> AddedWrongIngredient = new GameEvent<Clickable>();
		public static GameEvent<Clickable> AddedTooMuchIngredient = new GameEvent<Clickable>();
		public static GameEvent BowlClicked = new GameEvent();
		public static GameEvent<Clickable> TrashClicked = new GameEvent<Clickable>();
		public static GameEvent PutDownTool = new GameEvent();
		public static GameEvent<Clickable> AddedIngredient = new GameEvent<Clickable>();
		public static GameEvent<GlowType> ShowGlow = new GameEvent<GlowType>();
		public static GameEvent CakeWrongBake = new GameEvent();
		public static GameEvent CakeCorrectBake = new GameEvent();
		
		public static GameEvent GameReset = new GameEvent();
		public static GameEvent GameStart = new GameEvent();
		public static GameEvent GameWin = new GameEvent();
		public static GameEvent GameLose = new GameEvent();

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