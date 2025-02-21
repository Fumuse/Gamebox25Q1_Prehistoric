using UnityEngine;
using Zenject;

public class PuffballsInstaller : MonoInstaller
{
    [SerializeField] private AudioLibrary audioLibrary;
    [SerializeField] private AudioSource[] puffAudioSources;
    
    public override void InstallBindings()
    {
        BindPuffball();
        BindAudioHandler();
    }

    private void BindPuffball()
    {
        Container.BindInterfacesAndSelfTo<AudioLibrary>().FromInstance(audioLibrary).AsSingle().NonLazy();
        Container.Bind<Puffball>().FromNew().AsSingle().WithArguments(puffAudioSources);
    }

    private void BindAudioHandler()
    {
        Container.BindInterfacesAndSelfTo<AudioHandler>().FromNew().AsSingle().NonLazy();
    }
}