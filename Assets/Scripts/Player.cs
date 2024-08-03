using UnityEngine;

public class Player : MonoBehaviour
{
	private IInputSource _inputSource; 
	internal IInputSource InputSource { get => _inputSource; set => _inputSource = value; }
	
	[SerializeField] private float _speed;


	private float CorrectedSpeed => _speed * Time.deltaTime;


	void Update()
    {
        transform.position += (Vector3)_inputSource.MovementDelta * CorrectedSpeed;
	}
}
