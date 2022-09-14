using Quartz;
using QuartzJobFactory.Utils;

namespace QuartzJobFactory;

public class JobBuilder<T> : JobBuilder where T : IJob, new()
{
    // I would like a better way to handle these chains ie. not using new, but I don't think it's possible

    /// <summary>
    /// Create a JobBuilder with which to define a <see cref="IJobDetail" />,
    /// and set the class name of the job to be executed.
    /// </summary>
    /// <returns>a new JobBuilder</returns>
    public new static JobBuilder<T> Create()
    {
        var b = new JobBuilder<T>();
        b.OfType(typeof(T));
        return b;
    }

    /// <summary>
    /// Create a JobBuilder with which to define a <see cref="IJobDetail" />,
    /// and set the class name of the job to be executed.
    /// </summary>
    /// <returns>a new JobBuilder</returns>
    [Obsolete]
    private new JobBuilder<T> Create(Type jobType)
    {
        var b = new JobBuilder<T>();
        b.OfType(jobType);
        return b;
    }

    /// <summary>
    /// Create a JobBuilder with which to define a <see cref="IJobDetail" />,
    /// and set the class name of the job to be executed.
    /// </summary>
    /// <returns>a new JobBuilder</returns>
    [Obsolete]
    private new JobBuilder<T1> Create<T1>() where T1 : IJob, new()
    {
        var b = new JobBuilder<T1>();
        b.OfType(typeof(T1));
        return b;
    }


    /// <summary>
    /// Create a JobBuilder with which to define a <see cref="IJobDetail" />,
    /// and set the class name of the job to be executed.
    /// </summary>
    /// <returns>a new JobBuilder</returns>
    [Obsolete]
    private new JobBuilder<T1> CreateForAsync<T1>() where T1 : IJob, new()
    {
        var b = new JobBuilder<T1>();
        b.OfType(typeof(T1));
        return b;
    }

    /*
    /// <summary>
    /// Instructs the <see cref="IScheduler" /> whether or not concurrent execution of the job should be disallowed.
    /// </summary>
    /// <param name="concurrentExecutionDisallowed">Indicates whether or not concurrent execution of the job should be disallowed.</param>
    /// <returns>
    /// The updated <see cref="JobBuilder"/>.
    /// </returns>
    /// <remarks>
    /// If not explicitly set, concurrent execution of a job is only disallowed it either the <see cref="IJobDetail.JobType"/> itself,
    /// one of its ancestors or one of the interfaces that it implements, is annotated with <see cref="DisallowConcurrentExecutionAttribute"/>.
    /// </remarks>
    /// <seealso cref="DisallowConcurrentExecutionAttribute"/>
    public new JobBuilder<T> DisallowConcurrentExecution(bool concurrentExecutionDisallowed = true)
    {
        base.DisallowConcurrentExecution(concurrentExecutionDisallowed);
        return this;
    }

    /// <summary>
    /// Instructs the <see cref="IScheduler" /> whether or not job data should be re-stored when execution of the job completes.
    /// </summary>
    /// <param name="persistJobDataAfterExecution">Indicates whether or not job data should be re-stored when execution of the job completes.</param>
    /// <returns>
    /// The updated <see cref="JobBuilder"/>.
    /// </returns>
    /// <remarks>
    /// If not explicitly set, job data is only re-stored it either the <see cref="IJobDetail.JobType"/> itself, one of
    /// its ancestors or one of the interfaces that it implements, is annotated with <see cref="PersistJobDataAfterExecutionAttribute"/>.
    /// </remarks>
    /// <seealso cref="PersistJobDataAfterExecutionAttribute"/>
    public new JobBuilder<T> PersistJobDataAfterExecution(bool persistJobDataAfterExecution = true)
    {
        base.PersistJobDataAfterExecution(persistJobDataAfterExecution);
        return this;
    }
    */

    /// <summary>
    /// Use a <see cref="JobKey" /> with the given name and default group to
    /// identify the JobDetail.
    /// </summary>
    /// <remarks>
    /// <para>If none of the 'withIdentity' methods are set on the JobBuilder,
    /// then a random, unique JobKey will be generated.</para>
    /// </remarks>
    /// <param name="name">the name element for the Job's JobKey</param>
    /// <returns>the updated JobBuilder</returns>
    /// <seealso cref="JobKey" />
    /// <seealso cref="IJobDetail.Key" />
    public new JobBuilder<T> WithIdentity(string name)
    {
        base.WithIdentity(name);
        return this;
    }

    /// <summary>
    /// Use a <see cref="JobKey" /> with the given name and group to
    /// identify the JobDetail.
    /// </summary>
    /// <remarks>
    /// <para>If none of the 'withIdentity' methods are set on the JobBuilder,
    /// then a random, unique JobKey will be generated.</para>
    /// </remarks>
    /// <param name="name">the name element for the Job's JobKey</param>
    /// <param name="group"> the group element for the Job's JobKey</param>
    /// <returns>the updated JobBuilder</returns>
    /// <seealso cref="JobKey" />
    /// <seealso cref="IJobDetail.Key" />
    public new JobBuilder<T> WithIdentity(string name, string group)
    {
        base.WithIdentity(name, group);
        return this;
    }

    /// <summary>
    /// Use a <see cref="JobKey" /> to identify the JobDetail.
    /// </summary>
    /// <remarks>
    /// <para>If none of the 'withIdentity' methods are set on the JobBuilder,
    /// then a random, unique JobKey will be generated.</para>
    /// </remarks>
    /// <param name="key">the Job's JobKey</param>
    /// <returns>the updated JobBuilder</returns>
    /// <seealso cref="JobKey" />
    /// <seealso cref="IJobDetail.Key" />
    public new JobBuilder<T> WithIdentity(JobKey key)
    {
        base.WithIdentity(key);
        return this;
    }

    /// <summary>
    /// Set the given (human-meaningful) description of the Job.
    /// </summary>
    /// <param name="description"> the description for the Job</param>
    /// <returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.Description" />
    public new JobBuilder<T> WithDescription(string? description)
    {
        base.WithDescription(description);
        return this;
    }

    /*
    /// <summary>
    /// Set the JobType by name
    /// </summary>
    /// <param name="typeName">the Type name</param>
    /// <returns>the updated JobBuilder</returns>
    [Obsolete]
    private new JobBuilder OfType(string typeName)
    {
        base.OfType(typeName);
        return this;
    }
    */

    /// <summary>
    /// Set the class which will be instantiated and executed when a
    /// Trigger fires that is associated with this JobDetail.
    /// </summary>
    /// <returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobType" />
    private new JobBuilder OfType<T1>()
    {
        return OfType(typeof(T1));
    }

    /// <summary>
    /// Set the class which will be instantiated and executed when a
    /// Trigger fires that is associated with this JobDetail.
    /// </summary>
    /// <returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobType" />
    private new JobBuilder OfType(Type type)
    {
        base.OfType(type);
        return this;
    }

    /// <summary>
    /// Instructs the <see cref="IScheduler" /> whether or not the job
    /// should be re-executed if a 'recovery' or 'fail-over' situation is
    /// encountered.
    /// </summary>
    /// <remarks>
    /// If not explicitly set, the default value is <see langword="false" />.
    /// </remarks>
    /// <param name="shouldRecover"></param>
    /// <returns>the updated JobBuilder</returns>
    public new JobBuilder<T> RequestRecovery(bool shouldRecover = true)
    {
        base.RequestRecovery(shouldRecover);
        return this;
    }

    /// <summary>
    /// Whether or not the job should remain stored after it is
    /// orphaned (no <see cref="ITrigger" />s point to it).
    /// </summary>
    /// <remarks>
    /// If not explicitly set, the default value is <see langword="false" />.
    /// </remarks>
    /// <param name="durability">the value to set for the durability property.</param>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.Durable" />
    public new JobBuilder<T> StoreDurably(bool durability = true)
    {
        base.StoreDurably(durability);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new JobBuilder<T> UsingJobData(string key, string? value)
    {
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new JobBuilder<T> UsingJobData(string key, int value)
    {
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new JobBuilder<T> UsingJobData(string key, long value)
    {
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new JobBuilder<T> UsingJobData(string key, float value)
    {
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new JobBuilder<T> UsingJobData(string key, double value)
    {
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new JobBuilder<T> UsingJobData(string key, bool value)
    {
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new JobBuilder<T> UsingJobData(string key, Guid value)
    {
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new JobBuilder<T> UsingJobData(string key, char value)
    {
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add all the data from the given <see cref="JobDataMap" /> to the
    /// <see cref="IJobDetail" />'s <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new JobBuilder<T> UsingJobData(JobDataMap newJobDataMap)
    {
        base.UsingJobData(newJobDataMap);
        return this;
    }

    /// <summary>
    /// Bind the given <see cref="IJob"/> model to the <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public JobBuilder<T> UsingJobData(Action<T> ctor)
    {
        var original = new T();
        var temp = new T();
        ctor.Invoke(temp);

        var map = new JobDataMap();
        var properties = TypePropertyCache.Get(typeof(T));

        foreach (var property in properties)
        {
            var originalValue = property.GetValue(original);
            var newValue = property.GetValue(temp);
            if (!Equals(newValue, originalValue)) map.Put(property.Name, newValue!);
        }

        base.UsingJobData(map);
        return this;
    }
}