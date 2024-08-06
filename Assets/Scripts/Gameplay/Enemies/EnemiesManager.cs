using System;
using Gameplay.Props;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Enemies
{
	public class EnemiesManager : Pool<Enemy>
	{
		public event EventHandler<float> CausedDamageToTarget;

		[SerializeField] private EnemyManagerScriptableObject _data;
		[SerializeField] private PropsManager _propsManager;
		[SerializeField] private Transform _target;
		[SerializeField] private Enemy[] _enemyPrefabs;
		[SerializeField] private Transform _enemiesParent;
		[SerializeField] private float _spawnRadius;
		[SerializeField] private float _maxDropDist = 1f;


		private float _spawnElapsedTime;

		private void Start()
		{
			foreach (var p in _enemyPrefabs)
			{
				// var enemyInstance = Instantiate(p, _enemiesParent);
				// _enemyInstances.Add(enemyInstance);
				// DeactivateEnemy(enemyInstance, false);
			}
		}

		private Enemy SpawnEnemy()
		{
			Enemy enemyInstance = Spawn(CreateRandomEnemy);

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

		private void OnEnemyDied(object sender, EventArgs args)
		{
			DeactivateEnemy((Enemy)sender);
		}

		private void DeactivateEnemy(Enemy e, bool dropProps = true)
		{
			RemoveListenersFromEnemy(e);
			Despawn(e);

			if (!dropProps) return;

			var ep = _propsManager.CreateProp(PropType.Experience);
			var rp = _propsManager.CreateRandomProp();

			ep.transform.position = e.transform.position;
			rp.transform.position = e.transform.position;
			
			ep.transform.position += new Vector3(Random.Range(0, _maxDropDist), Random.Range(0, _maxDropDist), 0f);
			rp.transform.position += new Vector3(Random.Range(0, _maxDropDist), Random.Range(0, _maxDropDist), 0f);
		}

		public void GetEnemyClosestToPlayer(out Enemy closest, out float distance)
		{
			float minSqrMagnitude = float.MaxValue;
			closest = null;

			foreach (var e in _instances)
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

		private Enemy CreateRandomEnemy()
		{
			var prefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
			return Instantiate(prefab, _enemiesParent);
		}
	}
}