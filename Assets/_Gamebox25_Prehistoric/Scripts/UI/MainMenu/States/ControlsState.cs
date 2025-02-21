public class ControlsState : State
{
    private MainMenuStateMachine _stateMachine;
    private EventBus _eventBus;
    
    public ControlsState(MainMenuStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _eventBus = _stateMachine.EventBus;
    }

    public override void Enter()
    {
        _stateMachine.ControlsWindow.SetActive(true);
        _eventBus.OnOpenUiWindow += OnOpenUiWindow;
    }

    public override void Tick()
    {}

    public override void Exit()
    {
        _stateMachine.ControlsWindow.SetActive(false);
        _eventBus.OnOpenUiWindow -= OnOpenUiWindow;
    }

    private void OnOpenUiWindow(UiWindowType uiWindowType)
    {
        if (uiWindowType == UiWindowType.MainMenuControls)
        {
            return;
        }

        if (uiWindowType == UiWindowType.MainMenuDefault)
        {
            _stateMachine.SwitchState(new InitialState(_stateMachine));
        }
    }
}