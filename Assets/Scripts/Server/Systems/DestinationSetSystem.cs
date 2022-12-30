using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using UnityEngine;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class DestinationSetSystem : BaseEcsFilterSystem<IsPlayer, DestinationPosition, InputEntityContainer>
    {
        private readonly EcsWorld _ecsWorld;

        public DestinationSetSystem(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }


        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<DestinationPosition>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                ref var destinationPosition = ref pools.GetComponent<DestinationPosition>(entity);
                var inputEntityContainer = pools.GetComponent<InputEntityContainer>(entity);


                if(inputEntityContainer.Value.Unpack(_ecsWorld, out var inputEntity))
                {
                    if (pools.HasComponent<Position>(inputEntity))
                    {
                        var inputPosition = Pools.GetComponent<Position>(inputEntity);
                        destinationPosition.Value = inputPosition.Value;
                    }
                    else
                    {
                        Debug.LogError($"Input {inputEntity} has no Position component");
                    }
                }
                else
                {
                    Debug.LogError($"Player {entity} has no InputEntity component");
                }
            }
        }
    }
}