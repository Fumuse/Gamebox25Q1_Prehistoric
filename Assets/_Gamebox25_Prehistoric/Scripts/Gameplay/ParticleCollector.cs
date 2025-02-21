using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class ParticleCollector : MonoBehaviour
{
    [SerializeField] private Vector3 startOffset = new Vector3(.75f, 0f, 0f);
    [SerializeField] private Transform collectorPoint;
    
    public IReadOnlyList<Particle> Queue => _particleQueue;

    private EventBus _eventBus;
    private CancellationTokenSource _cts;
    private List<Particle> _particleQueue = new();
    private Vector3 _lastPosition;
    private Vector3 _lastDirection = Vector3.zero;

    [Inject]
    public void Constructor(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void OnEnable()
    {
        _cts = new();
        TrackQueueDirection(_cts.Token);
    }

    private void OnDisable()
    {
        _cts?.Cancel();
    }

    private void Start()
    {
        _lastPosition = collectorPoint.position;
    }

    public void Add(Particle particle)
    {
        if (_particleQueue.Contains(particle))
        {
            return;
        }
        
        Particle target = _particleQueue.LastOrDefault();
        if (target)
        {
            particle.Collect(target.transform);
        }
        else
        {
            particle.Collect(collectorPoint, startOffset);
        }
        
        _particleQueue.Add(particle);
        
        _eventBus.RaiseAfterParticleAdded(_particleQueue);
    }

    public void Remove(Particle particle)
    {
        if (!_particleQueue.Contains(particle))
        {
            return;
        }

        ChangeFollowBinds(particle);
        
        particle.Unhook();
        _particleQueue.Remove(particle);
    }

    public void Delete(Particle particle)
    {
        if (!_particleQueue.Contains(particle))
        {
            return;
        }

        ChangeFollowBinds(particle);
        
        particle.Delete();
        _particleQueue.Remove(particle);
    }

    public void Clean()
    {
        foreach (Particle particle in _particleQueue)
        {
            Remove(particle);
        }
        _particleQueue.Clear();
    }

    public void ChangeFollowBinds(Particle particle)
    {
        if (particle.FollowTarget != null)
        {
            foreach (Particle particleInList in _particleQueue)
            {
                if (particleInList.FollowTarget.Equals(particle.transform))
                {
                    particleInList.FollowTarget = particle.FollowTarget != null 
                        ? particle.FollowTarget 
                        : collectorPoint;
                }
            }
        }
    }

    private async void TrackQueueDirection(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            bool isCanceled = await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token).SuppressCancellationThrow();
            if (isCanceled)
            {
                return;
            }

            Vector3 direction = (collectorPoint.position - _lastPosition).normalized;
            if (direction != _lastDirection && direction != Vector3.zero)
            {
                foreach (var particle in _particleQueue)
                {
                    particle.InverseDirection(direction);
                }

                _lastDirection = direction;
            }

            _lastPosition = collectorPoint.position;
        }
    }
}