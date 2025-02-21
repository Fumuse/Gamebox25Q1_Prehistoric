using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig", order = 1100)]
public class GameplayConfig : ScriptableObject
{
    [field: SerializeField] public float ParticleCauldronSpawnRadius { get; } = 2f;
    [field: SerializeField] public float ParticleFollowSpeed { get; } = 10f;
}