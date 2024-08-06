using UnityEngine;

namespace ScriptableObjects
{

	[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/Props/Health")]
	public class HealthPropsScriptableObject : ScriptableObject
	{
		public float HealthValue;
	}
}