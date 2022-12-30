using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;
using Unity.Mathematics;

namespace Client.Systems
{
    public class MoveSystem : BaseEcsFilterSystem<DestinationPosition, Position, Speed>
    {
        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems system, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                ref var position = ref pools.GetComponent<Position>(entity);
                var destinationPosition = pools.GetComponent<Position>(entity);
                var speed = pools.GetComponent<Speed>(entity);

                var direction = math.normalize((destinationPosition.Value - position.Value));
                position.Value += direction * speed.Value;
            }
        }

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<DestinationPosition>();
            yield return ecsWorld.GetPool<Speed>();
        }
    }
}