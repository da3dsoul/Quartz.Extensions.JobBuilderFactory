using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Example;

public class LongTestJob : IJob
{
    private readonly ILogger<TestJob> _logger = null!;

    public LongTestJob(ILogger<TestJob> logger)
    {
        _logger = logger;
    }

    protected LongTestJob()
    {
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Job Identity is : {Job} | {Group}", context.JobDetail.Key.Name,
            context.JobDetail.Key.Group);
        _logger.LogInformation("Trigger Identity is : {Trigger} | {Group}", context.Trigger.Key.Name,
            context.Trigger.Key.Group);
        await Task.Delay(10 * 1000);
        _logger.LogInformation("Finished Long Test Job");
    }
}