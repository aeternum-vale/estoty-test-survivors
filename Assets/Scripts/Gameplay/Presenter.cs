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
		}

		private void OnNormalizedHealthChanged(object sender, float normalizedHealth)
		{
			View.SetNormalizedHealth(normalizedHealth);
		}
	}
}