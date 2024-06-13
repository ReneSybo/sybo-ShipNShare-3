using Misc;
using UnityEngine;

namespace BakingGame.States
{
	public class StateChanger : MonoBehaviour
	{
		public void HandleGameReset()
		{
			GameEvent.GameReset.Dispatch();
		}

		public void HandleGameStart()
		{
			GameEvent.GameStart.Dispatch();
		}

		public void HandleGameWin()
		{
			GameEvent.GameWin.Dispatch();
		}

		public void HandleGameLose()
		{
			GameEvent.GameLose.Dispatch();
		}
	}
}