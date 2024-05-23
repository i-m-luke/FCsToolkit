// See https://aka.ms/new-console-template for more information
using FCS;
using FCS.DataTypes.Either;
using FCS.DataTypes.Option;

Console.WriteLine("Hello, World!");

Func<int, Option<int>> fn = (x) => x switch
{
    > 0 => Option<int>.Some(x),
    _ => Option<int>.None()
};

10.PipeO(fn).MatchEffect(
    some => Console.WriteLine("Some: " + some),
    () => Console.WriteLine("None"));

// Out: Failure!
0.Try(x => x != 0 ? x / 2 : throw new DivideByZeroException()).MatchEffect<int, Exception, int>(
    x => Console.WriteLine("Failure!: " + x.StackTrace),
    x => Console.WriteLine("Success!: " + x));

// Out: Success!
2.Try(x => x != 0 ? x / 2 : throw new DivideByZeroException()).MatchEffect<int, Exception, int>(
    x => Console.WriteLine("Failure!: " + x.StackTrace),
    x => Console.WriteLine("Success!: " + x));

Console.ReadKey();