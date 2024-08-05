using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player")]
	public class PlayerScriptableObject : ScriptableObject
	{
		public float Speed;
		public float ShootingFrequencySec;
		public float BulletSpeed;
	}
}