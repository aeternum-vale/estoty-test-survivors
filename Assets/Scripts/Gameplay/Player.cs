using UnityEngine;
using ScriptableObjects;
using System;

namespace Gameplay
{
	public class Player : MonoBehaviour
	{
		public IInputSource InputSource { get; set; }
		public event EventHandler Died;

		[SerializeField] private PlayerScriptableObject _data;
		[SerializeField] private EnemiesManager _enemiesManager;
		[SerializeField] private Gun _gun;
		[SerializeField] private float _damageRadius;

		private float _health;

		private void Start()
		{
			_health = _data.Health;

			_enemiesManager.CausedDamageBySomeEnemy += OnDamageMade;
		}

		private void OnDamageMade(object sender, float d)
		{
			_health -= d;
			Debug.Log($"<b><color=lightblue>{GetType().Name}:</color></b> Health: {_health}");

			if (_health <= 0)
			{
				Died?.Invoke(this, EventArgs.Empty);
			}
		}

		void Update()
		{
			transform.position += _data.Speed * Time.deltaTime * (Vector3)InputSource.MovementDelta;

			_enemiesManager.GetEnemyClosestToPlayer(out var closestEnemy, out var distance);
			_gun.ClosestEnemy = (closestEnemy != null) ? closestEnemy.transform : null;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _damageRadius);
		}
	}
}