using Leopotam.EcsLite;

namespace UnityTemplateProjects.Installers
{
    public class LateUpdateSystemsContainer : BaseSystemsContainer
    {
        public LateUpdateSystemsContainer(IEcsSystem[] systems) : base(systems)
        {
        }

        public override SystemContainerType Type => SystemContainerType.LateUpdate;
    }
}