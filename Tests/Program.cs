using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzJobFactory;

namespace Tests;

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
                        });
                    });
                    o.AddTrigger(b => b.WithIdentity("Test", "AddQuartz").ForJob("Test", "AddQuartz").StartNow());
                    o.ScheduleJob<TestJob>(trigger => trigger.WithIdentity("Test", "ScheduleJob").StartNow(),
                        job => job.UsingJobData(j => j.SomeID = 56));
                });
                services.AddQuartzHostedService(a => a.WaitForJobsToComplete = true);
                services.AddOptions<QuartzOptions>().Configure(o =>
                {
                    o.AddJob<TestJob>(a => a.WithIdentity("Test", "QuartzOptions").UsingJobData(b => b.SomeID = 12));
                    o.AddTrigger(c => c.WithIdentity("Test", "QuartzOptions").ForJob("Test", "QuartzOptions").StartNow());
                });
                services.AddTransient<TestJob>();
            }).Build();
        await host.StartAsync();
        await host.WaitForShutdownAsync(Token.Token);
    }
}