using Gameplay.Enemies;
using UnityEngine;

namespace Gameplay.PlayerModule
{
	public class Bullet : MonoBehaviour
	{
		public Vector2 Direction { get; set; }
		public float Speed { get; set; }

		private void Update()
		{
			transform.position += Speed * Time.deltaTime * (Vector3)Direction.normalized;
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.transform.TryGetComponent<Enemy>(out var enemy))
			{
				enemy.DecreaseHealth();
				Destroy(gameObject);
			}
		}

		private void OnBecameInvisible()
		{
			Destroy(gameObject);
		}
	}
}