using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using Unity.Mathematics;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class VelocityResetSystem : BaseEcsFilterSystem<Velocity>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Velocity>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                ref var velocity = ref pools.GetComponent<Velocity>(entity);
                velocity.Value = float3.zero;
            }
        }
    }
}