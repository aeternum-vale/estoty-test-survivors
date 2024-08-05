using Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IView
{
	public Presenter Presenter { get; set; }

	[SerializeField] private Slider _healthSlider;
	[SerializeField] private Slider _experienceSlider;

	public void SetNormalizedHealth(float healthPercentage)
	{
		_healthSlider.value = healthPercentage;
	}

	public void SetNormalizedExperience(float normalizedExperience)
	{
		_experienceSlider.value = normalizedExperience;
	}
}