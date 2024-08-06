using System;
using Gameplay.Enemies;
using Gameplay.PlayerModule;
using UnityEngine;

namespace Gameplay
{
	public class Presenter : MonoBehaviour
	{
		public IView View { get; set; }

		[SerializeField] private Player _player;
		[SerializeField] private EnemiesManager _enemiesManager;
		[SerializeField] private Gun _gun;

		private void Awake()
		{
			AddListeners();
		}

		private void AddListeners()
		{
			_player.NormalizedHealthChanged += OnNormalizedHealthChanged;
			_player.NormalizedExperienceChanged += OnNormalizedExperienceChanged;
			_player.LevelChanged += OnLevelChanged;
			_enemiesManager.KillsCountChanged += OnKillsCountChanged;
			_gun.AmmoAmountChanged += OnAmmoAmountChanged;
		}

		private void OnAmmoAmountChanged(object sender, int a)
		{
			View.SetAmmoAmount(a);
		}

		private void OnKillsCountChanged(object sender, int k)
		{
			View.SetKillsCount(k);
		}

		private void OnLevelChanged(object sender, int l)
		{
			View.SetLevel(l);
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