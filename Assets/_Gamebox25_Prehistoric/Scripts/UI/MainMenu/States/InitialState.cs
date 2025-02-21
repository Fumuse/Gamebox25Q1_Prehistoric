public class InitialState : State
{
    private MainMenuStateMachine _stateMachine;
    private EventBus _eventBus;
    
    public InitialState(MainMenuStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _eventBus = _stateMachine.EventBus;
    }

    public override void Enter()
    {
        _stateMachine.InitialWindow.SetActive(true);
        _eventBus.OnOpenUiWindow += OnOpenUiWindow;
    }

    public override void Tick()
    {}

    public override void Exit()
    {
        _stateMachine.InitialWindow.SetActive(false);
        _eventBus.OnOpenUiWindow -= OnOpenUiWindow;
    }
    
    private void OnOpenUiWindow(UiWindowType uiWindowType)
    {
        if (uiWindowType == UiWindowType.MainMenuDefault)
        {
            return;
        }

        if (uiWindowType == UiWindowType.MainMenuControls)
        {
            _stateMachine.SwitchState(new ControlsState(_stateMachine));
        }
    }
}