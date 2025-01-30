namespace FCsToolkitTests.Examples;

using FCsToolkit.DataTypes.Option;
using FCsToolkit;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using FCsToolkit.DataTypes.Either;

internal class BasicUsageTests
{
    [Test]
    public void UsageExample()
    {
        static Option<int> fn(int x)
            => x switch
            {
                > 0 => Option<int>.Some(x),
                _ => Option<int>.None()
            };

        10.PipeO(fn).MatchEffect(
            some => Debug.WriteLine("Some: " + some),
            () => Debug.WriteLine("None"));

        // Out: Failure!
        0.Try(x => x != 0 ? x / 2 : throw new DivideByZeroException()).MatchEffect(
            x => Debug.WriteLine("Failure!: " + x.StackTrace),
            x => Debug.WriteLine("Success!: " + x));

        // Out: Success!
        2.Try(x => x != 0 ? x / 2 : throw new DivideByZeroException()).MatchEffect(
            x => Debug.WriteLine("Failure!: " + x.StackTrace),
            x => Debug.WriteLine("Success!: " + x));
    }
}