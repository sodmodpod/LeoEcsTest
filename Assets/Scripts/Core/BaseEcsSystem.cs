using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace UnityTemplateProjects.Systems
{
    public abstract class BaseEcsSystem : IEcsInitSystem, IEcsRunSystem
    {
        protected readonly Dictionary<Type, IEcsPool> Pools = new();
        private EcsWorld _ecsWorld;
        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld(GetWorldName());
            foreach (var pool in GetPools(systems.GetWorld()))
            {
                Pools.Add(pool.GetComponentType(), pool);
            }
            
            OnInit(systems, _ecsWorld);
        }

        public void Run(IEcsSystems systems)
        {
            OnRun(systems, _ecsWorld);
        }
        
        protected virtual string GetWorldName()
        {
            return null;
        }
        
        public abstract IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld);

        protected abstract void OnRun(IEcsSystems systems, EcsWorld ecsWorld);
        protected abstract void OnInit(IEcsSystems systems, EcsWorld ecsWorld);
    }
}