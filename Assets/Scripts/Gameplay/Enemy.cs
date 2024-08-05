using System;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private EnemyScriptableObject _enemyData;

		private float _health;

		public Transform Target { get; set; }

		public bool IsDead;

		public event EventHandler Died;

		private void Awake()
		{
			Reinitialize();
		}

		public void Reinitialize()
		{
			IsDead = false;
			_health = _enemyData.Health;
		}

		private void Update()
		{
			if (IsDead) return;

			var direction = (Target.position - transform.position).normalized;
			transform.position += direction * _enemyData.Speed * Time.deltaTime;

			var scaleX = Mathf.Abs(transform.localScale.x);
			transform.localScale = transform.localScale.WithX(direction.x >= 0 ? scaleX : -scaleX);
		}

		public void DecreaseHealth()
		{
			if (IsDead) return;

			_health--;
			if (_health <= 0)
			{
				IsDead = true;
				Died?.Invoke(this, EventArgs.Empty);
			}
		}

	}
}