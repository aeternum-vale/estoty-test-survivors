using UnityEngine;

namespace Gameplay.PlayerModule
{

	public interface IInputSource
	{
		public Vector2 MovementDelta { get; }
	}
}