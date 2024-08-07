using System;
using Gameplay.Enemies;
using Gameplay.PlayerModule;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public class Presenter : MonoBehaviour
	{
		private IView _view;
		private Player _player;
		private EnemiesManager _enemiesManager;
		private Gun _gun;

		[Inject]
		private void Construct(IView view, Player player, EnemiesManager enemiesManager, Gun gun)
		{
			_view = view;
			_player = player;
			_enemiesManager = enemiesManager;
			_gun = gun;
		}

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
			_view.SetAmmoAmount(a);
		}

		private void OnKillsCountChanged(object sender, int k)
		{
			_view.SetKillsCount(k);
		}

		private void OnLevelChanged(object sender, int l)
		{
			_view.SetLevel(l);
		}

		private void OnNormalizedExperienceChanged(object sender, float e)
		{
			_view.SetNormalizedExperience(e);
		}

		private void OnNormalizedHealthChanged(object sender, float n)
		{
			_view.SetNormalizedHealth(n);
		}
	}
}