using ScriptableObjects;
using UnityEngine;

namespace Gameplay.Props
{
	public class ExperienceProp : MonoBehaviour
	{
		[SerializeField] private ExperiencePropsScriptableObject _data;
		public float ExperienceValue => _data.ExperienceValue;
	}
}