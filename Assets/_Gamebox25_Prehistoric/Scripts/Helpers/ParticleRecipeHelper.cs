using System.Collections.Generic;

public static class ParticleRecipeHelper
{
    public static bool HasAllParticles(
        Dictionary<ElementType, int> requiredParticles,
        Dictionary<ElementType, int> availableParticles
    ) {
        foreach (var required in requiredParticles)
        {
            if (
                !availableParticles.TryGetValue(required.Key, out int availableCount)
                || availableCount < required.Value
            )
            {
                return false;
            }
        }

        return true;
    }
}