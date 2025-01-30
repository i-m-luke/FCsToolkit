namespace FCsToolkitTests.Examples;

using FCsToolkit;

using NUnit.Framework;

internal class ContextUsageTests
{
    public readonly struct CtxData(int x)
    {
        public int X { get; } = x;
    }

    [Test]
    public void UsageExample()
    {
        var result = new CtxData(10)
            .InitContext(data => data.X * 2)
            .Next((data, input) => data.X + input)
            .Final((data, input) => data.X - input);
    }
}