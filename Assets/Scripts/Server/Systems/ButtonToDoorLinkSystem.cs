using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class ButtonToDoorLinkSystem : BaseEcsFilterCollectionSystem
    {
        private readonly EcsWorld _ecsWorld;

        public ButtonToDoorLinkSystem(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<ButtonId>();
            yield return ecsWorld.GetPool<IsDoor>();
            yield return ecsWorld.GetPool<ButtonEntityContainer>();
            
        }

        protected override IEnumerable<EcsFilter> InitFilters(EcsWorld ecsWorld)
        {
            yield return ecsWorld.Filter<ButtonId>().Inc<IsDoor>().Inc<ButtonEntityContainer>().End();
            yield return ecsWorld.Filter<IsButton>().Inc<ButtonId>().End();
        }

        protected override void Execute(EcsFilter[] entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            var doorEntity = entities[0];
            var buttonEntities = entities[1];

            var inputEntityContainerPool = pools.GetPool<InputEntityContainer>();
            foreach (var playerEntity in doorEntity)
            {
                var doorButtonId = pools.GetComponent<ButtonId>(playerEntity);
                
                foreach (var buttonEntity in buttonEntities)
                {
                    var buttonId = pools.GetComponent<ButtonId>(buttonEntity);
                    if (buttonId.Value == doorButtonId.Value)
                    {
                        var packedInputEntity = _ecsWorld.PackEntity(buttonEntity);
                        if (!pools.HasComponent<ButtonEntityContainer>(playerEntity))
                        {
                            inputEntityContainerPool.Add(playerEntity);
                        }
                
                        ref var buttonEntityContainer = ref inputEntityContainerPool.Get(playerEntity);
                        buttonEntityContainer.Value = packedInputEntity;
                    }
   
                    return;
                }
            }
        }
    }
}