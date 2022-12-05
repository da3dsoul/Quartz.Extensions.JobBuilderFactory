using System.Collections.Concurrent;
using System.Reflection;

namespace QuartzJobFactory.Utils;

/// <summary>
/// A static FieldInfo cache for generic classes, which have static members per generic parameter
/// </summary>
public static class TypeFieldCache
{
    private static readonly ConcurrentDictionary<Type, FieldInfo[]> fieldsCache = new();
    private static readonly ConcurrentDictionary<(Type Type, string Name), FieldInfo?> fieldCache = new();

    public static FieldInfo[] Get(Type type)
    {
        return fieldsCache.GetOrAdd(type, t => t.GetFields(BindingFlags.Public | BindingFlags.Instance));
    }

    public static bool ContainsKey(Type type)
    {
        return fieldsCache.ContainsKey(type);
    }

    public static FieldInfo[] GetOrAdd(Type type, Func<Type, FieldInfo[]> getter)
    {
        return fieldsCache.GetOrAdd(type, getter);
    }

    public static FieldInfo? Get(Type type, string name)
    {
        return fieldCache.GetOrAdd((type, name),
            t => t.Type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
    }

    public static bool ContainsKey(Type type, string name)
    {
        return fieldCache.ContainsKey((type, name));
    }

    public static FieldInfo? GetOrAdd(Type type, string name, Func<(Type Type, string Name), FieldInfo?> getter)
    {
        return fieldCache.GetOrAdd((type, name), getter);
    }

    public static T? Get<T>((Type Type, string Name) key, object arg) where T : class
    {
        return fieldCache.GetOrAdd((key.Type, key.Name),
                t => t.Type.GetField(key.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            ?.GetValue(arg) as T;
    }
}