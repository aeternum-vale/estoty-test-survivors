using UnityEngine;
using ScriptableObjects;
using System;

namespace Gameplay
{
	public class Player : MonoBehaviour
	{
		private IInputSource _inputSource;
		public IInputSource InputSource { get => _inputSource; set => _inputSource = value; }
		public Transform ClosestEnemy { get => _closestEnemy; set => _closestEnemy = value; }

		[SerializeField] private PlayerScriptableObject _playerData;

		[SerializeField] private Transform _gun;
		[SerializeField] private Transform _bulletSource;
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private Transform _closestEnemy;


		private float _shootingElapsedTime;

		void Update()
		{
			transform.position += (Vector3)_inputSource.MovementDelta * _playerData.Speed * Time.deltaTime;
			UpdateShooting();
		}

		private void UpdateShooting()
		{
			if (_closestEnemy == null) return;

			var gunDirection = _closestEnemy.position - _gun.position;
			_gun.right = gunDirection;

			_shootingElapsedTime += Time.deltaTime;
			if (_shootingElapsedTime >= _playerData.ShootingFrequencySec)
			{
				_shootingElapsedTime = 0f;
				Shoot();
			}

			// var scaleX = Mathf.Abs(transform.localScale.x);
			// transform.localScale = transform.localScale.WithX(gunDirection.x >= 0 ? scaleX : -scaleX);
			// _gun.right *= Mathf.Sign(transform.localScale.x);
		}

		private void Shoot()
		{
			var bullet = Instantiate(_bulletPrefab);
			bullet.transform.position = _bulletSource.position;
			bullet.Speed = _playerData.BulletSpeed;
			bullet.Direction = _closestEnemy.position - _bulletSource.position;
		}
	}
}