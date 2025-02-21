using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlatformEffector3D : MonoBehaviour
{
    [SerializeField] private Vector3 entryDirection = Vector3.up;
    [SerializeField] private float detectionDistance = 1f;
    [SerializeField] private float passThroughAngle = 45f;
    [SerializeField] private bool localDirection = false;
    [SerializeField] private Collider effectorCollider;
    [SerializeField] private LayerMask mask;

    protected Vector3 Direction => localDirection 
        ? transform.TransformDirection(entryDirection.normalized) 
        : entryDirection;

    protected Vector3 Position => transform.position;

    protected Vector3 Center => effectorCollider.bounds.center;

    private CancellationTokenSource _cts;
    private Vector3 _boxSize;

    private void OnValidate()
    {
        effectorCollider ??= GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _cts = new();
        CalculateBoxSize();
        CheckCollisionAsync(_cts.Token);
    }
    
    private void OnDisable()
    {
        _cts?.Cancel();
    }
    
    private void CalculateBoxSize()
    {
        if (effectorCollider is BoxCollider boxCollider)
        {
            _boxSize = boxCollider.size;
        }
        else if (effectorCollider is CapsuleCollider capsuleCollider)
        {
            _boxSize = capsuleCollider.GetSize();
        }
        else
        {
            _boxSize = effectorCollider.bounds.size;
        }
    }

    private async void CheckCollisionAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Vector3 checkPosition = Center - Direction * detectionDistance;
            if (Physics.CheckBox(checkPosition, _boxSize / 2, Quaternion.identity, mask))
            {
                Collider[] colliders = Physics.OverlapBox(
                    checkPosition,
                    _boxSize / 2f,
                    Quaternion.identity,
                    mask
                );
                
                bool shouldTrigger = false;
                foreach (Collider checkCollider in colliders)
                {
                    Vector3 toCollider = (checkCollider.transform.position - Position).normalized;
                    float angle = Vector3.Angle(-Direction, toCollider);

                    if (angle <= passThroughAngle)
                    {
                        shouldTrigger = true;
                        break;
                    }
                }

                effectorCollider.isTrigger = shouldTrigger;
            }
            else
            {
                effectorCollider.isTrigger = false;
            }

            bool isCanceled = await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token).SuppressCancellationThrow();
            if (isCanceled)
            {
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Направление, с которого нельзя пройти через платформу
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Position, Direction);
        
        // Направление, с которого можно пройти через платформу
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Position, -Direction);
        
        // Центр проверки OverlapBox
        Gizmos.color = Color.yellow;
        Vector3 checkPosition = Center - Direction * detectionDistance;
        Gizmos.DrawSphere(checkPosition, .1f);
        
        // Границы OverlapBox
        Gizmos.matrix = Matrix4x4.TRS(checkPosition, Quaternion.identity, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, _boxSize);
        Gizmos.matrix = Matrix4x4.identity;
        
        // Визуализация границы угла
        Gizmos.color = Color.white;
        Vector3 leftLimit = Quaternion.AngleAxis(-passThroughAngle, Vector3.forward) * -Direction;
        Vector3 rightLimit = Quaternion.AngleAxis(passThroughAngle, Vector3.forward) * -Direction;

        Gizmos.DrawRay(Position, leftLimit * 2f);
        Gizmos.DrawRay(Position, rightLimit * 2f);
    }
}