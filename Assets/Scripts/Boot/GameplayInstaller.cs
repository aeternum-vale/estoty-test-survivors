using Gameplay;
using Gameplay.Enemies;
using Gameplay.PlayerModule;
using Gameplay.Props;
using UI;
using UnityEngine;
using Zenject;

namespace Boot
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private Transform _bulletsParent;
		[SerializeField] private Transform _propsParent;
		[SerializeField] private Transform _enemiesParent;
		[SerializeField] private GameObject _gameOverScreen;

		[SerializeField] private Joystick _joystick;
		[SerializeField] private UIManager _uiManager;

		[SerializeField] private Player _playerPrefab;
		[SerializeField] private Presenter _presenterPrefab;
		[SerializeField] private CameraFollower _cameraFollowerPrefab;
		[SerializeField] private Game _gamePrefab;
		[SerializeField] private EnemiesManager _enemiesManagerPrefab;
		[SerializeField] private PropsManager _propsManagerPrefab;


		public override void InstallBindings()
		{
			var propsManager = Container.InstantiatePrefabForComponent<PropsManager>(_propsManagerPrefab);
			propsManager.PropsParent = _propsParent;
			Container.Bind<PropsManager>().FromInstance(propsManager).AsSingle();

			var enemiesManager = Container.InstantiatePrefabForComponent<EnemiesManager>(_enemiesManagerPrefab);
			enemiesManager.EnemiesParent = _enemiesParent;
			Container.Bind<EnemiesManager>().FromInstance(enemiesManager).AsSingle();

			Container.Bind<IInputSource>().To<Joystick>().FromComponentInHierarchy(_joystick).AsSingle();

			var player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab);
			Container.Bind<Player>().FromInstance(player).AsSingle();
			player.Gun.BulletsParent = _bulletsParent;
			Container.Bind<Gun>().FromInstance(player.Gun).AsSingle();

			Container.Bind<IView>().To<UIManager>().FromComponentInHierarchy(_uiManager).AsSingle();
			
			Container.InstantiatePrefabForComponent<Presenter>(_presenterPrefab);

			var camera = Container.InstantiatePrefabForComponent<CameraFollower>(_cameraFollowerPrefab);
			camera.Target = player.transform;

			var game = Container.InstantiatePrefabForComponent<Game>(_gamePrefab);
			game.GameOverScreen = _gameOverScreen;
		}
	}
}