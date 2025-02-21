using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GroundChecker))]
public class Player : StateMachine
{
    [SerializeField] private MeshRenderer playerBody;
    
    [Header("Required components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GroundChecker groundChecker;

    public Rigidbody Rigidbody => rb;
    public Animator Animator => animator;
    public MeshRenderer PlayerBody => playerBody;
    public PlayerConfig Config => _config;
    public GroundChecker GroundChecker => groundChecker;
    public InputReader InputReader { get; private set; }
        
    protected new PlayerLoopTiming LoopTiming => PlayerLoopTiming.FixedUpdate;
    private EventBus _eventBus;
    private PlayerConfig _config;

    [Inject]
    public void Construct(InputReader reader, PlayerConfig config, EventBus eventBus)
    {
        _config = config;
        _eventBus = eventBus;
        InputReader = reader;
        
        SwitchState(new PlayerMoveState(this));
    }

    private void OnValidate()
    {
        rb ??= GetComponent<Rigidbody>();
        groundChecker ??= GetComponent<GroundChecker>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _eventBus.OnWinGame += OnWinGame;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        _eventBus.OnWinGame -= OnWinGame;
    }

    protected override void UpdateLoop()
    {
        base.UpdateLoop();
    }

    private void OnWinGame()
    {
        SwitchState(new PlayerStopState(this));
    }
}