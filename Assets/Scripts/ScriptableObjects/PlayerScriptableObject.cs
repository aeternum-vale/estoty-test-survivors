using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/Player")]
	public class PlayerScriptableObject : ScriptableObject
	{
		public float Speed;
		public float Health;
		public float Ammo;
		public float ExperienceAmountOfOneLevel;
	}
}