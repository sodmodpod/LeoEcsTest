using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace UnityTemplateProjects.Installers
{
    public static class EcsPoolUtils
    {
        public static ref T GetComponent<T>(this Dictionary<Type, IEcsPool> pools, int entity) where T : struct
        {
            return ref ((EcsPool<T>) pools[typeof(T)]).Get(entity);
        }

        public static bool HasComponent<T>(this Dictionary<Type, IEcsPool> pools, int entity) where T : struct
        {
            return ((EcsPool<T>)pools[typeof(T)]).Has(entity);
        }
        
        public static bool HasComponents(this Dictionary<Type, IEcsPool> pools, int entity, params Type[] types)
        {
            foreach (var type in types)
            {
                if (!pools[type].Has(entity))
                {
                    return false;
                }
            }

            return true;
        }

        public static EcsPool<T> GetPool<T>(this Dictionary<Type, IEcsPool> pools) where T : struct
        {
            return (EcsPool<T>) pools[typeof(T)];
        }
    }
}