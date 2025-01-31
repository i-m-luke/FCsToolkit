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
            .InitContext(ctxData => ctxData.X * 2)
            .Next((ctxData, input) => ctxData.X + input)
            .Next((ctxData, input) => ctxData.X / input)
            .Final((ctxData, input) => ctxData.X - input);
    }
}