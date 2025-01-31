namespace FCsToolkit.DataTypes.Option;

public abstract class Option<TIn>(TIn? value)
{
    protected TIn? HiddenValue { get; } = value;

    public static Some<TIn> Some(TIn value) => new(value);

    public static None<TIn> None() => new();

    public static Option<TInNullable> NotNull<TInNullable>(TInNullable? nullable)
        where TInNullable : class
        => nullable switch
        {
            not null => Option<TInNullable>.Some(nullable),
            _ => Option<TInNullable>.None(),
        };
}

public class Some<T>(T value) : Option<T>(value)
{
    public T Value => HiddenValue!;
}

public class None<T>() : Option<T>(default);