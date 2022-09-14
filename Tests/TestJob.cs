using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using QuartzJobFactory;

namespace Tests;

public class TestJob : IJob
{
    // Lock only exists here to group log messages during parallel processing
    private static readonly object Lock = new();

    private readonly ISchedulerFactory _schedulerFactory = null!;
    private readonly ILogger<TestJob> _logger = null!;

    public int SomeID { get; set; }
    public bool Force { get; set; }
    
    public bool Cancel { get; set; }

    public TestJob(ILogger<TestJob> logger, ISchedulerFactory schedulerFactory)
    {
        _logger = logger;
        _schedulerFactory = schedulerFactory;
    }

    public TestJob()
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
            await (await _schedulerFactory.GetScheduler()).ScheduleJob(JobBuilder<TestJob>.Create().UsingJobData(b =>
            {
                b.SomeID = 14;
                b.Force = true;
                b.Cancel = true;
            }).WithIdentity("Test2", context.JobDetail.Key.Group).Build(), TriggerBuilder.Create().WithIdentity("Test2", context.JobDetail.Key.Group).StartNow().Build());
    }
}