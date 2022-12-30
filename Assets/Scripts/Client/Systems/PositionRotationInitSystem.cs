using System;
using System.Collections.Generic;
using Client.Components;
using Leopotam.EcsLite;
using Server.Components;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class PositionRotationInitSystem : BaseEcsFilterSystem<IsPlayer, Transform, Position, Rotation>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<Rotation>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                var transform = pools.GetComponent<Transform>(entity);
                var position = pools.GetComponent<Position>(entity);
                var rotation = pools.GetComponent<Rotation>(entity);
                
                position.Value = transform.Value.position;
                rotation.Value = transform.Value.rotation;
            }
        }
    }
}