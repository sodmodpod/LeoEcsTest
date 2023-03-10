using System.Collections.Generic;
using Leopotam.EcsLite;
using Zenject;

namespace UnityTemplateProjects.Installers
{
    public abstract class BaseSystemsContainer
    {
        public abstract SystemContainerType Type { get; }

        public BaseSystemsContainer(IEcsSystem[] systems)
        {
            _systems.AddRange(systems);
        }
        
        public IList<IEcsSystem> Systems => _systems;

        private List<IEcsSystem> _systems = new List<IEcsSystem>();
    }
}