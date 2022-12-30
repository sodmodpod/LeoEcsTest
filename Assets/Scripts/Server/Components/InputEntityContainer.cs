using System;
using Leopotam.EcsLite;

namespace Server.Components
{
    [Serializable]
    public struct InputEntityContainer
    {
        public EcsPackedEntity Value;
    }
}