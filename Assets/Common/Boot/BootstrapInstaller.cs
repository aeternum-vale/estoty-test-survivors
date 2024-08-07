using Meta;
using UnityEngine;
using Zenject;

namespace Boot
{
	public class BootstrapInstaller : MonoInstaller
	{
		[SerializeField] private Main _mainPrefab;

		public override void InstallBindings()
		{
			Container.InstantiatePrefabForComponent<Main>(_mainPrefab);
		}
	}
}