using System;
using System.Collections.Generic;
using Client.Services;
using Leopotam.EcsLite;
using Server.Components;
using Server.Services;
using Server.Services.Data;
using Unity.Mathematics;
using UnityEngine;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class SetDestinationSystem : BaseEcsFilterSystem<DestinationPosition>
    {
        private readonly IInputService _inputService;

        public SetDestinationSystem(IInputService inputService)
        {
            _inputService = inputService;
        }
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<DestinationPosition>();
            yield return ecsWorld.GetPool<InputEntityContainer>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                var inputData = _inputService.GetInputData();

                if (!inputData.Pressed)
                {
                    continue;
                }
                
                ref var destinationPosition = ref pools.GetComponent<DestinationPosition>(entity);

                destinationPosition.Value = GetMousePosition(inputData);
            }
        }
        
        private float3 GetMousePosition(InputData inputData){
            if (UnityEngine.Camera.main != null)
            {
                var ray = UnityEngine.Camera.main.ScreenPointToRay(inputData.Position);
                if (Physics.Raycast(ray, out var hit, LayerMask.GetMask("Ground")))
                {
                    return hit.point;
                }
            }

            return float3.zero;
        }
    }
}