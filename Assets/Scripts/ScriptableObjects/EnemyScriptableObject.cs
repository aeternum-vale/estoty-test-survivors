using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
	public class EnemyScriptableObject : ScriptableObject
	{
		public float Speed;
	}
}