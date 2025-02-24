using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 1100)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; } = 5f;  
    
    [field: SerializeField] public float JumpForce { get; private set; } = 6f;  
    
    [field: SerializeField] public float TimeToStop { get; private set; } = 5f;  
}