using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace UnityTemplateProjects.Installers
{
    public abstract class BaseEcsSystemsInstaller : MonoInstaller
    {
        [SerializeField] private bool _isGlobal;
        [SerializeField] private EcsExecutor _ecsExecutor;

        public override void InstallBindings()
        {
            OnInstallBindings();
            
            Container.Bind(typeof(BaseSystemsContainer), typeof(UpdateSystemsContainer)).To<UpdateSystemsContainer>().AsTransient().WithArguments(_isGlobal);
            Container.Bind(typeof(BaseSystemsContainer), typeof(FixedUpdateSystemsContainer)).To<FixedUpdateSystemsContainer>().AsTransient().WithArguments(_isGlobal);
            Container.Bind(typeof(BaseSystemsContainer), typeof(LateUpdateSystemsContainer)).To<LateUpdateSystemsContainer>().AsTransient().WithArguments(_isGlobal);
            
            Container.BindSystem<EcsWorldDebugSystem, UpdateSystemsContainer>();
            
            Container.InstantiatePrefabForComponent<EcsExecutor>(_ecsExecutor);
        }

        protected abstract void OnInstallBindings();
    }
}

    