using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class WinPoint : MonoBehaviour
{
    [SerializeField] private Collider pointCollider;

    private EventBus _eventBus;
    
    [Inject]
    public void Constructor(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void OnValidate()
    {
        pointCollider ??= GetComponent<Collider>();
        if (pointCollider != null)
        {
            pointCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        _eventBus.RaiseAfterGameWinEnded();
    }
}