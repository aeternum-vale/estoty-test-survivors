using UnityEngine;

namespace Gameplay
{
	public class CameraFollower : MonoBehaviour
	{
		public Transform Target { get => _target; set => _target = value; }
		private Transform _target;


		private void LateUpdate()
		{
			var newPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
			transform.position = newPosition;
		}
	}
}