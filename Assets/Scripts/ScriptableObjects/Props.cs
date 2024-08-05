using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "AmmoData", menuName = "ScriptableObjects/Props/Ammo")]
	public class AmmoPropsScriptableObject : ScriptableObject
	{
		public float AmmoValue;
	}

	[CreateAssetMenu(fileName = "ExperienceData", menuName = "ScriptableObjects/Props/Experience")]
	public class ExperiencePropsScriptableObject : ScriptableObject
	{
		public float ExperienceValue;
	}

	[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/Props/Health")]
	public class HealthPropsScriptableObject : ScriptableObject
	{
		public float HealthValue;
	}
}