using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform feets;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkGroundDistance = .5f;
    
    public bool IsGrounded => Physics.OverlapSphere(
        feets.position, 
        checkGroundDistance, 
        groundLayer
    ).Length > 0;
}