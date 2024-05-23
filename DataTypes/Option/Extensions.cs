using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCS.DataTypes.Option
{
    internal static class Extensions
    {
        /// <summary>
        /// Pipe operation resulting in option monad.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        internal static Option<TOut> PipeO<TIn, TOut>(this TIn input, Func<TIn, Option<TOut>> func) => func(input);

        internal static TOut Match<TIn, TOut>(this Option<TIn> option, Func<TIn, TOut> ifSome, Func<TOut> ifNone)
            => option switch
            {
                Some<TIn> some => ifSome(some.Value),
                None<TIn> => ifNone(),
                _ => throw new NotSupportedException()
            };

        internal static void MatchEffect<T>(this Option<T> option, Action<T> ifSome, Action ifNone)
        {
            switch (option)
            {
                case Some<T> some:
                    ifSome(some.Value);
                    return;
                case None<T> _:
                    ifNone();
                    return;
            }

            throw new NotSupportedException();
        }
    }
}
