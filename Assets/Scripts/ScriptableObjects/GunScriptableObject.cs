using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/Player/Gun")]
	public class GunScriptableObject : ScriptableObject
	{
		public float ShootingFrequencySec;
		public float BulletSpeed;
	}
}