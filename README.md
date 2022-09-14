# Quartz Job Factory
This is a Quartz extension for JobBuilder to allow factory creation of IJobDetails with generics and compile-time checking of types and parameters.

Detailed usage can be found in the Tests project.

# Usage

```cs
services.AddQuartz(o =>
    {
        o.UseMicrosoftDependencyInjectionJobFactory();
        o.ScheduleJob<TestJob>(trigger => trigger.WithIdentity("Test", "ScheduleJob").StartNow(),
            job => job.UsingJobData(j =>
            {
                j.SomeID = 56;
                j.Force = true;
            }));
    });
```
