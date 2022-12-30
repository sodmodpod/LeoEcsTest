using System;
using System.Collections.Generic;
using Client.Components;
using Leopotam.EcsLite;
using Server.Components;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class PositionInitSystem : BaseEcsFilterSystem<Transform, Position>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<Transform>();
        }

        protected override void Initialize(EcsWorld ecsWorld, EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                var transform = pools.GetComponent<Transform>(entity);
                ref var position = ref pools.GetComponent<Position>(entity);
                
                position.Value = transform.Value.position;
            }
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
    
        }
    }
}