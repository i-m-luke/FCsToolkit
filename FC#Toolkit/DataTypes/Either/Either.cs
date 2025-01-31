namespace FCsToolkit.DataTypes.Either;

/// <summary>
/// <u>Either monad:</u>
/// Represents a value that can be either of two types - typically used for success/failure scenarios.
/// Either contains exactly one value of either left (typically failure) or right (typically success) type.
/// </summary>
/// <typeparam name="TLeft">Failure type.</typeparam>
/// <typeparam name="TRight">Success type.</typeparam>
public class Either<TLeft, TRight>
{
    protected Either(TLeft left) => HiddenLeft = left;

    protected Either(TRight right) => HiddenRight = right;

    protected TLeft? HiddenLeft { get; }

    protected TRight? HiddenRight { get; }

    public static Left<TLeft, TRight> FromLeft(TLeft left) => new(left);

    public static Right<TLeft, TRight> FromRight(TRight right) => new(right);
}

/// <summary>
/// Represents the Left case of an <see cref="Either{TLeft, TRight}"/> monad, typically used for failure scenarios.
/// Contains a value of the left type.
/// </summary>
/// <typeparam name="TLeft">The type of the left (failure) value.</typeparam>
/// <typeparam name="TRight">The type of the right (success) value.</typeparam>
/// <param name="value">The value to store in this Left instance.</param>
public sealed class Left<TLeft, TRight>(TLeft value)
    : Either<TLeft, TRight>(value)
{
    public TLeft Value => HiddenLeft!;
}

/// <summary>
/// Represents the Right case of an <see cref="Either{TLeft, TRight}"/> monad, typically used for success scenarios.
/// Contains a value of the right type.
/// </summary>
/// <typeparam name="TLeft">The type of the left (failure) value.</typeparam>
/// <typeparam name="TRight">The type of the right (success) value.</typeparam>
/// <param name="value">The value to store in this Right instance.</param>
public sealed class Right<TLeft, TRight>(TRight value)
    : Either<TLeft, TRight>(value)
{
    public TRight Value => HiddenRight!;
}