using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace UnityTemplateProjects.Systems
{
    public abstract class BaseEcsFilterSystem<T1, T2, T3, T4, T5> : BaseEcsFilterSystem where T1: struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().Inc<T2>().Inc<T3>().Inc<T4>().Inc<T5>().End();
        }
    }
    
    public abstract class BaseEcsFilterSystem<T1, T2, T3, T4> : BaseEcsFilterSystem where T1: struct where T2 : struct where T3 : struct where T4 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().Inc<T2>().Inc<T3>().Inc<T4>().End();
        }
    }
    
    public abstract class BaseEcsFilterSystem<T1, T2, T3> : BaseEcsFilterSystem where T1: struct where T2 : struct where T3 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().Inc<T2>().Inc<T3>().End();
        }
    }
    
    public abstract class BaseEcsFilterSystem<T1, T2> : BaseEcsFilterSystem where T1: struct where T2 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().Inc<T2>().End();
        }
    }
    
    public abstract class BaseEcsFilterSystem<T1> : BaseEcsFilterSystem where T1 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().End();
        }
    }

    public abstract class BaseEcsFilterSystem : BaseEcsSystem
    {
        private EcsFilter _entities;

        protected override void OnInit(IEcsSystems systems, EcsWorld ecsWorld)
        {
            _entities = InitFilter(ecsWorld);
            Initialize(ecsWorld, _entities, Pools);
        }

        protected override void OnRun(IEcsSystems systems, EcsWorld ecsWorld)
        {
            Execute(_entities, Pools, systems, ecsWorld);
        }

        protected virtual void Initialize(EcsWorld ecsWorld, EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
        }

        protected abstract EcsFilter InitFilter(EcsWorld ecsWorld);

        public void Run(IEcsSystems systems, EcsWorld ecsWorld)
        {
            Execute(_entities, Pools, systems, ecsWorld);
        }


        protected abstract void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld);

    }
}