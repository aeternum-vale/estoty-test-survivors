using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "EnemiesGeneralData", menuName = "ScriptableObjects/Enemies/General")]
	public class EnemyManagerScriptableObject : ScriptableObject
	{
		public float SpawnFrequencySec;
	}
}