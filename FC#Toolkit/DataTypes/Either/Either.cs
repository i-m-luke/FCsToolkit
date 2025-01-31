using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCsToolkit.DataTypes.Either
{
    /// <summary>
    /// Either monad.
    /// </summary>
    /// <typeparam name="TLeft">Failure type</typeparam>
    /// <typeparam name="TRight">Success type</typeparam>
    public class Either<TLeft, TRight>
    {
        protected Either(TLeft left)
        {
            HiddenLeft = left;
            IsRight = false;
        }

        protected Either(TRight right)
        {
            HiddenRight = right;
            IsRight = true;
        }

        public bool IsRight { get; }

        protected TLeft? HiddenLeft { get; }
        protected TRight? HiddenRight { get; }

        public static Left<TLeft, TRight> FromLeft(TLeft left) => new(left);

        public static Right<TLeft, TRight> FromRight(TRight right) => new(right);
    }

    public sealed class Left<TLeft, TRight>(TLeft value) : Either<TLeft, TRight>(value)
    {
        public TLeft Value => HiddenLeft!;
    }

    public sealed class Right<TLeft, TRight>(TRight value) : Either<TLeft, TRight>(value)
    {
        public TRight Value => HiddenRight!;
    }
}