using System;
using System.Collections.Generic;
using Client.Services;
using Leopotam.EcsLite;
using Server.Components;
using Server.Services;
using Unity.Mathematics;
using UnityEngine;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class DoorOpeningSystem : BaseEcsFilterSystem<IsDoor, Rotation, OpenedDoorRotation, State>
    {
        private readonly ITimeService _timeService;

        public DoorOpeningSystem(ITimeService timeService)
        {
            _timeService = timeService;
        }
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Rotation>();
            yield return ecsWorld.GetPool<State>();
            yield return ecsWorld.GetPool<OpenedDoorRotation>();
            yield return ecsWorld.GetPool<ClosedDoorRotation>();
            yield return ecsWorld.GetPool<Speed>();
        }

        protected override void Initialize(EcsWorld ecsWorld, EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                var rotation = pools.GetComponent<Rotation>(entity);

                ref var openedRotation = ref pools.GetComponent<OpenedDoorRotation>(entity);
                openedRotation.Value = math.mul(quaternion.Euler(0, math.radians(90),0), rotation.Value);
            }
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                ref var rotation = ref pools.GetComponent<Rotation>(entity);
                var openedRotation = pools.GetComponent<OpenedDoorRotation>(entity);
                var speed = pools.GetComponent<Speed>(entity);
                var closedRotation = math.mul(quaternion.Euler(0,math.radians(-90),0), openedRotation.Value);
                var state = pools.GetComponent<State>(entity);
                
                if (state.Value)
                {
                    rotation.Value = math.slerp(rotation.Value, openedRotation.Value, speed.Value * _timeService.DeltaTime);
                }
            }
        }
    }
}