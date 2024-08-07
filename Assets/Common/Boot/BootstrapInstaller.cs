using Gameplay;
using Gameplay.Enemies;
using Gameplay.PlayerModule;
using Gameplay.Props;
using ScriptableObjects;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Boot
{
	public class BootstrapInstaller : MonoInstaller
	{
		[SerializeField] private EnemiesManager _enemiesManagerPrefab;
		[SerializeField] private PropsManager _propsManagerPrefab;
		


		public override void InstallBindings()
		{
			Container.Bind<PropsManager>().FromComponentInNewPrefab(_propsManagerPrefab).AsSingle();
			Container.Bind<EnemiesManager>().FromComponentInNewPrefab(_enemiesManagerPrefab).AsSingle();
		}
	}
}