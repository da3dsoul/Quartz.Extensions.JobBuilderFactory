using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzJobFactory.Utils;

namespace QuartzJobFactory;

public static class JobFactoryExtensions
{
    public static IJobDetail AddJob<T>(this QuartzOptions options, Action<IJobConfigurator<T>> configure)
        where T : class, IJob
    {
        var builder = JobBuilder<T>.Create();
        configure(builder);

        var getJobDetails = new Func<QuartzOptions, List<IJobDetail>?>(t =>
            TypeFieldCache.Get<List<IJobDetail>>("jobDetails", t));

        var detail = builder.WithDefaultIdentity().Build();
        getJobDetails(options)?.Add(detail);

        return detail;
    }

    public static IServiceCollectionQuartzConfigurator AddJob<T>(this IServiceCollectionQuartzConfigurator options,
        JobKey? jobKey = null, Action<IJobConfigurator<T>>? configure = null) where T : class, IJob
    {
        var builder = JobBuilder<T>.Create();
        if (jobKey != null) builder.WithIdentity(jobKey);

        configure?.Invoke(builder);
        var jobDetail = builder.WithDefaultIdentity().Build();

        var services = TypePropertyCache.Get<IServiceCollection>("Services", options);
        var jobDetails =
            new Func<QuartzOptions, List<IJobDetail>?>(t => TypeFieldCache.Get<List<IJobDetail>>("jobDetails", t));

        services?.Configure<QuartzOptions>(x => { jobDetails.Invoke(x)?.Add(jobDetail); });

        return options;
    }

    /// <summary>
    /// Schedule job with trigger to underlying service collection. This API maybe change!
    /// </summary>
    public static IServiceCollectionQuartzConfigurator ScheduleJob<T>(
        this IServiceCollectionQuartzConfigurator options,
        Action<TriggerBuilder> triggerAction,
        Action<IJobConfigurator<T>>? jobAction = null) where T : class, IJob
    {
        if (triggerAction is null) throw new ArgumentNullException(nameof(triggerAction));

        var builder = JobBuilder<T>.Create();
        jobAction?.Invoke(builder);
        var key = TypeFieldCache.Get<JobKey>("_key", builder);
        var jobHasCustomKey = key is not null;
        var jobDetail = builder.WithDefaultIdentity().Build();

        var services = TypePropertyCache.Get<IServiceCollection>("Services", options);
        var jobDetails =
            new Func<QuartzOptions, List<IJobDetail>?>(t => TypeFieldCache.Get<List<IJobDetail>>("jobDetails", t));

        services?.Configure<QuartzOptions>(x => { jobDetails.Invoke(x)?.Add(jobDetail); });

        var triggerConfigurator = TriggerBuilder.Create();
        triggerConfigurator.ForJob(jobDetail);

        triggerAction.Invoke(triggerConfigurator);
        var trigger = triggerConfigurator.Build();

        // The job configurator is optional and omitted in most examples
        // If no job key was specified, have the job key match the trigger key
        if (!jobHasCustomKey)
        {
            var keyProp = TypePropertyCache.Get(typeof(JobDetailImpl), "Key");
            keyProp?.SetValue((JobDetailImpl)jobDetail, new JobKey(trigger.Key.Name, trigger.Key.Group));

            // Keep ITrigger.JobKey in sync with IJobDetail.Key
            ((IMutableTrigger)trigger).JobKey = jobDetail.Key;
        }

        if (trigger.JobKey is null || !trigger.JobKey.Equals(jobDetail.Key))
            throw new InvalidOperationException("Trigger doesn't refer to job being scheduled");

        var triggers = new Func<QuartzOptions, List<ITrigger>?>(t => TypeFieldCache.Get<List<ITrigger>>("triggers", t));

        services?.Configure<QuartzOptions>(x => { triggers.Invoke(x)?.Add(trigger); });

        return options;
    }

    /// <summary>
    /// Start a job with TriggerBuilder.<see cref="TriggerBuilder.StartNow()"/> on the given scheduler
    /// </summary>
    /// <param name="scheduler">The scheduler to schedule the job with</param>
    /// <param name="job">The job to schedule</param>
    /// <param name="priority">It will go in order by start time, then choose the higher priority. <seealso cref="TriggerBuilder.WithPriority(int)"/></param>
    /// <param name="replaceExisting">Replace the queued trigger if it's still waiting to execute. Default false</param>
    /// <param name="token">The cancellation token</param>
    /// <returns></returns>
    public static async Task<DateTimeOffset> StartJob(this IScheduler scheduler, IJobDetail job, int priority = 0, bool replaceExisting = false, CancellationToken token = default)
    {
        // if it's running, then ignore
        var currentJobs = await scheduler.GetCurrentlyExecutingJobs(token);
        if (currentJobs.Any(a => Equals(a.JobDetail.Key, job.Key))) return DateTimeOffset.Now;

        var triggerBuilder = TriggerBuilder.Create().StartNow();
        if (priority != 0) triggerBuilder = triggerBuilder.WithPriority(priority);

        if (await scheduler.CheckExists(job.Key, token))
        {
            // get waiting triggers
            var triggers = (await scheduler.GetTriggersOfJob(job.Key, token)).Select(a => a.GetNextFireTimeUtc())
                .Where(a => a != null).Select(a => a!.Value).ToList();

            // we are not set to replace the job, then return the first scheduled time
            if (triggers.Any() && !replaceExisting) return triggers.Min();

            // since we are replacing it, it will remove the triggers, as well
            await scheduler.DeleteJob(job.Key, token);
        }

        return await scheduler.ScheduleJob(job, triggerBuilder.Build(), token);
    }
}