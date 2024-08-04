using UnityEngine;

namespace Gameplay
{

	public interface IInputSource
	{
		public Vector2 MovementDelta { get; }
	}
}