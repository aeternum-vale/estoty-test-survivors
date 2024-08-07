using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/Player")]
	public class PlayerScriptableObject : ScriptableObject
	{
		public float Speed;
		public float TotalHealth;
		public float ExperienceAmountOfOneLevel;
		public float ShootingRadius;
	}
}