using System;
using System.Collections.Generic;
using Client.Components;
using Client.Services;
using Leopotam.EcsLite;
using Server.Components;
using Unity.Mathematics;
using UnityEngine;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;
using Camera = Client.Components.Camera;

namespace Client.Systems
{
    public class SetInputPositionSystem : BaseEcsFilterSystem<IsInput, Position>
    {
        private readonly IInputService _inputService;

        public SetInputPositionSystem(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            var inputData = _inputService.GetInputData();
            
            foreach (var entity in entities)
            {
                ref var position = ref pools.GetComponent<Position>(entity);
                
                if (inputData.Pressed)
                {
                    position.Value = GetMousePosition(inputData);
                }
            }
        }

        private float3 GetMousePosition(InputData inputData){
            if (UnityEngine.Camera.main != null)
            {
                var ray = UnityEngine.Camera.main.ScreenPointToRay(inputData.Position);
                if (Physics.Raycast(ray, out var hit, LayerMask.GetMask("Move")))
                {
                    return hit.point;
                }
            }

            return float3.zero;
        }

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<Camera>();
            yield return ecsWorld.GetPool<InputLayerMask>();
        }
    }
}