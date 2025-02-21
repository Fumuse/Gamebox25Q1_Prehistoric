using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlatformBuilder : MonoBehaviour
{
    [SerializeField] private List<ParticleVolume> particlesToBuildPlatform = new();
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject builderView;
    [SerializeField] private Collider builderCollider;
    
    private Dictionary<ElementType, int> _requiredParticles = new();
    private ParticleCollector _collector;

    private void Awake()
    {
        platform.SetActive(false);
    }

    private void OnValidate()
    {
        builderCollider ??= GetComponent<Collider>();
        if (builderCollider != null)
        {
            builderCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ParticleCollector collector))
        {
            _collector = collector;
            if (CheckCreate())
            {
                CreatePlatform();
            }
        }
    }

    private Dictionary<ElementType, int> GetRequiredParticles()
    {
        if (_requiredParticles.Count > 0)
        {
            return _requiredParticles;
        }
        
        _requiredParticles = particlesToBuildPlatform.GetRequiredParticles();

        return _requiredParticles;
    }

    private bool CheckCreate()
    {
        Dictionary<ElementType, int> requiredParticles = GetRequiredParticles();
        Dictionary<ElementType, int> availableParticles = _collector.Queue.GetAvailableParticles();

        return ParticleRecipeHelper.HasAllParticles(requiredParticles, availableParticles);
    }

    private void CreatePlatform()
    {
        platform.SetActive(true);
        builderCollider.enabled = false;
        builderView.SetActive(false);

        DeleteParticles();
    }

    private void DeleteParticles()
    {
        List<Particle> particlesToDelete = _collector.Queue.GetParticlesToDelete(GetRequiredParticles());
        foreach (Particle particle in particlesToDelete)
        {
            _collector.Delete(particle);
        }
    }
}