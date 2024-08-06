using System;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay.PlayerModule
{
	public class Gun : Pool<Bullet>
	{
		public event EventHandler<int> AmmoAmountChanged;

		[SerializeField] private GunScriptableObject _data;
		[SerializeField] private Transform _bulletSource;
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private Transform _bulletsParent;

		public Transform ClosestEnemy { get; set; }

		private float _shootingElapsedTime;
		private float _currentShootingInterval;

		private int _ammo;
		public int Ammo
		{
			get => _ammo;
			set
			{
				if (_ammo != value)
				{
					_ammo = value;
					AmmoAmountChanged?.Invoke(this, _ammo);
				}
			}
		}

		private void Awake()
		{
			Ammo = _data.InitialAmmo;
			_currentShootingInterval = _data.InitialShootingIntervalSec;
		}

		public void DecreaseShootingInterval()
		{
			_currentShootingInterval -= _data.ShootingIntervalDecreaseAmountSec;

			_currentShootingInterval = _currentShootingInterval < _data.MinShootingIntervalSec ?
				_data.MinShootingIntervalSec : _currentShootingInterval;
		}

		private void Update()
		{
			bool hasClosestEnemy = ClosestEnemy != null;
			var gunDirection = hasClosestEnemy ? ClosestEnemy.position - transform.position : Vector3.right;
			transform.right = gunDirection;

			_shootingElapsedTime += Time.deltaTime;

			if (!hasClosestEnemy) return;

			if (_shootingElapsedTime >= _currentShootingInterval)
			{
				Shoot();
				_shootingElapsedTime = 0f;
			}
		}

		private void Shoot()
		{
			if (Ammo <= 0) return;

			var bullet = Spawn(() => Instantiate(_bulletPrefab, _bulletsParent));
			bullet.transform.position = _bulletSource.position;
			bullet.Speed = _data.BulletSpeed;
			bullet.Direction = ClosestEnemy.position - _bulletSource.position;
			Ammo--;

			bullet.Completed += OnBulletCompleted;
		}

		private void OnBulletCompleted(object sender, Bullet b)
		{
			Bullet bullet = b;
			b.Completed -= OnBulletCompleted;
			Despawn(bullet);
		}
	}
}