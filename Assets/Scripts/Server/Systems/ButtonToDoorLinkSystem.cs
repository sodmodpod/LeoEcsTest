using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using Unity.Mathematics;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class ButtonToDoorLinkSystem : BaseEcsFilterCollectionSystem
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<ButtonId>();
            yield return ecsWorld.GetPool<IsDoor>();
            yield return ecsWorld.GetPool<ButtonEntityContainer>();
        }

        protected override void Initialize(EcsFilter[] entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            var doorEntities = entities[0];
            var buttonEntities = entities[1];
            
            var buttonEntityContainerPool = pools.GetPool<ButtonEntityContainer>();
            foreach (var doorEntity in doorEntities)
            {
                var doorButtonId = pools.GetComponent<ButtonId>(doorEntity);
                
                foreach (var buttonEntity in buttonEntities)
                {
                    var buttonId = pools.GetComponent<ButtonId>(buttonEntity);
                    if (buttonId.Value == doorButtonId.Value)
                    {
                        var packedInputEntity = ecsWorld.PackEntity(buttonEntity);
                        if (!pools.HasComponent<ButtonEntityContainer>(doorEntity))
                        {
                            buttonEntityContainerPool.Add(doorEntity);
                        }
                
                        ref var buttonEntityContainer = ref buttonEntityContainerPool.Get(doorEntity);
                        buttonEntityContainer.Value = packedInputEntity;
                    }
                }
            }
        }

        protected override IEnumerable<EcsFilter> InitFilters(EcsWorld ecsWorld)
        {
            yield return ecsWorld.Filter<IsDoor>().Inc<ButtonId>().End();
            yield return ecsWorld.Filter<IsButton>().Inc<ButtonId>().End();
        }

        protected override void Execute(EcsFilter[] entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
        }
    }
}