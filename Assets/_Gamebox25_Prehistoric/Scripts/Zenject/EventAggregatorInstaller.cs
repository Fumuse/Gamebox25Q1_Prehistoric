using Zenject;

public class EventAggregatorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EventBus>().FromNew().AsSingle().NonLazy();
    }
}