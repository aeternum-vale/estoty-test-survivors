using System;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay
{
	public class Enemy : MonoBehaviour
	{
		public Transform Target { get; set; }
		public bool IsDead { get; set; }
		public event EventHandler Died;
		public event EventHandler<float> CausedDamageToTarget;


		[SerializeField] private EnemyScriptableObject _data;
		private float _health;
		private bool _isWithinDamageRadius;
		private float _elapsedTimeSinceLastDamage;


		private void Awake()
		{
			Reinitialize();
		}

		public void Reinitialize()
		{
			IsDead = false;
			_health = _data.Health;
		}

		private void Update()
		{
			if (IsDead) return;

			UpdateDamaging();

			var direction = (Target.position - transform.position).normalized;
			if (!_isWithinDamageRadius)
			{
				transform.position += _data.Speed * Time.deltaTime * direction;
			}

			var scaleX = Mathf.Abs(transform.localScale.x);
			transform.localScale = transform.localScale.WithX(direction.x >= 0 ? scaleX : -scaleX);
		}

		private void UpdateDamaging()
		{
			_elapsedTimeSinceLastDamage += Time.deltaTime;

			_isWithinDamageRadius =
				Vector2.Distance(Target.position, transform.position) <= _data.DamageRadius;

			if (_isWithinDamageRadius)
			{
				if (_elapsedTimeSinceLastDamage >= _data.DamageFrequencySec)
				{
					_elapsedTimeSinceLastDamage = 0f;
					CausedDamageToTarget?.Invoke(this, _data.Damage);
				}
			}
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

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _data.DamageRadius);
		}

	}
}