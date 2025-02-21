using System;
using Cinemachine;
using UnityEngine;

[Serializable]
public class VirtualCameraContext
{
    [field: SerializeField] public CameraEnvironmentType Type { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera Camera { get; private set; }
    [field: SerializeField] public GameObject EnvironmentBackgroundEffects { get; private set; }
}