using ScriptableObjects;
using UnityEngine;

namespace Gameplay.Props
{
	public class AmmoProp : MonoBehaviour
	{
		[SerializeField] private AmmoPropsScriptableObject _data;
		public int AmmoValue => _data.AmmoValue;
	}
}