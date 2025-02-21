using System;
using UnityEngine;

[Serializable]
public class ChargeEffectsLibraryNode
{
    [field: SerializeField] public ChargeType ChargeType { get; private set; } = ChargeType.NoCharge;
    [field: SerializeField] public GameObject ChargeBodyEffectsContainer { get; private set; }
}