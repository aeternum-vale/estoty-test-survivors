using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "EnemiesGeneralData", menuName = "ScriptableObjects/Enemies/General")]
	public class EnemyManagerScriptableObject : ScriptableObject
	{
		public float InitialSpawnIntervalSec;

		public float SpawnIntervalDecreaseIntervalSec;
		public float SpawnIntervalDecreaseAmountSec;
		public float MinSpawnIntervalSec;
	}
}