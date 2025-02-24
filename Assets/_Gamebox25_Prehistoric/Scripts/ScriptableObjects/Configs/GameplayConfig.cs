using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig", order = 1100)]
public class GameplayConfig : ScriptableObject
{
    [field: SerializeField] public float ParticleCauldronSpawnRadius { get; private set; } = 2f;
    [field: SerializeField] public float ParticleFollowSpeed { get; private set; } = 10f;
}