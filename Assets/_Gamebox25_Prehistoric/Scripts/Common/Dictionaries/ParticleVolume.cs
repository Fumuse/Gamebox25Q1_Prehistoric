using System;
using UnityEngine;

[Serializable]
public class ParticleVolume
{
    [field: SerializeField] public ElementType ElementType { get; private set; }
    [field: SerializeField] public int Amount { get; private set; } = 1;
}