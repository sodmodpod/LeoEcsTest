using System;
using System.Collections.Generic;
using Client.Components;
using Leopotam.EcsLite;
using Server.Components;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class TransformPositionApplySystem : BaseEcsFilterSystem<Transform, Position>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<Transform>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                var transform = pools.GetComponent<Transform>(entity);
                var position = pools.GetComponent<Position>(entity);
                
                transform.Value.position = position.Value;
            }
        }
    }
}