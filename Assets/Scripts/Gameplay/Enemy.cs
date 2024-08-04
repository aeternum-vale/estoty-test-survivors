using ScriptableObjects;
using UnityEngine;

namespace Gameplay
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private Transform _player;
		[SerializeField] private EnemyScriptableObject _enemyData;

		private float CorrectedSpeed => _enemyData.Speed * Time.deltaTime;

		void Update()
		{
			var direction = (_player.position - transform.position).normalized;
			transform.position += direction * CorrectedSpeed;

			var scaleX = Mathf.Abs(transform.localScale.x);
			transform.localScale = transform.localScale.WithX(direction.x >= 0 ? scaleX : -scaleX);
		}
	}
}