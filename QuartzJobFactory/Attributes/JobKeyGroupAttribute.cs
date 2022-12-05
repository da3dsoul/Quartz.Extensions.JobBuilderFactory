﻿namespace QuartzJobFactory.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class JobKeyGroupAttribute : Attribute
{
    public JobKeyGroupAttribute(string groupName)
    {
        GroupName = groupName;
    }

    public string GroupName { get; set; }
}