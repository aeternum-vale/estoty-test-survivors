using Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IView
{
	public Presenter Presenter { get; set; }

	[SerializeField] private Slider _healthSlider;

	public void SetNormalizedHealth(float healthPercentage)
	{
		_healthSlider.value = healthPercentage;
	}
}