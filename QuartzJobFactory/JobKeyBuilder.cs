using System.Reflection;
using Quartz;
using QuartzJobFactory.Attributes;
using QuartzJobFactory.Utils;

namespace QuartzJobFactory;

public class JobKeyBuilder<T> where T : class, IJob
{
    private JobDataMap _jobDataMap = new JobDataMap();
    private string? _group;
    
    /// <summary>
    /// Create a JobKeyBuilder with which to define a <see cref="JobKey" /> that matches <see cref="IdentityExtensions.WithGeneratedIdentity{T}(string)"/>
    /// </summary>
    /// <returns>a new JobKeyBuilder</returns>
    public static JobKeyBuilder<T> Create()
    {
        return new JobKeyBuilder<T>();
    }

    public JobKeyBuilder<T> WithGroup(string? group)
    {
        _group = group;
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(string key, string? value)
    {
        _jobDataMap.Put(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(string key, int value)
    {
        _jobDataMap.Put(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(string key, long value)
    {
        _jobDataMap.Put(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(string key, float value)
    {
        _jobDataMap.Put(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(string key, double value)
    {
        _jobDataMap.Put(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(string key, bool value)
    {
        _jobDataMap.Put(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(string key, Guid value)
    {
        _jobDataMap.Put(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(string key, char value)
    {
        _jobDataMap.Put(key, value);
        return this;
    }

    /// <summary>
    /// Add all the data from the given <see cref="JobDataMap" /> to the
    /// <see cref="IJobDetail" />'s <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(JobDataMap newJobDataMap)
    {
        _jobDataMap.PutAll(newJobDataMap);
        return this;
    }

    /// <summary>
    /// Bind the given <see cref="IJob"/> model to the <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobKeyBuilder<T> UsingJobData(Action<T> ctor)
    {
        var map = JobDataMapBuilder.FromType(ctor);
        UsingJobData(map);
        return this;
    }
    
    public JobKey Build()
    {
        var type = typeof(T);
        // Get all members with JobKeyMember attribute
        var members = TypePropertyCache.Get(typeof(T))
            .Select(a => (Member: a, Attribute: a.GetCustomAttribute<JobKeyMemberAttribute?>()))
            .Where(a => a.Attribute != null).ToArray();

        if (members.Length == 0)
        {
            members = TypePropertyCache.Get(typeof(T)).Select(a => (Member: a, Attribute: (JobKeyMemberAttribute?)null))
                .ToArray();
        }
        else
        {
            // sort by Index, then order
            // get max of index, falling back on count, and add that to the index of the members (+1) to ensure that ordering is consistent
            var maxIndex = members.Max(a => a.Attribute!.Index);
            if (maxIndex == -1) maxIndex = members.Length;
            members = members.Select((a, index) => (Tuple: a, index))
                .OrderBy(a => a.Tuple.Attribute!.Index >= 0 ? a.Tuple.Attribute.Index : a.index + maxIndex + 1)
                .Select(a => a.Tuple).ToArray();
        }

        // iterate and build JobKey
        var builder = new List<string>();
        var className = type.GetCustomAttribute<JobKeyMemberAttribute>()?.Id ?? type.Name;
        builder.Add(className);
        foreach (var (member, attribute) in members)
        {
            var id = attribute?.Id ?? member.Name;
            if (!_jobDataMap.TryGetValue(member.Name, out var value)) continue;
            builder.Add(id + ":" + value);
        }

        var name = string.Join("_", builder);

        return _group == null ? new JobKey(name) : new JobKey(name, _group);
    }
}