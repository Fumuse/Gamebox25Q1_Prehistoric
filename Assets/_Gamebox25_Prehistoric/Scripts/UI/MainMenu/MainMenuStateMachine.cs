using UnityEngine;
using Zenject;

public class MainMenuStateMachine : StateMachine
{
    [SerializeField] private GameObject initialWindow;
    [SerializeField] private GameObject controlsWindow;

    public GameObject InitialWindow => initialWindow;
    public GameObject ControlsWindow => controlsWindow;
    
    public EventBus EventBus { get; private set; }
    
    [Inject]
    public void Constructor(EventBus eventBus)
    {
        EventBus = eventBus;
    }

    private void Start()
    {
        HideWindowsOnStart();
        SwitchState(new InitialState(this));
    }

    private void HideWindowsOnStart()
    {
        controlsWindow.SetActive(false);
    }
}