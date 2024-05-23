using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCS.DataTypes.Option;

internal abstract class Option<TIn>(TIn? value)
{
    protected TIn? HiddenValue { get; } = value;

    public static Some<TIn> Some(TIn value) => new(value);

    public static None<TIn> None() => new();
}

internal class Some<T>(T value) : Option<T>(value)
{
    public T Value => HiddenValue!;
}

internal class None<T>() : Option<T>(default);