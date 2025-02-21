using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ElementalPlatform : MonoBehaviour
{
    [SerializeField] protected ChargeType chargeType;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ElementCharger charger))
        {
            charger.SwitchCharge(chargeType);
        }
    }
}