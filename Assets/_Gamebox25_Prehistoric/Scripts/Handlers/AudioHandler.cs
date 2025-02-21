using System;
using System.Collections.Generic;

public class AudioHandler : IDisposable
{
    private EventBus _eventBus;
    private Puffball _puffball;
    
    public AudioHandler(EventBus eventBus, Puffball puffball)
    {
        _eventBus = eventBus;
        _puffball = puffball;
        
        HandleEvents();
    }

    private void HandleEvents()
    {
        _eventBus.OnAfterParticleAdded += OnAfterParticleAdded;
        _eventBus.OnAfterParticleTransformed += OnAfterParticleTransformed;
        _eventBus.OnWaterEnter += OnWaterEnter;
        _eventBus.OnUiClick += OnUiClick;
    }

    public void Dispose()
    {
        _eventBus.OnAfterParticleAdded -= OnAfterParticleAdded;
        _eventBus.OnAfterParticleTransformed -= OnAfterParticleTransformed;
        _eventBus.OnWaterEnter -= OnWaterEnter;
        _eventBus.OnUiClick -= OnUiClick;
    }

    private void OnAfterParticleAdded(List<Particle> particles)
    {
        _puffball.AudioPuff("ParticleCollect", .1f);
    }

    private void OnAfterParticleTransformed()
    {
        _puffball.AudioPuff("ParticleTransform", 0.4f);
    }

    private void OnWaterEnter()
    {
        _puffball.AudioPuff("WaterSplash", 0.5f);
    }

    private void OnUiClick()
    {
        _puffball.AudioPuff("UiClick", .4f);
    }
}