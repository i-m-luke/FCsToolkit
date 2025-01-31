namespace FCsToolkit;

using FCsToolkit.DataTypes;
using FCsToolkit.DataTypes.Either;
using FCsToolkit.DataTypes.Option;

public static class Extensions
{
    /// <summary>
    /// Basic pipe function that enables function chaining by passing an input value through a transformation function.
    /// </summary>
    /// <typeparam name="TIn">Type of the input value to be transformed.</typeparam>
    /// <typeparam name="TOut">Type of the transformed result.</typeparam>
    /// <param name="input">The input value to be transformed.</param>
    /// <param name="func">The transformation function to be applied to the input.</param>
    /// <returns>The transformed result of type TOut.</returns>
    public static TOut Pipe<TIn, TOut>(
        this TIn input,
        Func<TIn, TOut> func)
        => func(input);

    /// <summary>
    /// Pipe operation that transforms an input value into an <see cref="Option{TOut}"/> monad, enabling safe handling of nullable values.
    /// </summary>
    /// <typeparam name="TIn">Type of the input value.</typeparam>
    /// <typeparam name="TOut">Type of the output value wrapped in Option.</typeparam>
    /// <param name="input">The input value to be transformed.</param>
    /// <param name="func">The transformation function that returns an Option monad.</param>
    /// <returns>An <see cref="Option{TOut}"/> monad containing the transformed value.</returns>
    public static Option<TOut> PipeO<TIn, TOut>(this TIn input, Func<TIn, Option<TOut>> func) => func(input);

    /// <summary>
    /// Pipe operation that transforms an input value into an <see cref="Either{TLeft, TRight}"/> monad, enabling error handling and branching logic.
    /// </summary>
    /// <typeparam name="TIn">Type of the input value.</typeparam>
    /// <typeparam name="TLeft">Type representing the error or left case.</typeparam>
    /// <typeparam name="TRight">Type representing the success or right case.</typeparam>
    /// <param name="input">The input value to be transformed.</param>
    /// <param name="func">The transformation function that returns an Either monad.</param>
    /// <returns>An <see cref="Either{TLeft, TRight}"/> monad containing either an error (Left) or success (Right) value.</returns>
    public static Either<TLeft, TRight> PipeE<TIn, TLeft, TRight>(this TIn input, Func<TIn, Either<TLeft, TRight>> func) => func(input);

    /// <summary>
    /// Safely executes a function within a try-catch block and returns the result as an <see cref="Either{TLeft, TRight}"/> monad.
    /// If an exception occurs, it will be captured in the Left case of the Either.
    /// </summary>
    /// <typeparam name="TIn">Type of the input value.</typeparam>
    /// <typeparam name="TOut">Type of the expected output on successful execution.</typeparam>
    /// <param name="in">The input value to be processed.</param>
    /// <param name="try">The function to be executed safely.</param>
    /// <returns>An <see cref="Either{TLeft, TRight}"/> monad containing either the caught Exception (Left) or the successful result (Right).</returns>
    public static Either<Exception, TOut> Try<TIn, TOut>(
        this TIn @in,
        Func<TIn, TOut> @try)
    {
        try
        {
            return Either<Exception, TOut>.FromRight(@try(@in));
        }
        catch (Exception ex)
        {
            return Either<Exception, TOut>.FromLeft(ex);
        }
    }

    /// <summary>
    /// Initializes a new context with provided data and an initial transformation function.
    /// </summary>
    /// <typeparam name="TCtxData">Type of the context data structure.</typeparam>
    /// <typeparam name="TOut">Type of the transformed context result.</typeparam>
    /// <param name="ctxData">The initial context data structure.</param>
    /// <param name="fn">The initial transformation function to be applied to the context data.</param>
    /// <returns>A new Context instance containing the transformed data.</returns>
    public static Context<TCtxData, TOut> InitContext<TCtxData, TOut>(
        this TCtxData ctxData,
        Context<TCtxData, TOut>.InitFn<TOut> fn)
        where TCtxData : struct
        => new(ctxData, fn(ctxData));

    /// <summary>
    /// Composes two functions into a single function by applying them in sequence.
    /// The output of the first function becomes the input of the second function.
    /// </summary>
    /// <typeparam name="TFn01In">Type of the input for the first function.</typeparam>
    /// <typeparam name="TFn02In">Type of the intermediate result (output of first function, input of second function).</typeparam>
    /// <typeparam name="TOut">Type of the final output result.</typeparam>
    /// <param name="fn01">The first function to be applied.</param>
    /// <param name="fn02">The second function to be applied to the result of the first function.</param>
    /// <returns>A new function that represents the composition of the two input functions.</returns>
    public static Func<TFn01In, TOut> Compose<TFn01In, TFn02In, TOut>(
        this Func<TFn01In, TFn02In> fn01,
        Func<TFn02In, TOut> fn02)
        => @in => fn02(fn01(@in));
}