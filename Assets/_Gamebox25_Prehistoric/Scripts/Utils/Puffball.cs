using System.Linq;
using UnityEngine;

public class Puffball
{
    private AudioSource[] _defaultSources;
    private AudioLibrary _library;
    
    public Puffball(AudioSource[] defaultSources, AudioLibrary library)
    {
        _defaultSources = defaultSources;
        _library = library;
    }
    
    public void AudioPuff(AudioClip clip, float volume = default, AudioSource source = null)
    {
        if (clip == null)
        {
            return;
        }
        
        if (source != null)
        {
            source.PlayOneShot(clip, volume);
        }
        else
        {
            GetFreeSource().PlayOneShot(clip, volume);
        }
    }

    public void AudioPuff(string audioLabel, float volume = default, AudioSource source = null)
    {
        AudioClip clip = _library[audioLabel];
        AudioPuff(clip, volume, source);
    }

    private AudioSource GetFreeSource()
    {
        foreach (AudioSource source in _defaultSources)
        {
            if (source.isPlaying)
            {
                continue;
            }
            return source;
        }

        return _defaultSources.FirstOrDefault();
    }
}