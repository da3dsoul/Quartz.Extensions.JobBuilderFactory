using Quartz;

namespace QuartzJobFactory;

public static class UsingJobDataExtensions
{
    /// <summary>
    /// Bind the given <see cref="IJob"/> model to the <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, Action<T> ctor) where T : IJob, new()
    {
        var map = JobDataMapBuilder.FromType(ctor);
        jobConfigurator.GetJobData().PutAll(map);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, int value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, long value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, bool value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, string value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, char value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, DateTime value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, DateTimeOffset value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, TimeSpan value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, double value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, float value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, byte value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, short value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, ushort value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, decimal value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithData<T> UsingJobData<T>(this IJobConfigurator<T> jobConfigurator, string key, Guid value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithData<T>)jobConfigurator;
    }

    /// <summary>
    /// Bind the given <see cref="IJob"/> model to the <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, Action<T> ctor) where T : IJob, new()
    {
        var map = JobDataMapBuilder.FromType(ctor);
        jobConfigurator.GetJobData().PutAll(map);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, int value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, long value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, bool value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, string value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, char value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, DateTime value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, DateTimeOffset value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, TimeSpan value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, double value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, float value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, byte value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, short value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, ushort value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, decimal value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithIdentity<T> jobConfigurator, string key, Guid value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Bind the given <see cref="IJob"/> model to the <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, Action<T> ctor) where T : IJob, new()
    {
        var map = JobDataMapBuilder.FromType(ctor);
        jobConfigurator.GetJobData().PutAll(map);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, int value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, long value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, bool value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, string value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, char value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, DateTime value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, DateTimeOffset value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, TimeSpan value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, double value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, float value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, byte value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, short value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, ushort value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, decimal value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }

    /// <summary>
    /// Add the given key-value pair to the JobDetail's <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    [Obsolete("WithGeneratedIdentity was used before UsingJobData. This will cause the JobKey to be missing data")]
    public static IJobConfiguratorWithDataAndIdentity<T> UsingJobData<T>(this IJobConfiguratorWithGeneratedIdentity<T> jobConfigurator, string key, Guid value) where T : IJob, new()
    {
        jobConfigurator.GetJobData().Put(key, value);
        return (IJobConfiguratorWithDataAndIdentity<T>)jobConfigurator;
    }
}
