using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/Player/Gun")]
	public class GunScriptableObject : ScriptableObject
	{
		public int InitialAmmo;
		public float InitialShootingIntervalSec;
		public float ShootingIntervalDecreaseAmountSec;
		public float BulletSpeed;
		public float MinShootingIntervalSec;
	}
}