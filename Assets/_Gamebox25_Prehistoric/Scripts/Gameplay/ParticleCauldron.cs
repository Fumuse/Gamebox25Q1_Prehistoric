using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleCauldron : IDisposable
{
    private EventBus _eventBus;
    private ParticleCollector _collector;
    private ElementsLibrary _library;
    private GameplayConfig _gameplayConfig;
    private ElementFactory _elementFactory;
    
    public ParticleCauldron(
        EventBus eventBus,
        ParticleCollector collector,
        ElementsLibrary library,
        GameplayConfig gameplayConfig,
        ElementFactory elementFactory
    ) {
        _eventBus = eventBus;
        _collector = collector;
        _library = library;
        _gameplayConfig = gameplayConfig;
        _elementFactory = elementFactory;

        HandleEvents();
    }

    private void HandleEvents()
    {
        _eventBus.OnAfterParticleAdded += OnAfterParticleAdded;
    }
    
    public void Dispose()
    {
        _eventBus.OnAfterParticleAdded -= OnAfterParticleAdded;
    }

    /**
     * Создание нового элемента на сцене
     */
    private void OnAfterParticleAdded(List<Particle> particles)
    {
        ElementCompound toTransform = _library.GetElementToTransform(particles.GetAvailableParticles());
        if (toTransform != null)
        {
            DeleteTransformedParticles(toTransform, particles);
            
            Vector3 randomPoint = _collector.transform.position 
                                  + new Vector3(
                                    Random.Range(
                                          -_gameplayConfig.ParticleCauldronSpawnRadius,
                                          _gameplayConfig.ParticleCauldronSpawnRadius
                                      ),
                                    0, 0
                                );

            ElementFactoryContext context = new ElementFactoryContext(toTransform.Element, randomPoint);
            _elementFactory.Create(context);
            
            _eventBus.RaiseAfterParticleTransformed();
        }
    }

    /**
     * Удаление частиц, использованных по рецепту
     */
    private void DeleteTransformedParticles(ElementCompound elementCompound, List<Particle> particles)
    {
        List<Particle> particlesToDelete = particles.GetParticlesToDelete(elementCompound.GetRequiredParticles());
        foreach (Particle particle in particlesToDelete)
        {
            _collector.Delete(particle);
        }
    }
}
