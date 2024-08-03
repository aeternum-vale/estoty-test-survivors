using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] private Joystick _joystick;
	[SerializeField] private Player _player;

	private void Awake()
	{
		_player.InputSource = _joystick;
	}

	private void Start()
	{
		Application.targetFrameRate = 60;
	}
}