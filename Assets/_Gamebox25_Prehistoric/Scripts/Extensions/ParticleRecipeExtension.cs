using System.Collections.Generic;

public static class ParticleRecipeExtension
{
    /**
     * Получение списка частиц, которые необходимы для преобразования в новый элемент по рецепту
     */
    public static Dictionary<ElementType, int> GetRequiredParticles(this IReadOnlyList<ParticleVolume> recipe)
    {
        Dictionary<ElementType, int> requiredParticles = new Dictionary<ElementType, int>();
        foreach (ParticleVolume particleVolume in recipe)
        {
            ElementType key = particleVolume.ElementType;
            if (requiredParticles.ContainsKey(key))
            {
                requiredParticles[key] += particleVolume.Amount;
            }
            else
            {
                requiredParticles[key] = particleVolume.Amount;
            }
        }

        return requiredParticles;
    }
    
    /**
     * Получение списка частиц и их количества в текущей очереди частиц
     */
    public static Dictionary<ElementType, int> GetAvailableParticles(this IReadOnlyList<Particle> particles)
    {
        Dictionary<ElementType, int> availableParticles = new Dictionary<ElementType, int>();
        foreach (Particle particle in particles)
        {
            ElementType key = particle.ElementType;
            if (availableParticles.ContainsKey(key))
            {
                availableParticles[key]++;
            }
            else
            {
                availableParticles[key] = 1;
            }
        }

        return availableParticles;
    }

    /**
     * Получение списка частиц на удаление после преобразования по рецепту
     */
    public static List<Particle> GetParticlesToDelete(
        this IReadOnlyList<Particle> particles,
        Dictionary<ElementType, int> requiredParticles
    ) {
        List<Particle> particlesToDelete = new();
        
        Dictionary<ElementType, int> tempRequiredParticles = new Dictionary<ElementType, int>(
            requiredParticles
        );
        foreach (Particle particle in particles)
        {
            ElementType key = particle.ElementType;
            if (tempRequiredParticles.ContainsKey(key))
            {
                if (tempRequiredParticles.TryGetValue(key, out int amount))
                {
                    if (amount > 0)
                    {
                        tempRequiredParticles[key] = amount - 1;
                        particlesToDelete.Add(particle);
                    }
                }
            }
        }

        return particlesToDelete;
    }
}