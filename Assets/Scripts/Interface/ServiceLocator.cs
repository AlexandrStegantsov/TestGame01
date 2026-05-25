using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> Services = new();

    public static void Register<T>(T service)
    {
        Services[typeof(T)] = service;
    }

    public static T Get<T>()
    {
        Type type = typeof(T);

        if (Services.TryGetValue(type, out object service))
        {
            return (T)service;
        }

        Debug.LogError($"Service not registered: {type.Name}");

        return default;
    }
}