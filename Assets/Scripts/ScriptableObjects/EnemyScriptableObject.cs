using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemies/Single Enemy")]
	public class EnemyScriptableObject : ScriptableObject
	{
		public float Speed = 3;
		public float Health = 5;
		public float DamageRadius = 4;
		internal float Damage = 1;
		internal float DamageFrequencySec = 0.5f;
	}
}