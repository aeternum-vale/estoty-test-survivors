using UnityEngine;
using UI;
using Gameplay;
using Gameplay.PlayerModule;

namespace Meta
{
	public class Main : MonoBehaviour
	{
		[SerializeField] private Joystick _joystick;
		[SerializeField] private Player _player;

		[SerializeField] private Presenter _presenter;
		[SerializeField] private UIManager _uiManager;

		
		// private void Awake()
		// {
		// 	_player._inputSource = _joystick;
		// 	_presenter.View = _uiManager;
		// }

		private void Start()
		{
			Application.targetFrameRate = 60;
		}
	}
}