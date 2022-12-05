using Quartz;
using QuartzJobFactory.Attributes;

namespace QuartzJobFactory;

public interface IJobConfiguratorWithData<T> : IJobConfigurator<T> where T : IJob, new()
{
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
    IJobConfiguratorWithData<T> WithGeneratedIdentity(string? group = null);
}