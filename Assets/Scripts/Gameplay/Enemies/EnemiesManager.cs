using System;
using System.Collections.Generic;
using Gameplay;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Enemies
{



	public class EnemiesManager : MonoBehaviour
	{
		public event EventHandler<float> CausedDamageToTarget;

		[SerializeField] private EnemyManagerScriptableObject _data;
		[SerializeField] private Transform _target;
		[SerializeField] private Enemy[] _enemyPrefabs;
		[SerializeField] private Transform _enemiesParent;
		[SerializeField] private float _spawnRadius;

		private readonly List<Enemy> _enemyInstances = new List<Enemy>();
		private readonly Queue<Enemy> _inactiveInstances = new Queue<Enemy>();
		private float _spawnElapsedTime;

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

			enemyInstance.transform.position = _target.position + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0).normalized * _spawnRadius;
			enemyInstance.Target = _target;
			AddListenersToEnemy(enemyInstance);

			return enemyInstance;
		}

		private void AddListenersToEnemy(Enemy enemy)
		{
			enemy.Died += OnEnemyDied;
			enemy.CausedDamageToTarget += OnCausedDamageToTarget;
		}

		private void RemoveListenersFromEnemy(Enemy enemy)
		{
			enemy.Died -= OnEnemyDied;
			enemy.CausedDamageToTarget -= OnCausedDamageToTarget;
		}

		private void OnCausedDamageToTarget(object sender, float damage)
		{
			CausedDamageToTarget?.Invoke(this, damage);
		}

		private void OnEnemyDied(object sender, System.EventArgs args)
		{
			DeactivateEnemy((Enemy)sender);
		}

		private void DeactivateEnemy(Enemy e)
		{
			RemoveListenersFromEnemy(e);
			e.gameObject.SetActive(false);
			_inactiveInstances.Enqueue(e);
		}

		public void GetEnemyClosestToPlayer(out Enemy closest, out float distance)
		{
			float minSqrMagnitude = float.MaxValue;
			closest = null;

			foreach (var e in _enemyInstances)
			{
				if (!e.gameObject.activeSelf || e.IsDead) continue;

				var sqrMagnitude = Vector3.SqrMagnitude(e.transform.position - _target.position);
				if (sqrMagnitude < minSqrMagnitude)
				{
					minSqrMagnitude = sqrMagnitude;
					closest = e;
				}
			}

			distance = Mathf.Sqrt(minSqrMagnitude);
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
			Gizmos.DrawWireSphere(_target.position, _spawnRadius);
		}
	}
}