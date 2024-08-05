using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemies/Single Enemy")]
	public class EnemyScriptableObject : ScriptableObject
	{
		public float Speed = 3;
		public float Health = 5;
	}
}