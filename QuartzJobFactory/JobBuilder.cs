using System.Reflection;
using Quartz;
using BindingFlags = System.Reflection.BindingFlags;

namespace QuartzJobFactory;

public class JobBuilder<T> : JobBuilder where T : class, IJob
{
    private Func<T> _ctor = null!;
    private PropertyInfo[] _properties = null!;

    /// <summary>
    /// Create a JobBuilder with which to define a <see cref="IJobDetail" />,
    /// and set the class name of the job to be executed.
    /// </summary>
    /// <returns>a new JobBuilder</returns>
    [Obsolete("The generic type of JobBuilder<> allows compile-time helpers via lambdas. This cannot be done if the Job Type isn't a generic parameter.")]
    public new static JobBuilder<T> Create(Type jobType)
    {
        throw new NotSupportedException(
            "The generic type of JobBuilder<> allows compile-time helpers via lambdas. This cannot be done if the Job Type isn't a generic parameter.");
    }

    /// <summary>
    /// Create a JobBuilder with which to define a <see cref="IJobDetail" />,
    /// and set the class name of the job to be executed.
    /// </summary>
    /// <returns>a new JobBuilder</returns>
    public new static JobBuilder<T> Create()
    {
        var ctor = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, Array.Empty<Type>());
        if (ctor == null)
            throw new ArgumentException(
                $"The type {typeof(T).FullName} does not have a parameterless constructor. To avoid problems with Dependency Injection, include a non-public parameterless constructor");

        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var b = new JobBuilder<T>
        {
            _ctor = () => (ctor.Invoke(null) as T)!,
            _properties = props
        };
        b.OfType(typeof(T));
        return b;
    }
    
    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobBuilder<T> UsingJobData(Action<T> ctor)
    {
        var original = _ctor.Invoke();
        var temp = _ctor.Invoke();
        ctor.Invoke(temp);

        var map = new JobDataMap();

        foreach (var property in _properties)
        {
            var originalValue = property.GetValue(original);
            var newValue = property.GetValue(temp);
            if (!Equals(newValue, originalValue)) map.Put(property.Name, newValue!);
        }

        SetJobData(map);
        return this;
    }
}