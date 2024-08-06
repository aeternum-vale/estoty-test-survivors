using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "GeneralPropsData", menuName = "ScriptableObjects/Props/General")]
	public class GeneralPropsScriptableObject : ScriptableObject
	{
		public float AttractionSpeed;
		public float AttractionRadius;
	}
}