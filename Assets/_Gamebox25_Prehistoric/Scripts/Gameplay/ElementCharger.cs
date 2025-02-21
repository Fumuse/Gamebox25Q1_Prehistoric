using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ElementCharger : MonoBehaviour
{
    [SerializeField] private List<ChargeEffectsLibraryNode> chargeEffects = new();
    
    private ChargeType _currentCharge = ChargeType.NoCharge;
    public ChargeType CurrentCharge => _currentCharge;

    private void Awake()
    {
        ClearCharge();
    }

    public void SwitchCharge(ChargeType targetCharge)
    {
        _currentCharge = targetCharge;
        ChangeChargeBodyEffect(targetCharge);
    }

    public void ClearCharge()
    {
        SwitchCharge(ChargeType.NoCharge);
    }

    private void ChangeChargeBodyEffect(ChargeType targetCharge)
    {
        foreach (ChargeEffectsLibraryNode node in chargeEffects)
        {
            if (node.ChargeBodyEffectsContainer != null)
            {
                node.ChargeBodyEffectsContainer.SetActive(false);
            }
        }

        ChargeEffectsLibraryNode targetNode =
            chargeEffects.FirstOrDefault(effect => effect.ChargeType.Equals(targetCharge));
        if (targetNode != null)
        {
            if (targetNode.ChargeBodyEffectsContainer != null)
            {
                targetNode.ChargeBodyEffectsContainer.SetActive(true);
            }
        }
    }
}