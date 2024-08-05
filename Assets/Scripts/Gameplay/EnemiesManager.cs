using System.Collections.Generic;
using Gameplay;
using ScriptableObjects;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{

	[SerializeField] private EnemyManagerScriptableObject _data;
	[SerializeField] private Transform _player;
	[SerializeField] private Enemy[] _enemyPrefabs;
	[SerializeField] private Transform _enemiesParent;
	[SerializeField] private float _spawnRadius;

	private List<Enemy> _enemyInstances = new List<Enemy>();
	private float _spawnElapsedTime;

	private Queue<Enemy> _inactiveInstances = new Queue<Enemy>();

	private void Start()
	{
		foreach (var p in _enemyPrefabs)
		{
			var enemyInstance = Instantiate(p, _enemiesParent);
			_enemyInstances.Add(enemyInstance);
			DeactivateEnemy(enemyInstance);
		}
	}

	private Enemy SpawnEnemy()
	{
		Enemy enemyInstance;

		if (_inactiveInstances.Count > 0)
		{
			enemyInstance = _inactiveInstances.Dequeue();
			enemyInstance.Reinitialize();
			enemyInstance.gameObject.SetActive(true);
		}
		else
		{
			var prefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
			enemyInstance = Instantiate(prefab, _enemiesParent);
			_enemyInstances.Add(enemyInstance);
		}

		enemyInstance.transform.position = _player.position + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0).normalized * _spawnRadius;
		enemyInstance.Target = _player;
		enemyInstance.Died += OnEnemyDied;

		return enemyInstance;
	}

	private void OnEnemyDied(object sender, System.EventArgs args)
	{
		DeactivateEnemy((Enemy)sender);
	}

	private void DeactivateEnemy(Enemy e)
	{
		e.Died -= OnEnemyDied;
		e.gameObject.SetActive(false);
		_inactiveInstances.Enqueue(e);
	}

	public Enemy GetEnemyClosestToPlayer()
	{
		float minSqrMagnitude = float.MaxValue;
		Enemy closest = null;

		foreach (var e in _enemyInstances)
		{
			if (!e.gameObject.activeSelf || e.IsDead) continue;

			var sqrMagnitude = Vector3.SqrMagnitude(e.transform.position - _player.position);
			if (sqrMagnitude < minSqrMagnitude)
			{
				minSqrMagnitude = sqrMagnitude;
				closest = e;
			}
		}

		return closest;
	}

	private void Update()
	{
		_spawnElapsedTime += Time.deltaTime;
		if (_spawnElapsedTime >= _data.SpawnFrequencySec)
		{
			_spawnElapsedTime = 0f;
			SpawnEnemy();
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _spawnRadius);
	}
}
