using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class WinPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup alphaGroup;
    
    private EventBus _eventBus;

    private void OnValidate()
    {
        alphaGroup ??= GetComponent<CanvasGroup>();
    }
    
    [Inject]
    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Awake()
    {
        alphaGroup.alpha = 0f;
    }

    private void OnEnable()
    {
        _eventBus.OnWinGame += OnWinGame;
    }

    private void OnDisable()
    {
        _eventBus.OnWinGame -= OnWinGame;
    }

    private void OnWinGame()
    {
        alphaGroup.alpha = 1f;
    }
}