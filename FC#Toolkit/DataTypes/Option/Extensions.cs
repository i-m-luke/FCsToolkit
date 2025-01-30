using System;
using System.Collections.Generic;
using System.Linq;

namespace FCsToolkit.DataTypes.Option;

using System.Text;
using System.Threading.Tasks;

public static class Extensions
{
    /// <summary>
    /// Helper extension method for resolving <see cref="Option{TIn}"/> datatype.
    /// </summary>
    /// <typeparam name="TIn">Type of input.</typeparam>
    /// <typeparam name="TOut">Type of output result.</typeparam>
    /// <param name="option">Resolved either.</param>
    /// <param name="isSome">Function for some processing.</param>
    /// <param name="isNone">Function for none processing.</param>
    /// <returns>The output result.</returns>
    public static TOut Match<TIn, TOut>(this Option<TIn> option, Func<TIn, TOut> isSome, Func<TOut> isNone)
        => option switch
        {
            Some<TIn> some => isSome(some.Value),
            None<TIn> => isNone(),
            _ => throw new NotSupportedException()
        };

    /// <summary>
    /// IMPURE: Heper extension method for resolving <see cref="Option{TIn}"/> datatype without returning a value.
    /// </summary>
    /// <typeparam name="T">Type of processed value.</typeparam>
    /// <param name="option">Resolved either.</param>
    /// <param name="isSome">Function for some processing.</param>
    /// <param name="isNone">Function for none processing.</param>
    /// <returns>The output result.</returns>
    /// <exception cref="NotSupportedException"></exception>
    public static void MatchEffect<T>(this Option<T> option, Action<T> isSome, Action isNone)
    {
        switch (option)
        {
            case Some<T> some:
                isSome(some.Value);
                return;
            case None<T> _:
                isNone();
                return;
        }

        throw new NotSupportedException();
    }
}