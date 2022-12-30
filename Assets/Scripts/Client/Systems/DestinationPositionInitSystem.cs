using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class DestinationPositionInitSystem : BaseEcsFilterSystem<DestinationPosition, Position>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<DestinationPosition>();
            yield return ecsWorld.GetPool<Position>();
        }

        protected override void Initialize(EcsWorld ecsWorld, EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                ref var destinationPosition = ref pools.GetComponent<DestinationPosition>(entity);
                var position = pools.GetComponent<Position>(entity);

                destinationPosition.Value = position.Value;
            }
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
        }
    }
}