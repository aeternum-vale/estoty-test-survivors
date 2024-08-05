using UnityEngine;
using ScriptableObjects;

namespace Gameplay
{
	public class Player : MonoBehaviour
	{
		public IInputSource InputSource { get; set; }

		[SerializeField] private PlayerScriptableObject _data;
		[SerializeField] private EnemiesManager _enemiesManager;
		[SerializeField] private Gun _gun;

		void Update()
		{
			transform.position += _data.Speed * Time.deltaTime * (Vector3)InputSource.MovementDelta;

			var closestEnemy = _enemiesManager.GetEnemyClosestToPlayer();
			_gun.ClosestEnemy = (closestEnemy != null) ? closestEnemy.transform : null;
		}
	}
}