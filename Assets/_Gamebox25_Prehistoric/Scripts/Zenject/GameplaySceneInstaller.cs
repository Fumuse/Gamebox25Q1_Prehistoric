using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameplayConfig gameplayConfig;
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private ElementsLibrary elementsLibrary;
    
    public override void InstallBindings()
    {
        BindCamera();
        BindInputReader();
        BindConfigs();
        BindPlayer();
        BindParticleWorkers();
    }

    private void BindCamera()
    {
        Container.Bind<Camera>().FromInstance(mainCamera).AsSingle().NonLazy();
    }
    
    private void BindInputReader()
    {
        Container.BindInterfacesAndSelfTo<InputReader>().AsSingle().NonLazy();
    }

    private void BindConfigs()
    {
        Container.BindInterfacesAndSelfTo<GameplayConfig>().FromInstance(gameplayConfig).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerConfig>().FromInstance(playerConfig).AsSingle().NonLazy();
    }

    private void BindPlayer()
    {
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
    }

    private void BindParticleWorkers()
    {
        Container.BindFactory<ElementFactoryContext, GameObject, ElementFactory>().AsSingle();
        Container.Bind<ParticleCollector>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<ElementsLibrary>().FromInstance(elementsLibrary).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ParticleCauldron>().FromNew().AsSingle().NonLazy();
    }
}