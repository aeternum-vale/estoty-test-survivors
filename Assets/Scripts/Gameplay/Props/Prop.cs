using System;
using ScriptableObjects;
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
		[SerializeField] private GeneralPropsScriptableObject _data;

		public PropType Type { get => _type; }
		public bool IsUsed { get; private set; }
		public bool IsAttracted { get; set; }
		public Transform AttractionTarget { get; set; }

		public event EventHandler PickedUp;

		public void OnPickedUp()
		{
			IsUsed = true;

			PickedUp?.Invoke(this, EventArgs.Empty);
		}

		public void Reinitialize()
		{
			IsUsed = false;
			IsAttracted = false;
		}

		private void Update()
		{
			if (!IsAttracted)
			{
				if (Vector2.Distance(AttractionTarget.position, transform.position) <= _data.AttractionRadius)
					IsAttracted = true;
			}
			else
			{
				transform.position +=
					_data.AttractionSpeed * Time.deltaTime * (AttractionTarget.position - transform.position).normalized;
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, _data.AttractionRadius);
		}
	}
}