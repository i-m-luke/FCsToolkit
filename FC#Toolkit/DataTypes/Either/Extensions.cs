namespace FCsToolkit.DataTypes.Either;

using System;

public static class Extensions
{
    /// <summary>
    /// Helper extension method for resolving <see cref="Either{TLeft, TRight}"/> datatype.
    /// </summary>
    /// <typeparam name="TOut">Type of output result.</typeparam>
    /// <typeparam name="TLeft">Type of left.</typeparam>
    /// <typeparam name="TRight">Type of right.</typeparam>
    /// <param name="either">Resolved either.</param>
    /// <param name="isLeft">Function for left processing.</param>
    /// <param name="isRight">Function for right processing.</param>
    /// <returns>The output result.</returns>
    /// <exception cref="NotSupportedException"></exception>
    public static TOut Match<TOut, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, TOut> isLeft, Func<TRight, TOut> isRight)
        => either switch
        {
            Left<TLeft, TRight> left => isLeft(left.Value),
            Right<TLeft, TRight> right => isRight(right.Value),
            _ => throw new NotSupportedException()
        };

    /// <summary>
    /// IMPURE: Helper extension method for resolving <see cref="Either{TLeft, TRight}"/> datatype without returning a value.
    /// </summary>
    /// <typeparam name="TLeft">Type of left.</typeparam>
    /// <typeparam name="TRight">Type of right.</typeparam>
    /// <param name="either">Resolved either.</param>
    /// <param name="isLeft">Function for left processing.</param>
    /// <param name="isRight">Function for right processing.</param>
    /// <exception cref="NotSupportedException"></exception>
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