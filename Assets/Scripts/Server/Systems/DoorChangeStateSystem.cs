using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class DoorChangeStateSystem : BaseEcsFilterSystem<IsDoor, ButtonEntityContainer, State>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<ButtonEntityContainer>();
            yield return ecsWorld.GetPool<State>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                ref var state = ref pools.GetComponent<State>(entity);
                var buttonEntityContainer = pools.GetComponent<ButtonEntityContainer>(entity);

                if (buttonEntityContainer.Value.Unpack(ecsWorld, out var buttonEntity))
                {
                    if (pools.HasComponent<State>(buttonEntity))
                    {
                        var buttonState = pools.GetComponent<State>(buttonEntity);
                        state.Value = buttonState.Value;
                    }
                }
            }
        }
    }
}