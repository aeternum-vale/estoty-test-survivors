using ScriptableObjects;
using UnityEngine;

namespace Gameplay.PlayerModule
{
	public class Gun : MonoBehaviour
	{
		[SerializeField] private GunScriptableObject _data;
		[SerializeField] private Transform _bulletSource;
		[SerializeField] private Bullet _bulletPrefab;

		public Transform ClosestEnemy { get; set; }
		private float _shootingElapsedTime;


		private void Update()
		{
			if (ClosestEnemy == null) return;

			var gunDirection = ClosestEnemy.position - transform.position;
			transform.right = gunDirection;

			_shootingElapsedTime += Time.deltaTime;
			if (_shootingElapsedTime >= _data.ShootingFrequencySec)
			{
				_shootingElapsedTime = 0f;
				Shoot();
			}
		}

		private void Shoot()
		{
			var bullet = Instantiate(_bulletPrefab);
			bullet.transform.position = _bulletSource.position;
			bullet.Speed = _data.BulletSpeed;
			bullet.Direction = ClosestEnemy.position - _bulletSource.position;
		}
	}
}