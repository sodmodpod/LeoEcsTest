using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Server.Components;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Client.Systems
{
    public class InputToPlayerLinkSystem : BaseEcsFilterCollectionSystem
    {
        protected override IEnumerable<EcsFilter> InitFilters(EcsWorld ecsWorld)
        {
            yield return ecsWorld.Filter<IsPlayer>().End();
            yield return ecsWorld.Filter<IsInput>().End();
        }

        protected override void Execute(EcsFilter[] entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
        }

        protected override void Initialize(EcsFilter[] entities, Dictionary<Type, IEcsPool> pools, IEcsSystems systems, EcsWorld ecsWorld)
        {
            var playerEntities = entities[0];
            var inputEntities = entities[1];

            var inputEntityContainerPool = pools.GetPool<InputEntityContainer>();
            foreach (var playerEntity in playerEntities)
            {
                foreach (var inputEntity in inputEntities)
                {
                    var packedInputEntity = ecsWorld.PackEntity(inputEntity);

                    if (pools.HasComponent<InputEntityContainer>(playerEntity))
                    {
                        inputEntityContainerPool.Add(inputEntity);
                    }
                    
                    ref var inputEntityContainer = ref inputEntityContainerPool.Get(playerEntity);
                    inputEntityContainer.Value = packedInputEntity;
                    
                    return;
                }
            }
        }




        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<IsPlayer>();
            yield return ecsWorld.GetPool<IsInput>();
            yield return ecsWorld.GetPool<InputEntityContainer>();
        }
    }
}