using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExplodingPlatform : MonoBehaviour
{
    [SerializeField] private List<ParticleVolume> particlesToExplodePlatform = new();
    [SerializeField] protected ChargeType chargeToExplode;
    
    private Dictionary<ElementType, int> _requiredParticles = new();
    private ParticleCollector _collector;

    private void OnCollisionEnter(Collision collision)
    {
        if (
            collision.gameObject.TryGetComponent(out ElementCharger charger)
            && collision.gameObject.TryGetComponent(out ParticleCollector collector)
        ) {
            _collector = collector;
            if (CheckExplode(charger.CurrentCharge))
            {
                ExplodePlatform();
                charger.ClearCharge();
            }
        }
    }

    private bool CheckExplode(ChargeType currentCharge)
    {
        if (!currentCharge.Equals(chargeToExplode))
        {
            return false;
        }
        
        Dictionary<ElementType, int> requiredParticles = GetRequiredParticles();
        Dictionary<ElementType, int> availableParticles = _collector.Queue.GetAvailableParticles();
        
        return ParticleRecipeHelper.HasAllParticles(requiredParticles, availableParticles);
    }

    private Dictionary<ElementType, int> GetRequiredParticles()
    {
        if (_requiredParticles.Count > 0)
        {
            return _requiredParticles;
        }
        
        _requiredParticles = particlesToExplodePlatform.GetRequiredParticles();

        return _requiredParticles;
    }

    private void ExplodePlatform()
    {
        DeleteParticles();
        gameObject.SetActive(false);
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