using Leopotam.EcsLite;
using System.Collections.Generic;

namespace UnityTemplateProjects.Installers
{
    public class FixedUpdateSystemsContainer : BaseSystemsContainer
    {
        public FixedUpdateSystemsContainer(IEcsSystem[] systems) : base(systems)
        {
        }

        public override SystemContainerType Type => SystemContainerType.FixedUpdate;
    }
}