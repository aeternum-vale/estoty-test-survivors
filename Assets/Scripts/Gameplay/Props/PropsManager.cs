using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay.Props
{
	public class PropsManager : MonoBehaviour
	{
		[SerializeField] private Prop[] _propPrefabs;
		[SerializeField] private Transform _propsParent;
		[SerializeField] private Transform _attractionTarget;
		[SerializeField] private GeneralPropsScriptableObject _data;
		private Dictionary<PropType, Prop> _propPrefabsDictionary;

		private void Awake()
		{
			_propPrefabsDictionary = _propPrefabs.ToDictionary(p => p.Type);
		}

		public Prop CreateProp(PropType propType)
		{
			var prop = Instantiate(_propPrefabsDictionary[propType], _propsParent);
			prop.AttractionTarget = _attractionTarget;
			prop.PickedUp += OnPropPickedUp;
			return prop;
		}

		public Prop CreateRandomProp()
		{
			Array values = Enum.GetValues(typeof(PropType));
			PropType randomPropType =
				(PropType)values.GetValue(UnityEngine.Random.Range(1, values.Length));
			return CreateProp(randomPropType);
		}

		private void OnPropPickedUp(object sender, EventArgs e)
		{
			var prop = (Prop)sender;
			prop.PickedUp -= OnPropPickedUp;
			Destroy(prop.gameObject);
		}

	}
}