using System;
using System.Collections.Generic;

public class EventBus
{
    public event Action<List<Particle>> OnAfterParticleAdded;
    public event Action<CameraEnvironmentType> OnEnvironmentChange;
    public event Action OnAfterParticleTransformed;
    public event Action OnWinGame;
    public event Action OnWaterEnter;
    
    #region UI Events
    public event Action<UiWindowType> OnOpenUiWindow;
    public event Action OnUiClick;
    #endregion
    
    public void RaiseAfterParticleAdded(List<Particle> particles)
    {
        OnAfterParticleAdded?.Invoke(particles);
    }

    public void RaiseAfterParticleTransformed()
    {
        OnAfterParticleTransformed?.Invoke();
    }

    public void RaiseBeforeEnvironmentChanged(CameraEnvironmentType cameraType)
    {
        OnEnvironmentChange?.Invoke(cameraType);
    }

    public void RaiseOnWaterEnter()
    {
        OnWaterEnter?.Invoke();
    }

    public void RaiseOnOpenUiWindow(UiWindowType type)
    {
        OnOpenUiWindow?.Invoke(type);
    }

    public void RaiseUiClick()
    {
        OnUiClick?.Invoke();
    }

    public void RaiseAfterGameWinEnded()
    {
        OnWinGame?.Invoke();
    }
}