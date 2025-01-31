namespace FCsToolkit.DataTypes.Option;

public abstract class Option<TIn>(TIn? value)
{
    protected TIn? HiddenValue { get; } = value;

    public static Some<TIn> Some(TIn value) => new(value);

    public static None<TIn> None() => new();
}

public class Some<T>(T value) : Option<T>(value)
{
    public T Value => HiddenValue!;
}

public class None<T>() : Option<T>(default);