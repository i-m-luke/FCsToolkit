using System;
using System.Collections.Generic;
using System.Linq;

namespace FCsToolkit.DataTypes.Option;

using System.Text;
using System.Threading.Tasks;

public static class Extensions
{
    public static TOut Match<TIn, TOut>(this Option<TIn> option, Func<TIn, TOut> ifSome, Func<TOut> ifNone)
        => option switch
        {
            Some<TIn> some => ifSome(some.Value),
            None<TIn> => ifNone(),
            _ => throw new NotSupportedException()
        };

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