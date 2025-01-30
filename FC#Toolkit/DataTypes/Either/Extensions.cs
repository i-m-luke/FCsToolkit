namespace FCsToolkit.DataTypes.Either;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Extensions
{
    public static TOut Match<TOut, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, TOut> isLeft, Func<TRight, TOut> isRight)
        => either switch
        {
            Left<TLeft, TRight> left => isLeft(left.Value),
            Right<TLeft, TRight> right => isRight(right.Value),
            _ => throw new NotSupportedException()
        };

    public static void MatchEffect<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TLeft> isLeft, Action<TRight> isRight)
    {
        switch (either)
        {
            case Left<TLeft, TRight> left:
                isLeft(left.Value);
                return;

            case Right<TLeft, TRight> right:
                isRight(right.Value);
                return;
        }

        throw new NotSupportedException();
    }
}