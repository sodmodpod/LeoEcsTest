using System;
using System.Collections.Generic;
using Client.Services;
using Leopotam.EcsLite;
using Server.Components;
using Server.Services;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;
using Unity.Mathematics;

namespace Client.Systems
{
    public class MoveSystem : BaseEcsFilterSystem<DestinationPosition, Position, Velocity, Speed, HasToMove>
    {
        private readonly ITimeService _timeService;
        private float3 xz = new float3(1, 0, 1); 
        public MoveSystem(ITimeService timeService)
        {
            _timeService = timeService;
        }
        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems system, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                ref var position = ref pools.GetComponent<Position>(entity);
                ref var velocity = ref pools.GetComponent<Velocity>(entity);
                var destinationPosition = pools.GetComponent<DestinationPosition>(entity);
                var speed = pools.GetComponent<Speed>(entity);
                var direction = math.normalize((destinationPosition.Value - position.Value) * xz);

                velocity.Value = direction * speed.Value;
                position.Value += velocity.Value * _timeService.DeltaTime;
            }
        }

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Position>();
            yield return ecsWorld.GetPool<DestinationPosition>();
            yield return ecsWorld.GetPool<Speed>();
            yield return ecsWorld.GetPool<Velocity>();
        }
    }
}