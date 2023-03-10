using System;
using System.ComponentModel;
using Client.Services;
using Leopotam.EcsLite;
using UnityEngine.PlayerLoop;
using Zenject;

namespace UnityTemplateProjects.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance(new EcsWorld()).AsSingle().NonLazy();
            Container.BindInterfacesTo<DesktopInputService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ClientTimeService>().AsSingle().NonLazy();
        }
    }
}