# Quartz Job Factory
This is a Quartz extension for JobBuilder to allow factory creation of IJobDetails with generics and compile-time checking of types and parameters.

Detailed usage can be found in the Example project.

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

There is also a JobKeyBuilder that will allow generating JobKeys unique to the JobDataMap. An example usage and output is below.
```cs
> JsonConvert.SerializeObject(JobKeyBuilder<TestJob2>.Create().UsingJobData(j => j.SomeID = 12).WithGroup("Test").Build())
{ "name": "TestJob2_SomeID:12", "group": "Test" }
```
