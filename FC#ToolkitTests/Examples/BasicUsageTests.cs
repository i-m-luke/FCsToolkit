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
        #region Option datatype example

        static Option<int> fn(int x)
            => x switch
            {
                > 0 => Option<int>.Some(x),
                _ => Option<int>.None()
            };

        10.PipeO(fn).MatchEffect(
            some => Debug.WriteLine("Some: " + some),
            () => Debug.WriteLine("None"));

        #endregion

        #region Try example

        static Func<int, int> divideCurryFn(int dividend)
            => divisor => divisor != 0 ? dividend / divisor : throw new DivideByZeroException();

        // Out: Failure!
        0.Try(divideCurryFn(10))
            .MatchEffect(
                x => Debug.WriteLine("Failure!: " + x.StackTrace),
                x => Debug.WriteLine("Success!: " + x));

        // Out: Success!
        2.Try(divideCurryFn(10))
            .MatchEffect(
                x => Debug.WriteLine("Failure!: " + x.StackTrace),
                x => Debug.WriteLine("Success!: " + x));

        // Out: Failure
        var matchResult = 0.Try(divideCurryFn(100))
            .Match(
                ex => "Failure!: " + ex,
                x => "Success!: " + x);
        Debug.WriteLine(matchResult);

        // Out: Success!
        matchResult = 2.Try(divideCurryFn(100))
            .Match(
                ex => "Failure!: " + ex,
                x => "Success!: " + x);
        Debug.WriteLine(matchResult);

        #endregion
    }
}