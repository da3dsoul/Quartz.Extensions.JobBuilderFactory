using System.Collections.Concurrent;
using System.Reflection;

namespace QuartzJobFactory.Utils;

/// <summary>
/// A static PropertyInfo cache for generic classes, which have static members per generic parameter
/// </summary>
public static class TypePropertyCache
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> propertiesCache = new();
    private static readonly ConcurrentDictionary<(Type Type, string Name), PropertyInfo?> propertyCache = new();

    public static PropertyInfo[] Get(Type type)
    {
        return propertiesCache.GetOrAdd(type, t => t.GetProperties(BindingFlags.Public | BindingFlags.Instance));
    }

    public static bool ContainsKey(Type type)
    {
        return propertiesCache.ContainsKey(type);
    }

    public static PropertyInfo[] GetOrAdd(Type type, Func<Type, PropertyInfo[]> getter)
    {
        return propertiesCache.GetOrAdd(type, getter);
    }

    public static PropertyInfo? Get(Type type, string name)
    {
        return propertyCache.GetOrAdd((type, name),
            t => t.Type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(a => a.Name == name));
    }

    public static bool ContainsKey(Type type, string name)
    {
        return propertyCache.ContainsKey((type, name));
    }

    public static PropertyInfo? GetOrAdd(Type type, string name, Func<(Type Type, string Name), PropertyInfo?> getter)
    {
        return propertyCache.GetOrAdd((type, name), getter);
    }

    public static T? Get<T>((Type Type, string Name) key, object arg) where T : class
    {
        return propertyCache.GetOrAdd((key.Type, key.Name),
            t => t.Type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(a => a.Name == key.Name))?.GetValue(arg) as T;
    }
}