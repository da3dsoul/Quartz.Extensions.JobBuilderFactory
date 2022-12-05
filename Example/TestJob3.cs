using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using QuartzJobFactory;
using QuartzJobFactory.Attributes;

namespace Example;

[JobKeyMember("TestJob3")]
public class TestJob3 : IJob
{
    // Lock only exists here to group log messages during parallel processing
    private static readonly object Lock = new();

    private readonly ISchedulerFactory _schedulerFactory = null!;
    private readonly ILogger<TestJob3> _logger = null!;

    public int SomeID { get; set; }

    [JobKeyMember]
    public bool Force { get; set; }

    [JobKeyMember]
    public bool Cancel { get; set; }

    public TestJob3(ILogger<TestJob3> logger, ISchedulerFactory schedulerFactory)
    {
        _logger = logger;
        _schedulerFactory = schedulerFactory;
    }

    public TestJob3()
    {
    }

    public async Task Execute(IJobExecutionContext context)
    {
        lock (Lock)
        {
            _logger.LogInformation("Job Identity is : {Job} | {Group}", context.JobDetail.Key.Name,
                context.JobDetail.Key.Group);
            _logger.LogInformation("Trigger Identity is : {Trigger} | {Group}", context.Trigger.Key.Name,
                context.Trigger.Key.Group);
            _logger.LogInformation("SomeID is {ID}", SomeID);
            _logger.LogInformation("Force is {Force}", Force);
            _logger.LogInformation("Cancel is {Cancel}", Cancel);
        }

        if (Cancel) Program.Token.Cancel();
        else
            await (await _schedulerFactory.GetScheduler()).ScheduleJob(JobBuilder<TestJob3>.Create().UsingJobData(b =>
                {
                    b.SomeID = 14;
                    b.Force = true;
                    b.Cancel = true;
                }).WithGeneratedIdentity(context.JobDetail.Key.Group).Build(),
                TriggerBuilder.Create().WithIdentity("Test3_Continued", context.JobDetail.Key.Group).StartNow().Build());
    }
}