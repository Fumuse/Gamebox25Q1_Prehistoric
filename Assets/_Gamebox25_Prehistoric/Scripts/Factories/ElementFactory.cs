using UnityEngine;
using Zenject;

public class ElementFactory : PlaceholderFactory<ElementFactoryContext, GameObject>
{
    private readonly DiContainer _container;

    public ElementFactory(DiContainer container)
    {
        _container = container;
    }

    public override GameObject Create(ElementFactoryContext context)
    {
        GameObject particle =
            _container.InstantiatePrefab(context.Element, context.SpawnPoint, Quaternion.identity, null);
        particle.SetActive(true);
        return particle;
    }
}