using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;
    private CancellationTokenSource _cts;

    protected virtual PlayerLoopTiming LoopTiming => PlayerLoopTiming.Update;

    public void SwitchState(State state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    protected virtual void OnEnable()
    {
        _cts?.Cancel();
        UpdateLoop();
    }

    protected virtual void OnDisable()
    {
        _cts?.Cancel();
    }

    protected virtual async void UpdateLoop()
    {
        _cts = new();

        while (true)
        {
            AsyncUpdate();

            bool isCanceled = await UniTask.Yield(LoopTiming, _cts.Token).SuppressCancellationThrow();
            if (isCanceled) return;
        }
    }

    private void AsyncUpdate()
    {
        currentState?.Tick();
    }

    private void OnDestroy()
    {
        currentState?.Exit();
    }
}