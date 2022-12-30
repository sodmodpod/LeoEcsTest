using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using Unity.Mathematics;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class SetHasToMoveSystem : BaseEcsFilterSystem<DestinationPosition, Position>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<DestinationPosition>();
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<HasToMove>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                var destinationPosition = pools.GetComponent<DestinationPosition>(entity);
                var position = pools.GetComponent<Position>(entity);
                
                if (math.distancesq(destinationPosition.Value,position.Value) > 0.01f)
                {
                    if (!pools.HasComponent<HasToMove>(entity))
                    {
                        pools.GetPool<HasToMove>().Add(entity);
                    }
                }
                else
                {
                    if (pools.HasComponent<HasToMove>(entity))
                    {
                        pools.GetPool<HasToMove>().Del(entity);
                    }
                }
            }
        }
    }
}