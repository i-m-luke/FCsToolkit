using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCS.DataTypes.Either
{
    internal static class Extensions
    {
        internal static TOut Match<TIn, TOut, TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, TOut> ifLeft, Func<TRight, TOut> ifRight)
            => either switch
            {
                Left<TLeft, TRight> left => ifLeft(left.Value),
                Right<TLeft, TRight> right => ifRight(right.Value),
                _ => throw new NotSupportedException()
            };

        internal static void MatchEffect<TIn, TLeft, TRight>(this Either<TLeft, TRight> either, Action<TLeft> ifLeft, Action<TRight> ifRight)
        {
            switch (either)
            {
                case Left<TLeft, TRight> left:
                    ifLeft(left.Value);
                    return;

                case Right<TLeft, TRight> right:
                    ifRight(right.Value);
                    return;
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Pipe operation resulting in either monad.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        internal static Either<TLeft, TRight> PipeE<TIn, TLeft, TRight>(this TIn input, Func<TIn, Either<TLeft, TRight>> func) => func(input);
    }
}
