using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using QuartzJobFactory;

namespace Tests;

public class TestJob : IJob
{
    private readonly ISchedulerFactory _schedulerFactory = null!;
    private readonly ILogger<TestJob> _logger = null!;

    public int AnimeID { get; set; }
    public bool Force { get; set; }
    
    public bool Cancel { get; set; }

    public TestJob(ILogger<TestJob> logger, ISchedulerFactory schedulerFactory)
    {
        _logger = logger;
        _schedulerFactory = schedulerFactory;
    }

    protected TestJob()
    {
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("AnimeID is {AnimeID}", AnimeID);
        _logger.LogInformation("Force is {Force}", Force);
        _logger.LogInformation("Cancel is {Cancel}", Cancel);
        if (Cancel) Program.Token.Cancel();
        else
            (await _schedulerFactory.GetScheduler()).ScheduleJob(JobBuilder<TestJob>.Create().UsingJobData(b =>
            {
                b.AnimeID = 14;
                b.Force = true;
                b.Cancel = true;
            }).WithIdentity("Test2").Build(), TriggerBuilder.Create().WithIdentity("Test2").StartNow().Build());
    }
}