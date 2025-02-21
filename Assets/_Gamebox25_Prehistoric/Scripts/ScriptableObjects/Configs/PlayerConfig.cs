using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 1100)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; } = 5f;  
    
    [field: SerializeField] public float JumpForce { get; } = 6f;  
    
    [field: SerializeField] public float TimeToStop { get; } = 5f;  
}