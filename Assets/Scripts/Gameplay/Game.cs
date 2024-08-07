using System;
using System.Collections;
using Gameplay.PlayerModule;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Gameplay
{
	public class Game : MonoBehaviour
	{
		public GameObject GameOverScreen { get => _gameOverScreen; set => _gameOverScreen = value; }

		private Player _player;
		private GameObject _gameOverScreen;


		[Inject]
		private void Construct(Player player) 
		{
			_player = player;
		}

		private void Awake()
		{
			_player.Died += OnPlayerDied;
		}

		private void OnPlayerDied(object sender, EventArgs e)
		{
			_gameOverScreen.SetActive(true);
			Time.timeScale = 0;
			StartCoroutine(FinishGame());
		}

		private IEnumerator FinishGame()
		{
			yield return new WaitForSecondsRealtime(3);
			Time.timeScale = 1;
			SceneManager.LoadScene("Boot");
		}
	}
}