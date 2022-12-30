using System;
using System.Collections.Generic;
using Client.Components;
using Leopotam.EcsLite;
using Server.Components;
using Unity.Mathematics;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class AnimatorSystem : BaseEcsFilterSystem<Animator, Velocity>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<Animator>();
            yield return ecsWorld.GetPool<Velocity>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            foreach (var entity in entities)
            {
                var velocity = pools.GetComponent<Velocity>(entity);
                var animator = pools.GetComponent<Animator>(entity);
                
                animator.Value.SetFloat("Speed", math.length(velocity.Value));
            }
        }
    }
}