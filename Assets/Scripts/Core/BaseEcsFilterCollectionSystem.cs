using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;

namespace UnityTemplateProjects.Systems
{
    public abstract class BaseEcsFilterCollectionSystem : BaseEcsSystem
    {
        private EcsFilter[] Entities;

        protected override void OnInit(IEcsSystems systems, EcsWorld ecsWorld)
        {
            Entities = InitFilters(systems.GetWorld()).ToArray();
            Initialize(Entities, Pools, systems, ecsWorld);
        }

        protected virtual void Initialize(EcsFilter[] entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
        }

        protected override void OnRun(IEcsSystems systems, EcsWorld ecsWorld)
        {
            Execute(Entities, Pools, systems, ecsWorld);
        }

        protected abstract IEnumerable<EcsFilter> InitFilters(EcsWorld ecsWorld);
        
        protected abstract void Execute(EcsFilter[] entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld);

    }
}