using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class WaterZone : MonoBehaviour
{
    [SerializeField] private float depthBeforeSubmerged = 3f;
    [SerializeField] private float displacementAmount = 3f;
    [SerializeField] private float waveHeight = 1f;
    [SerializeField] private Collider waterCollider;

    private EventBus _eventBus;

    [Inject]
    public void Constructor(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void OnValidate()
    {
        waterCollider ??= GetComponent<Collider>();
        if (waterCollider != null)
        {
            waterCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _eventBus.RaiseBeforeEnvironmentChanged(CameraEnvironmentType.Water);
            _eventBus.RaiseOnWaterEnter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Floater floater))
        {
            floater.WaveForce(waveHeight, depthBeforeSubmerged, displacementAmount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _eventBus.RaiseBeforeEnvironmentChanged(CameraEnvironmentType.Default);
        }
    }
}