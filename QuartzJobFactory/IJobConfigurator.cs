using Quartz;

namespace QuartzJobFactory;

public interface IJobConfigurator<T> : IJobConfigurator where T : IJob, new()
{
    /// <summary>
    /// Bind the given <see cref="IJob"/> model to the <see cref="JobDataMap" />.
    /// </summary>
    ///<returns>the updated JobBuilder</returns>
    /// <seealso cref="IJobDetail.JobDataMap" />
    IJobConfiguratorWithData<T> UsingJobData(Action<T> ctor);

    IJobDetail Build();
}