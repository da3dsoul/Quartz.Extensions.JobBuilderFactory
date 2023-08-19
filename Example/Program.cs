using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzJobFactory;

namespace Example;

public static class Program
{
    public static CancellationTokenSource Token { get; set; } = new();

    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(Array.Empty<string>())
            .ConfigureServices(services =>
            {
                services.AddQuartz(o =>
                {
                    o.UseMicrosoftDependencyInjectionJobFactory();
                    o.AddJob<TestJob>(new JobKey("Test", "AddQuartz"), builder =>
                    {
                        builder.UsingJobData(a =>
                        {
                            a.SomeID = 43;
                            a.Force = true;
                        }).WithDefaultIdentity().Build();
                    });
                    o.AddTrigger(b => b.WithIdentity("Test", "AddQuartz").ForJob("Test", "AddQuartz").StartNow());
                    o.ScheduleJob<TestJob>(trigger => trigger.WithIdentity("Test", "ScheduleJob").StartNow().Build(),
                        job => job.UsingJobData(j => j.SomeID = 56));
                });
                services.AddQuartzHostedService(a => a.WaitForJobsToComplete = true);
                services.AddOptions<QuartzOptions>().Configure(o =>
                {
                    var detail = o.AddJob<TestJob>(a => a.UsingJobData(b => b.SomeID = 12).WithIdentity("Test", "QuartzOptions").Build());
                    o.AddTrigger(c => c.WithIdentity("Test", "QuartzOptions").ForJob(detail.Key).StartNow().Build());
                    var detail2 = o.AddJob<TestJob2>(a => a.UsingJobData(b => b.SomeID = 12).WithGeneratedIdentity("QuartzOptions").Build());
                    o.AddTrigger(c => c.WithIdentity("Test2", "QuartzOptions").ForJob(detail2.Key).StartNow().Build());
                    var detail3 = o.AddJob<TestJob3>(a => a.UsingJobData(b => b.SomeID = 12).WithGeneratedIdentity("QuartzOptions").Build());
                    o.AddTrigger(c => c.WithIdentity("Test3", "QuartzOptions").ForJob(detail3.Key).StartNow().Build());
                });
                services.AddTransient<TestJob>();
                services.AddTransient<TestJob2>();
                services.AddTransient<TestJob3>();
            }).Build();
        await host.StartAsync();

        var services = host.Services;
        var scheduler = await services.GetRequiredService<ISchedulerFactory>().GetScheduler();
        await scheduler.StartJob(JobBuilder<TestJob2>.Create().UsingJobData(a => a.SomeID = 24).WithGeneratedIdentity("With24")
            .Build());
        
        await scheduler.StartJob(JobBuilder<TestJob2>.Create().WithGeneratedIdentity("Empty")
            .Build());

        await scheduler.StartJob(JobBuilder<LongTestJob>.Create().DisallowConcurrentExecution().WithIdentity("LongTest", "LongTest").Build());
        await Task.Delay(1000);
        await scheduler.StartJob(JobBuilder<LongTestJob>.Create().DisallowConcurrentExecution().WithIdentity("LongTest", "LongTest").Build());
        await Task.Delay(9000);
        await scheduler.StartJob(JobBuilder<LongTestJob>.Create().DisallowConcurrentExecution().WithIdentity("LongTest1", "LongTest").Build());

        await host.WaitForShutdownAsync(Token.Token);
    }
}