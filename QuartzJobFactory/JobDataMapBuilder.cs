using System.Reflection;
using Quartz;
using QuartzJobFactory.Utils;

namespace QuartzJobFactory;

public static class JobDataMapBuilder
{
    public static JobDataMap FromType<T>(Action<T> ctor) where T : class, IJob
    {
        var constructor = TypeConstructorCache.Get(typeof(T));
        var original = constructor.Invoke(null) as T;
        var temp = constructor.Invoke(null) as T;
        ctor.Invoke(temp);

        var map = new JobDataMap();
        var properties = TypePropertyCache.Get(typeof(T));

        foreach (var property in properties)
        {
            var originalValue = property.GetValue(original);
            var newValue = property.GetValue(temp);
            if (!Equals(newValue, originalValue)) map.Put(property.Name, newValue!);
        }

        return map;
    }
}