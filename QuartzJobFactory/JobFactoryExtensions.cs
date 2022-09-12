using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace QuartzJobFactory;

public static class JobFactoryExtensions
{
    private static FieldInfo? _quartzOptionsJobDetails;
    public static QuartzOptions AddJob<T>(this QuartzOptions options, Action<JobBuilder<T>> configure) where T : class, IJob
    {
        var builder = JobBuilder<T>.Create();
        configure(builder);

        if (_quartzOptionsJobDetails == null)
        {
            _quartzOptionsJobDetails = typeof(QuartzOptions).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(a => a.Name == "jobDetails");
        }

        if (_quartzOptionsJobDetails != null)
            (_quartzOptionsJobDetails.GetValue(options) as List<IJobDetail>)!.Add(builder.Build());

        return options;
    }

    private static PropertyInfo? _iServiceCollectionQuartzConfiguratorServices;
    public static IServiceCollectionQuartzConfigurator AddJob<T>(this IServiceCollectionQuartzConfigurator options, JobKey? jobKey = null, Action<JobBuilder<T>>? configure = null) where T : class, IJob
    {
        var builder = JobBuilder<T>.Create();
        if (jobKey != null)
        {
            builder.WithIdentity(jobKey);
        }

        builder.OfType(typeof(T));
        configure?.Invoke(builder);
        var jobDetail = builder.Build();

        if (_iServiceCollectionQuartzConfiguratorServices == null)
        {
            _iServiceCollectionQuartzConfiguratorServices = typeof(IServiceCollectionQuartzConfigurator).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(a => a.Name == "Services");
        }
        
        if (_quartzOptionsJobDetails == null)
        {
            _quartzOptionsJobDetails = typeof(QuartzOptions).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(a => a.Name == "jobDetails");
        }

        if (_iServiceCollectionQuartzConfiguratorServices != null && _quartzOptionsJobDetails != null)
        {
            (_iServiceCollectionQuartzConfiguratorServices.GetValue(options) as IServiceCollection)!.Configure<QuartzOptions>(x =>
            {
                (_quartzOptionsJobDetails.GetValue(x) as List<IJobDetail>)!.Add(jobDetail);
            });
        }

        return options;
    }
}