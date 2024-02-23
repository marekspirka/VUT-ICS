using Xunit.Sdk;

namespace ICS_project.Common.Test;

public class EqualObject : AssertActualExpectedException
{
    public EqualObject(object? expected, object? actual, string message)
        : base(expected, actual, "Asser.Equal Fail!!")
    {
        Message = message;
    }

    public override string Message { get; }
}
