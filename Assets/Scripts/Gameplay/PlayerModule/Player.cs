using UnityEngine;
using ScriptableObjects;
using System;
using Gameplay.Props;
using Gameplay.Enemies;

namespace Gameplay.PlayerModule
{
	public class Player : MonoBehaviour
	{
		public IInputSource InputSource { get; set; }
		public event EventHandler Died;
		public event EventHandler<float> NormalizedHealthChanged;
		public event EventHandler<float> NormalizedExperienceChanged;

		[SerializeField] private PlayerScriptableObject _data;
		[SerializeField] private EnemiesManager _enemiesManager;
		[SerializeField] private Gun _gun;
		[SerializeField] private float _damageRadius;

		private float _health;
		private float _ammo;
		private float _experience;

		private void Start()
		{
			_health = _data.Health;
			_ammo = _data.Ammo;
			_experience = 0f;

			_enemiesManager.CausedDamageToTarget += OnDamageMade;

			NormalizedHealthChanged(this, 1f);
			NormalizedExperienceChanged(this, 0f);
		}

		private void OnDamageMade(object sender, float d)
		{
			_health -= d;
			_health = _health < 0 ? 0 : _health;

			NormalizedHealthChanged(this, _health / _data.Health);

			if (_health == 0)
				Died?.Invoke(this, EventArgs.Empty);
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

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.transform.TryGetComponent<Prop>(out var prop))
			{
				if (prop.IsUsed) return;

				prop.OnPickedUp();

				switch (prop.Type)
				{
					case PropType.Experience:
						_experience += prop.gameObject.GetComponent<ExperienceProp>().ExperienceValue;
						NormalizedExperienceChanged?.Invoke(this, _experience / _data.ExperienceAmountOfOneLevel);
						break;
					case PropType.Health:
						_health += prop.gameObject.GetComponent<HealthProp>().HealthValue;
						NormalizedHealthChanged(this, _health / _data.Health);
						break;
					case PropType.Ammo:
						_ammo += prop.gameObject.GetComponent<AmmoProp>().AmmoValue;
						break;
					default:
						throw new Exception("Invalid prop type");
				}
			}
		}
	}
}