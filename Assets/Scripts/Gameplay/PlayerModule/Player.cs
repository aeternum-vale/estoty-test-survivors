using UnityEngine;
using ScriptableObjects;
using System;
using Gameplay.Props;
using Gameplay.Enemies;
using Zenject;

namespace Gameplay.PlayerModule
{
	public class Player : MonoBehaviour
	{
		public event EventHandler Died;
		public event EventHandler<float> NormalizedHealthChanged;
		public event EventHandler<float> NormalizedExperienceChanged;
		public event EventHandler<int> LevelChanged;

		private IInputSource _inputSource;
		private EnemiesManager _enemiesManager;
		[SerializeField] private PlayerScriptableObject _data;
		[SerializeField] private Gun _gun;

		private float _health;
		private float Health { get => _health; set => SetHealth(value); }
		private float _experience;
		public float Experience { get => _experience; set => SetExperience(value); }
		private int _level;
		private int Level { get => _level; set => SetLevel(value); }
		public Gun Gun { get => _gun; set => _gun = value; }

		public bool IsDead { get; set; }

		[Inject]
		private void Construct(IInputSource inputSource, EnemiesManager enemiesManager, PropsManager propsManager)
		{
			_inputSource = inputSource;
			_enemiesManager = enemiesManager;

			propsManager.AttractionTarget = transform;
		}

		private void Awake()
		{
			_enemiesManager.Target = transform;
		}

		private void Start()
		{
			Health = _data.TotalHealth;
			Experience = 0f;
			Level = 0;

			NormalizedHealthChanged?.Invoke(this, 1f);
			NormalizedExperienceChanged?.Invoke(this, 0f);
			LevelChanged?.Invoke(this, 0);

			_enemiesManager.CausedDamageToTarget += OnDamageMade;
		}

		private void OnDamageMade(object sender, float d)
		{
			Health -= d;
			if (Health <= 0 && !IsDead)
			{
				IsDead = true;
				Died?.Invoke(this, EventArgs.Empty);
			}
		}

		void Update()
		{
			transform.position += _data.Speed * Time.deltaTime * (Vector3)_inputSource.MovementDelta;

			_enemiesManager.GetEnemyClosestToPlayer(out var closestEnemy, out var distance);

			bool hasTarget = (closestEnemy != null) && (distance <= _data.ShootingRadius);
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
						_gun.Ammo += prop.gameObject.GetComponent<AmmoProp>().AmmoValue;
						break;
					default:
						throw new Exception("Invalid prop type");
				}
			}
		}

		private void SetExperience(float value)
		{
			var old = _experience;
			_experience = value;

			if (_experience >= _data.ExperienceAmountOfOneLevel)
			{
				Level++;
				_gun.DecreaseShootingInterval();
				_experience %= _data.ExperienceAmountOfOneLevel;
			}

			if (old != _experience)
				NormalizedExperienceChanged?.Invoke(this, _experience / _data.ExperienceAmountOfOneLevel);
		}

		private void SetHealth(float value)
		{
			var old = _health;
			_health = Math.Clamp(value, 0, _data.TotalHealth);
			if (old != _health)
				NormalizedHealthChanged?.Invoke(this, _health / _data.TotalHealth);
		}

		private void SetLevel(int value)
		{
			if (_level != value)
			{
				_level = value;
				LevelChanged?.Invoke(this, _level);
			}
		}

		private void OnDestroy()
		{
			_enemiesManager.CausedDamageToTarget -= OnDamageMade;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _data.ShootingRadius);
		}
	}
}