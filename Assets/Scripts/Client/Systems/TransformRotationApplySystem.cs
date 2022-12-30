using System;
using System.Collections.Generic;
using Client.Components;
using Leopotam.EcsLite;
using Server.Components;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class TransformRotationApplySystem : BaseEcsFilterSystem<Transform, Rotation>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Rotation>();
            yield return ecsWorld.GetPool<Transform>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                var transform = pools.GetComponent<Transform>(entity);
                var rotation = pools.GetComponent<Rotation>(entity);
                
                transform.Value.rotation = rotation.Value;
            }
        }
    }
}