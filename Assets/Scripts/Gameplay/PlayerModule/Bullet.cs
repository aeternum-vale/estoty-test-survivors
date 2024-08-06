using System;
using Gameplay.Enemies;
using UnityEngine;

namespace Gameplay.PlayerModule
{
	public class Bullet : MonoBehaviour, IPoolable
	{
		public Vector2 Direction { get; set; }
		public float Speed { get; set; }

		public event EventHandler<Bullet> Completed;

		private void Update()
		{
			transform.position += Speed * Time.deltaTime * (Vector3)Direction.normalized;
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.transform.TryGetComponent<Enemy>(out var enemy))
			{
				enemy.DecreaseHealth();
				Completed?.Invoke(this, this);
			}
		}

		private void OnBecameInvisible()
		{
			Completed?.Invoke(this, this);
		}

		public void Reinitialize()
		{
			
		}
	}
}