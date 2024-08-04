using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private Transform _player;
	[SerializeField] private float _speed;

	private float CorrectedSpeed => _speed * Time.deltaTime; 

	void Update()
	{
		var direction = (_player.position - transform.position).normalized;
		transform.position += direction * CorrectedSpeed;
		transform.localScale = transform.localScale.WithX(direction.x >= 0 ? 1f : -1f);
	}
}
