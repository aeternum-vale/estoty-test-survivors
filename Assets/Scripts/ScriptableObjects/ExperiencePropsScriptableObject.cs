using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "ExperienceData", menuName = "ScriptableObjects/Props/Experience")]
	public class ExperiencePropsScriptableObject : ScriptableObject
	{
		public float ExperienceValue;
	}
}