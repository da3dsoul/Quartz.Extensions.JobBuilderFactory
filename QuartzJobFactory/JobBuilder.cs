using System.Reflection;
using Quartz;
using QuartzJobFactory.Attributes;

namespace QuartzJobFactory;

public class JobBuilder<T> : JobBuilder, IJobConfiguratorWithData<T> where T : IJob, new()
{
    private JobDataMap _jobDataMap = new JobDataMap();

    /// <summary>
    /// Create a JobBuilder with which to define a <see cref="IJobDetail" />,
    /// and set the class name of the job to be executed.
    /// </summary>
    /// <returns>a new JobBuilder</returns>
    public new static IJobConfigurator<T> Create()
    {
        var b = new JobBuilder<T>();
        b.OfType(typeof(T));
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
    /// Generate a <see cref="JobKey" /> to identify the JobDetail from the set JobDataMap using <see cref="JobKeyMemberAttribute"/> on members.
    /// If none are marked, then all public properties will be considered, in the default order, with the member names.
    /// Only non-default values will be added to the <see cref="JobKey"/>
    /// </summary>
    /// <remarks>
    /// <para>If none of the 'withIdentity' methods are set on the JobBuilder,
    /// then a random, unique JobKey will be generated.</para>
    /// </remarks>
    /// <returns>the updated JobBuilder</returns>
    /// <seealso cref="JobKey" />
    public IJobConfiguratorWithData<T> WithGeneratedIdentity(string? group = null)
    {
        var type = typeof(T);
        var groupName = group ?? type.GetCustomAttribute<JobKeyGroupAttribute>()?.GroupName;

        var key = JobKeyBuilder<T>.Create().WithGroup(groupName).UsingJobData(_jobDataMap).Build();
        return WithIdentity(key);
    }

    /// <summary>
    /// Set the given (human-meaningful) description of the Job.
    /// </summary>
    /// <param name="description"> the description for the Job</param>
    /// <returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.Description" />
    public new IJobConfigurator<T> WithDescription(string? description)
    {
        base.WithDescription(description);
        return this;
    }

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
    public new IJobConfigurator<T> RequestRecovery(bool shouldRecover = true)
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
    public new IJobConfigurator<T> StoreDurably(bool durability = true)
    {
        base.StoreDurably(durability);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new IJobConfiguratorWithData<T> UsingJobData(string key, string? value)
    {
        _jobDataMap.Put(key, value);
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new IJobConfiguratorWithData<T> UsingJobData(string key, int value)
    {
        _jobDataMap.Put(key, value);
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new IJobConfiguratorWithData<T> UsingJobData(string key, long value)
    {
        _jobDataMap.Put(key, value);
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new IJobConfiguratorWithData<T> UsingJobData(string key, float value)
    {
        _jobDataMap.Put(key, value);
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new IJobConfiguratorWithData<T> UsingJobData(string key, double value)
    {
        _jobDataMap.Put(key, value);
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new IJobConfiguratorWithData<T> UsingJobData(string key, bool value)
    {
        _jobDataMap.Put(key, value);
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new IJobConfiguratorWithData<T> UsingJobData(string key, Guid value)
    {
        _jobDataMap.Put(key, value);
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new IJobConfiguratorWithData<T> UsingJobData(string key, char value)
    {
        _jobDataMap.Put(key, value);
        base.UsingJobData(key, value);
        return this;
    }

    /// <summary>
    /// Add all the data from the given <see cref="JobDataMap" /> to the
    /// <see cref="IJobDetail" />'s <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public new IJobConfiguratorWithData<T> UsingJobData(JobDataMap newJobDataMap)
    {
        _jobDataMap.PutAll(newJobDataMap);
        base.UsingJobData(newJobDataMap);
        return this;
    }

    /// <summary>
    /// Bind the given <see cref="IJob"/> model to the <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public IJobConfiguratorWithData<T> UsingJobData(Action<T> ctor)
    {
        var map = JobDataMapBuilder.FromType(ctor);
        UsingJobData(map);
        return this;
    }
}

public class JobBuilder : Quartz.JobBuilder
{
    /// <summary>
    /// Create a JobBuilder with which to define a <see cref="IJobDetail" />,
    /// and set the class name of the job to be executed.
    /// </summary>
    /// <returns>a new JobBuilder</returns>
    [Obsolete("Use the Generic JobBuilder to allow new features")]
    private new JobBuilder Create(Type jobType)
    {
        throw new NotSupportedException("Use the Generic JobBuilder to allow new features");
    }

    /// <summary>
    /// Create a JobBuilder with which to define a <see cref="IJobDetail" />,
    /// and set the class name of the job to be executed.
    /// </summary>
    /// <returns>a new JobBuilder</returns>
    private new JobBuilder<T> Create<T>() where T : IJob, new()
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
    private new JobBuilder<T> CreateForAsync<T>() where T : IJob, new()
    {
        var b = new JobBuilder<T>();
        b.OfType(typeof(T));
        return b;
    }
}