using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio Library", order = 220)]
public class AudioLibrary : ScriptableObject
{
    [SerializeField] private List<AudioLibraryNode> audios = new();

    [CanBeNull]
    public AudioClip this[string label]
    {
        get
        {
            AudioLibraryNode clipNode = audios.FirstOrDefault(node => node.Label.Equals(label));
            return clipNode?.Clip;
        }
    }
}