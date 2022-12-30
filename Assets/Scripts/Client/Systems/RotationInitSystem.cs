using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using UnityEngine;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;
using Transform = Client.Components.Transform;

namespace Client.Systems
{
    public class RotationInitSystem : BaseEcsFilterSystem<Transform, Rotation>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Rotation>();
            yield return ecsWorld.GetPool<Transform>();
        }

        protected override void Initialize(EcsWorld ecsWorld, EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                var transform = pools.GetComponent<Transform>(entity);
                ref var rotation = ref pools.GetComponent<Rotation>(entity);
                
                rotation.Value = transform.Value.rotation;
            }
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
        }
    }
}