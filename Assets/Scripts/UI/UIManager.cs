using Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IView
{
	[SerializeField] private Slider _healthSlider;
	[SerializeField] private Slider _experienceSlider;
	[SerializeField] private Text _levelText;
	[SerializeField] private Text _killsCountText;
	[SerializeField] private Text _ammoAmountText;

	public void SetNormalizedHealth(float healthPercentage)
	{
		_healthSlider.value = healthPercentage;
	}

	public void SetNormalizedExperience(float normalizedExperience)
	{
		_experienceSlider.value = normalizedExperience;
	}

	public void SetLevel(int level)
	{
		_levelText.text = level.ToString();
	}

	public void SetKillsCount(int killsCount)
	{
		_killsCountText.text = killsCount.ToString();
	}

	public void SetAmmoAmount(int ammo)
	{
		_ammoAmountText.text = ammo.ToString();
	}
}