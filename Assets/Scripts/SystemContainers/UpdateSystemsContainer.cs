using Leopotam.EcsLite;

namespace UnityTemplateProjects.Installers
{
    public class UpdateSystemsContainer : BaseSystemsContainer
    {
        public UpdateSystemsContainer(IEcsSystem[] systems) : base(systems)
        {
        }

        public override SystemContainerType Type => SystemContainerType.Update;
    }
}