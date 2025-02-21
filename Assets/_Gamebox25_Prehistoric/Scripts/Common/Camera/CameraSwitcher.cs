using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Zenject;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private List<VirtualCameraContext> cameras = new();

    private VirtualCameraContext _currentCamera;
    private CancellationTokenSource _cts;
    private EventBus _eventBus;

    [Inject]
    public void Constructor(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Awake()
    {
        VirtualCameraContext context = cameras.FirstOrDefault(
            cameraPair => cameraPair.Type == CameraEnvironmentType.Default
        );
        if (context != null)
        {
            _currentCamera = context;
            UpdateCameras(context);
        }
    }

    private void OnEnable()
    {
        _cts = new();

        _eventBus.OnEnvironmentChange += OnEnvironmentChange;
    }

    private void OnDisable()
    {
        _cts?.Cancel();

        _eventBus.OnEnvironmentChange -= OnEnvironmentChange;
    }

    public void SwitchCamera(VirtualCameraContext context)
    {
        UpdateCameras(context);
        _currentCamera = context;
    }

    private void UpdateCameras(VirtualCameraContext targetContext)
    {
        foreach (VirtualCameraContext vCamera in cameras)
        {
            vCamera.Camera.Priority = 10;
            if (vCamera.EnvironmentBackgroundEffects != null)
            {
                vCamera.EnvironmentBackgroundEffects.SetActive(false);
            }
        }
        
        targetContext.Camera.Priority = 11;
        if (targetContext.EnvironmentBackgroundEffects != null)
        {
            targetContext.EnvironmentBackgroundEffects.SetActive(true);
        }
    }

    private void OnEnvironmentChange(CameraEnvironmentType type)
    {
        VirtualCameraContext context = cameras.FirstOrDefault(
            cameraPair => cameraPair.Type == type
        );
        if (context != null)
        {
            SwitchCamera(context);
        }
    }
}
