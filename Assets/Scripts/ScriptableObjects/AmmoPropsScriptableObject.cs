using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "AmmoData", menuName = "ScriptableObjects/Props/Ammo")]
	public class AmmoPropsScriptableObject : ScriptableObject
	{
		public int AmmoValue;
	}
}