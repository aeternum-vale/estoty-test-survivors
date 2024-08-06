using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
	void Reinitialize();
} 

public class Pool<T> : MonoBehaviour where T : MonoBehaviour, IPoolable
{
	protected readonly List<T> _instances = new List<T>();
	private readonly Queue<T> _inactiveInstances = new Queue<T>();

	public virtual T Spawn(Func<T> instantiate, bool forceInstantiate = false)
	{
		T instance;
		if (_inactiveInstances.Count > 0 && !forceInstantiate)
		{
			instance = _inactiveInstances.Dequeue();
			instance.Reinitialize();
			instance.gameObject.SetActive(true);
		}
		else
		{
			instance = instantiate();
			_instances.Add(instance);
		}
		return instance;
	}

	public virtual void Despawn(T instance)
	{
		instance.gameObject.SetActive(false);
		_inactiveInstances.Enqueue(instance);
	}
}