using UnityEngine;
using UI;

namespace Gameplay
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private Joystick _joystick;
		[SerializeField] private Player _player;

		[SerializeField] private EnemiesManager _enemiesManager;

		private void Awake()
		{
			_player.InputSource = _joystick;
		}

		private void Start()
		{
			Application.targetFrameRate = 60;
		}

		private void Update()
		{
			var closestEnemy = _enemiesManager.GetEnemyClosestToPlayer();
			_player.ClosestEnemy = (closestEnemy != null) ? closestEnemy.transform : null;
		}
	}
}