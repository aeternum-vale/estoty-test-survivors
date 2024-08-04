using UnityEngine;
using ScriptableObjects;

namespace Gameplay
{
	public class Player : MonoBehaviour
	{
		private IInputSource _inputSource;
		public IInputSource InputSource { get => _inputSource; set => _inputSource = value; }

		[SerializeField] private PlayerScriptableObject _playerData;

		private float CorrectedSpeed => _playerData.Speed * Time.deltaTime;

		void Update()
		{
			transform.position += (Vector3)_inputSource.MovementDelta * CorrectedSpeed;
		}
	}
}