using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using Unity.Mathematics;
using UnityEngine;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class ButtonChangeStateSystem : BaseEcsFilterCollectionSystem
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<State>();
            yield return ecsWorld.GetPool<Radius>();
        }

        protected override IEnumerable<EcsFilter> InitFilters(EcsWorld ecsWorld)
        {
            yield return ecsWorld.Filter<IsButton>().Inc<State>().Inc<Position>().Inc<Radius>().End();
            yield return ecsWorld.Filter<IsInteracting>().Inc<Position>().End();
        }

        protected override void Execute(EcsFilter[] entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            var buttonEntities = entities[0];
            var interactingEntities = entities[1];
            
            foreach (var buttonEntity in buttonEntities)
            {
                var buttonPosition = pools.GetComponent<Position>(buttonEntity);
                var buttonRadius = pools.GetComponent<Radius>(buttonEntity);
                ref var buttonState = ref pools.GetComponent<State>(buttonEntity);
                
                foreach (var interactingEntity in interactingEntities)
                {
                    var interactingPosition = pools.GetComponent<Position>(interactingEntity);
                    buttonState.Value = CheckDistance(buttonPosition.Value, interactingPosition.Value, buttonRadius.Value);
                }
            }
        }

        private bool CheckDistance(float3 buttonPosition, float3 interactingPosition, float radius)
        {
            return math.distance(buttonPosition, interactingPosition) < radius;
        }
    }
}