using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using Unity.Mathematics;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class DoorOpeningSystem : BaseEcsFilterSystem<IsDoor, Rotation, OpenedDoorRotation, State>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Rotation>();
            yield return ecsWorld.GetPool<State>();
        }

        protected override void Initialize(EcsWorld ecsWorld, EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                var rotation = pools.GetComponent<Rotation>(entity);
                ref var openedRotation = ref pools.GetComponent<OpenedDoorRotation>(entity);

                openedRotation.Value = math.mul(rotation.Value, quaternion.RotateY(90));
            }
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                ref var rotation = ref pools.GetComponent<Rotation>(entity);
                var openedRotation = pools.GetComponent<OpenedDoorRotation>(entity);
                var speed = pools.GetComponent<Speed>(entity);
                var closedRotation = math.mul(openedRotation.Value, quaternion.RotateY(-90));
                
                var state = pools.GetComponent<State>(entity);
                
                
                if (state.Value)
                {
                    rotation.Value = math.slerp(rotation.Value, openedRotation.Value, speed.Value);
                }
                else
                {
                    rotation.Value = math.slerp(rotation.Value, closedRotation, speed.Value);
                }
            }
        }
    }
}