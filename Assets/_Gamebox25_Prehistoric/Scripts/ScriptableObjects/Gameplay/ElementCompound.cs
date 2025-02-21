using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementCompound", menuName = "Gameplay/ElementCompound", order = 1100)]
public class ElementCompound : ScriptableObject
{
    [field: SerializeField] public List<ParticleVolume> Recipe { get; } = new();
    [field: SerializeField] public ElementType ElementType { get; } = ElementType.H;
    [field: SerializeField] public GameObject Element { get; }

    private Dictionary<ElementType, int> _requiredParticles = new();

    public bool CanBeTransform(Dictionary<ElementType, int> availableParticles)
    {
        Dictionary<ElementType, int> requiredParticles = GetRequiredParticles();
        return ParticleRecipeHelper.HasAllParticles(requiredParticles, availableParticles);
    }

    public Dictionary<ElementType, int> GetRequiredParticles()
    {
        if (_requiredParticles.Count > 0)
        {
            return _requiredParticles;
        }
        
        _requiredParticles = Recipe.GetRequiredParticles();

        return _requiredParticles;
    }
}