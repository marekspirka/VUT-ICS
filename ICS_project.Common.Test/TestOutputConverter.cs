using System.Text;
using Xunit.Abstractions;

namespace ICS_project.Common.Test;

public class TestOutputConverter : TextWriter
{
    private readonly ITestOutputHelper _output;

    public TestOutputConverter(ITestOutputHelper output)
    {
        _output = output;
    }
    public override Encoding Encoding => Encoding.UTF8;

    public override void WriteLine (string? message) => _output.WriteLine (message);

    public override void WriteLine (string format, params object?[] args) => _output.WriteLine (format, args);
}
