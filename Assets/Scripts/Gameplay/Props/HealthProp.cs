using ScriptableObjects;
using UnityEngine;

namespace Gameplay.Props
{
	public class HealthProp : MonoBehaviour
	{
		[SerializeField] private HealthPropsScriptableObject _data;
		public float HealthValue => _data.HealthValue;
	}
}