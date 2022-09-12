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
                });
                services.AddQuartzHostedService(a => a.WaitForJobsToComplete = true);
                services.AddOptions<QuartzOptions>().Configure(o =>
                {
                    o.AddJob<TestJob>(a => a.UsingJobData(b => b.SomeID = 12).WithIdentity("Test", "QuartzOptions"));
                    o.AddTrigger(c => c.WithIdentity("Test", "QuartzOptions").ForJob("Test", "QuartzOptions").StartNow());
                });
            }).Build();
        await host.StartAsync();
        await host.WaitForShutdownAsync(Token.Token);
    }
}