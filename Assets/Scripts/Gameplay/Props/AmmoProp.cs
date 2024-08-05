using ScriptableObjects;
using UnityEngine;

namespace Gameplay.Props
{
	public class AmmoProp : MonoBehaviour
	{
		[SerializeField] private AmmoPropsScriptableObject _data;
		public float AmmoValue => _data.AmmoValue;
	}
}