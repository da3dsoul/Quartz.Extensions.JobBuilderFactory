using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzJobFactory.Utils;

namespace QuartzJobFactory;

public static class JobFactoryExtensions
{
    public static IJobDetail AddJob<T>(this QuartzOptions options, Action<IJobConfigurator<T>> configure)
        where T : IJob, new()
    {
        var builder = JobBuilder<T>.Create();
        configure(builder);

        var getJobDetails = new Func<object, List<IJobDetail>?>(t =>
            TypeFieldCache.Get<List<IJobDetail>>((typeof(QuartzOptions), "jobDetails"), t));

        var detail = builder.Build();
        getJobDetails(options)?.Add(detail);

        return detail;
    }

    public static IServiceCollectionQuartzConfigurator AddJob<T>(this IServiceCollectionQuartzConfigurator options,
        JobKey? jobKey = null, Action<IJobConfigurator<T>>? configure = null) where T : IJob, new()
    {
        var builder = JobBuilder<T>.Create();
        if (jobKey != null) builder.WithIdentity(jobKey);

        builder.OfType(typeof(T));
        configure?.Invoke(builder);
        var jobDetail = builder.Build();

        var services =
            TypePropertyCache.Get<IServiceCollection>((typeof(IServiceCollectionQuartzConfigurator), "Services"),
                options);
        var jobDetails = new Func<object, List<IJobDetail>?>(t =>
            TypeFieldCache.Get<List<IJobDetail>>((typeof(QuartzOptions), "jobDetails"), t));

        services?.Configure<QuartzOptions>(x => { jobDetails.Invoke(x)?.Add(jobDetail); });

        return options;
    }

    /// <summary>
    /// Schedule job with trigger to underlying service collection. This API maybe change!
    /// </summary>
    public static IServiceCollectionQuartzConfigurator ScheduleJob<T>(
        this IServiceCollectionQuartzConfigurator options,
        Action<TriggerBuilder> triggerAction,
        Action<IJobConfigurator<T>>? jobAction = null) where T : IJob, new()
    {
        if (triggerAction is null) throw new ArgumentNullException(nameof(triggerAction));

        var builder = JobBuilder<T>.Create();
        jobAction?.Invoke(builder);
        var key = TypeFieldCache.Get<JobKey>((typeof(JobBuilder), "key"), builder);
        var jobHasCustomKey = key is not null;
        var jobDetail = builder.Build();

        var services =
            TypePropertyCache.Get<IServiceCollection>((typeof(IServiceCollectionQuartzConfigurator), "Services"),
                options);
        var jobDetails = new Func<object, List<IJobDetail>?>(t =>
            TypeFieldCache.Get<List<IJobDetail>>((typeof(QuartzOptions), "jobDetails"), t));

        services?.Configure<QuartzOptions>(x => { jobDetails.Invoke(x)?.Add(jobDetail); });

        var triggerConfigurator = TriggerBuilder.Create();
        triggerConfigurator.ForJob(jobDetail);

        triggerAction.Invoke(triggerConfigurator);
        var trigger = triggerConfigurator.Build();

        // The job configurator is optional and omitted in most examples
        // If no job key was specified, have the job key match the trigger key
        if (!jobHasCustomKey)
        {
            ((JobDetailImpl)jobDetail).Key = new JobKey(trigger.Key.Name, trigger.Key.Group);

            // Keep ITrigger.JobKey in sync with IJobDetail.Key
            ((IMutableTrigger)trigger).JobKey = jobDetail.Key;
        }

        if (trigger.JobKey is null || !trigger.JobKey.Equals(jobDetail.Key))
            throw new InvalidOperationException("Trigger doesn't refer to job being scheduled");

        var triggers = new Func<object, List<ITrigger>?>(t =>
            TypeFieldCache.Get<List<ITrigger>>((typeof(QuartzOptions), "triggers"), t));

        services?.Configure<QuartzOptions>(x => { triggers.Invoke(x)?.Add(trigger); });

        return options;
    }
}