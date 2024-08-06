using System;
using System.Collections;
using Gameplay.PlayerModule;
using UnityEngine;

namespace Gameplay
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private Player _player;
		[SerializeField] private GameObject _gameOverScreen;

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
			yield return new WaitForSecondsRealtime(5);
			Debug.Log($"<b><color=lightblue>{GetType().Name}:</color></b> Finished");
		}
	}
}