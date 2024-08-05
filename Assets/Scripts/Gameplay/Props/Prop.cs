using System;
using UnityEngine;

namespace Gameplay.Props
{
	public enum PropType
	{
		None = 0,
		Experience,
		Health,
		Ammo
	}

	public class Prop : MonoBehaviour
	{
		[SerializeField] private PropType _type;
		public PropType Type { get => _type; }
		public bool IsUsed { get; private set; }

		public void OnPickedUp()
		{
			IsUsed = true;
		}
	}
}