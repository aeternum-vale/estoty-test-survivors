using ScriptableObjects;
using UnityEngine;

namespace Gameplay.PlayerModule
{
	public class Gun : Pool<Bullet>
	{
		[SerializeField] private GunScriptableObject _data;
		[SerializeField] private Transform _bulletSource;
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private Transform _bulletsParent;

		public Transform ClosestEnemy { get; set; }
		private float _shootingElapsedTime;


		private void Update()
		{
			bool hasClosestEnemy = ClosestEnemy != null;
			var gunDirection = hasClosestEnemy ? ClosestEnemy.position - transform.position : Vector3.right;
			transform.right = gunDirection;

			_shootingElapsedTime += Time.deltaTime;

			if (!hasClosestEnemy) return;

			if (_shootingElapsedTime >= _data.ShootingFrequencySec)
			{
				Shoot();
				_shootingElapsedTime = 0f;
			}
		}

		private void Shoot()
		{
			var bullet = Spawn(() => Instantiate(_bulletPrefab, _bulletsParent));
			bullet.transform.position = _bulletSource.position;
			bullet.Speed = _data.BulletSpeed;
			bullet.Direction = ClosestEnemy.position - _bulletSource.position;

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