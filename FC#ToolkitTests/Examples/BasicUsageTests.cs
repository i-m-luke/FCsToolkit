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

using System.ComponentModel;

internal class BasicUsageTests
{
    [Test]
    public void UsageExample()
    {
        #region Option datatype example

        static Option<int> OptFn01(int x)
            => x switch
            {
                > 0 => Option<int>.Some(x),
                _ => Option<int>.None()
            };

        10.PipeO(OptFn01).MatchEffect(
            some => Debug.WriteLine("Some: " + some), // OUT: Some: 10
            () => Debug.WriteLine("None"));

        static Option<object> OptFn02(object? obj)
            => obj switch
            {
                not null => Option<object>.Some(obj),
                _ => Option<object>.None()
            };

        var result = (null as object)
            .PipeO(OptFn02)
            .Match(
                some => "Some: " + some,
                () => "None");
        Debug.WriteLine(result); // OUT: None

        #endregion

        #region Try example

        static Func<int, int> DivideCurryFn(int dividend)
            => divisor => divisor != 0
                ? dividend / divisor
                : throw new DivideByZeroException();

        0.Try(DivideCurryFn(10))
            .MatchEffect(
                x => Debug.WriteLine("Failure!: " + x.StackTrace),
                x => Debug.WriteLine("Success!: " + x)); // OUT: Failure!

        2.Try(DivideCurryFn(10))
            .MatchEffect(
                x => Debug.WriteLine("Failure!: " + x.StackTrace),
                x => Debug.WriteLine("Success!: " + x)); // OUT: Success!: 5

        var matchResult = 0.Try(DivideCurryFn(100))
            .Match(
                ex => "Failure!: " + ex,
                x => "Success!: " + x);
        Debug.WriteLine(matchResult); // OUT: Failure

        matchResult = 2.Try(DivideCurryFn(100))
            .Match(
                ex => "Failure!: " + ex,
                x => "Success!: " + x);
        Debug.WriteLine(matchResult); // OUT: Success!: 50

        #endregion

        #region Compose example

        var baseFn = (int x) => x + 10;

        static int ComponentFn01(int x) => x * 2;
        static string ComponentFn2(int x) => x.ToString();

        var composedFn01 = baseFn
            .Compose(ComponentFn01)
            .Compose(ComponentFn2);

        Debug.WriteLine(composedFn01(10)); // OUT: 40

        var composedFn02 = ComposeCalculateThenConvertToStringFn();
        Debug.WriteLine(composedFn02(50)); // OUT: 110

        #endregion
    }

    public static int MultiplyByTwo(int x) => x * 2;

    public static string ConvertToString(int x) => x.ToString();

    public static Func<int, string> ComposeCalculateThenConvertToStringFn()
    {
        var baseFn = (int x) => x + 10;
        return baseFn.Compose(MultiplyByTwo).Compose(ConvertToString);
    }
}