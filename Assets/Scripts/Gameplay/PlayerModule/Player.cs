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
		[SerializeField] private float _shootingRadius;

		private float _health;
		private float Health
		{
			get => _health;
			set
			{
				var old = _health;
				_health = Math.Clamp(value, 0, _data.TotalHealth);
				if (old != _health)
					NormalizedHealthChanged?.Invoke(this, _health / _data.TotalHealth);
			}
		}

		private float _experience;
		public float Experience
		{
			get => _experience;
			set
			{
				var old = _experience;
				_experience = Math.Clamp(value, 0, _data.ExperienceAmountOfOneLevel);
				if (old != _experience)
					NormalizedExperienceChanged?.Invoke(this, _experience / _data.ExperienceAmountOfOneLevel);
			}
		}

		private float _ammo;

		private void Start()
		{
			Health = _data.TotalHealth;
			Experience = 0f;
			_ammo = _data.TotalAmmo;

			_enemiesManager.CausedDamageToTarget += OnDamageMade;
		}

		private void OnDamageMade(object sender, float d)
		{
			Health -= d;
			if (Health == 0)
				Died?.Invoke(this, EventArgs.Empty);
		}

		void Update()
		{
			transform.position += _data.Speed * Time.deltaTime * (Vector3)InputSource.MovementDelta;

			_enemiesManager.GetEnemyClosestToPlayer(out var closestEnemy, out var distance);

			bool hasTarget = (closestEnemy != null) && (distance <= _shootingRadius);
			_gun.ClosestEnemy = hasTarget ? closestEnemy.transform : null;
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
						Experience += prop.gameObject.GetComponent<ExperienceProp>().ExperienceValue;
						break;
					case PropType.Health:
						Health += prop.gameObject.GetComponent<HealthProp>().HealthValue;
						break;
					case PropType.Ammo:
						_ammo += prop.gameObject.GetComponent<AmmoProp>().AmmoValue;
						break;
					default:
						throw new Exception("Invalid prop type");
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _shootingRadius);
		}
	}
}