using Gameplay.PlayerModule;
using UnityEngine;

namespace Gameplay
{
	public class Presenter : MonoBehaviour
	{
		public IView View { get; set; }

		[SerializeField] private Player _player;

		private void Awake()
		{
			AddListeners();
		}

		private void AddListeners()
		{
			_player.NormalizedHealthChanged += OnNormalizedHealthChanged;
			_player.NormalizedExperienceChanged += OnNormalizedExperienceChanged;
		}

		private void OnNormalizedExperienceChanged(object sender, float e)
		{
			View.SetNormalizedExperience(e);
		}

		private void OnNormalizedHealthChanged(object sender, float n)
		{
			View.SetNormalizedHealth(n);
		}
	}
}