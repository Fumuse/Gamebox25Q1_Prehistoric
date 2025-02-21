using System;
using UnityEngine;

[Serializable]
public class AudioLibraryNode
{
    [field: SerializeField] public string Label { get; private set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }
}