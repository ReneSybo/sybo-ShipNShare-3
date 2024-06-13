using System;
using Misc;
using UnityEngine;

namespace BakingGame
{
	public class GameState : MonoBehaviour
	{
		public GameObject MainMenu;
		public GameObject PlayMode;
		public GameObject GameOver;
		public GameObject GameWon;

		public static int Difficulty;
		
		void Awake()
		{
			GameEvent.GameReset.AddListener(HandleGameReset);
			GameEvent.GameStart.AddListener(HandleGameStart);
			GameEvent.GameWin.AddListener(HandleGameWin);
			GameEvent.GameLose.AddListener(HandleGameLose);
		}
		
		void OnlyShow(GameObject toShow)
		{
			MainMenu.SetActive(false);
			PlayMode.SetActive(false);
			GameOver.SetActive(false);
			GameWon.SetActive(false);
			
			toShow.SetActive(true);
		}

		void HandleGameReset()
		{
			Difficulty = 0;
			OnlyShow(MainMenu);
		}

		void HandleGameStart()
		{
			OnlyShow(PlayMode);
		}

		void HandleGameWin()
		{
			OnlyShow(GameWon);
		}

		void HandleGameLose()
		{
			OnlyShow(GameOver);
		}
	}
}