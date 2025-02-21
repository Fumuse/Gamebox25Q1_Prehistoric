using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class Particle : MonoBehaviour
{
    [SerializeField] private Collider particleCollider;
    [SerializeField] protected ParticleType particleType = ParticleType.Simple;
    [SerializeField] protected ElementType elementType = ElementType.Proton;

    private GameplayConfig _gameplayConfig;
    private Vector3 _offset = new Vector3(.75f, 0, 0);
    private Vector3 _movementDirection = Vector3.right;
    private CancellationTokenSource _cts;

    public ParticleType Type => particleType;
    public ElementType ElementType => elementType;
    public Transform FollowTarget { get; set; }

    [Inject]
    public void Construct(GameplayConfig gameplayConfig)
    {
        _gameplayConfig = gameplayConfig;
    }
    
    protected virtual void OnValidate()
    {
        particleCollider ??= GetComponent<Collider>();
        if (particleCollider != null)
        {
            particleCollider.isTrigger = true;
        }
    }

    private void OnDisable()
    {
        _cts?.Cancel();
    }

    /**
     * Добавление частицы для следования за Игроком
     */
    public void Collect(Transform target, Vector3 offset = default)
    {
        FollowTarget = target;
        if (offset != default)
        {
            _offset = offset;
        }
        
        _cts = new();
        Follow(_cts.Token);
    }

    /**
     * Отцепление частицы от Игрока
     */
    public void Unhook()
    {
        FollowTarget = null;
        
        _cts?.Cancel();
    }

    /**
     * Сокрытие частицы со сцены
     */
    public void Delete()
    {
        Unhook();
        Destroy(gameObject);
    }

    /**
     * Изменение направления следования частицы
     */
    public void InverseDirection(Vector3 direction)
    {
        _movementDirection = direction;
    }

    /**
     * Следование частицы за Игроком
     */
    private async void Follow(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (FollowTarget != null && transform != null)
            {
                Vector3 targetPosition = FollowTarget.position - (_offset * _movementDirection.x);
                transform.position = Vector3.Lerp(
                    transform.position,
                    targetPosition,
                    Time.deltaTime * _gameplayConfig.ParticleFollowSpeed
                );
            }
            
            bool isCanceled = await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token).SuppressCancellationThrow();
            if (isCanceled)
            {
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ParticleCollector collector))
        {
            collector.Add(this);
        }
    }
}