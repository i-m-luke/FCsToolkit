namespace FCsToolkit;

using FCsToolkit.DataTypes;
using FCsToolkit.DataTypes.Either;
using FCsToolkit.DataTypes.Option;

public static class Extensions
{
    /// <summary>
    /// Basic pipe function used for function chaning.
    /// </summary>
    /// <typeparam name="TIn">Type of processed input.</typeparam>
    /// <typeparam name="TOut">Type of result.</typeparam>
    /// <param name="input">Processed input.</param>
    /// <param name="func">Function for input transformation.</param>
    /// <returns></returns>
    public static TOut Pipe<TIn, TOut>(
        this TIn input,
        Func<TIn, TOut> func)
        => func(input);

    /// <summary>
    /// Pipe operation resulting in <see cref="Option{TIn}"/> monad.
    /// </summary>
    /// <typeparam name="TIn">Type of the input value.</typeparam>
    /// <typeparam name="TOut">Type of the output result</typeparam>
    /// <param name="input">The input.</param>
    /// <param name="func">Function for input transformation.</param>
    /// <returns>Result of <see cref="Option{TOut}"/> datatype</returns>
    public static Option<TOut> PipeO<TIn, TOut>(this TIn input, Func<TIn, Option<TOut>> func) => func(input);

    /// <summary>
    /// Pipe operation resulting in <see cref="Either{TLeft, TRight}"/>  monad.
    /// </summary>
    /// <typeparam name="TIn">Type of the input value.</typeparam>
    /// <typeparam name="TLeft">Type of either left.</typeparam>
    /// <typeparam name="TRight">Type of either right.</typeparam>
    /// <param name="input">The input.</param>
    /// <param name="func">Function for input transformation.</param>
    /// <returns>Result of <see cref="Either{TLeft, TRight}"/> datatype</returns>
    public static Either<TLeft, TRight> PipeE<TIn, TLeft, TRight>(this TIn input, Func<TIn, Either<TLeft, TRight>> func) => func(input);

    /// <summary>
    /// Executes a function in wrapped by try block resulting in <see cref="Either{TLeft, TRight}"/> monad.
    /// </summary>
    /// <typeparam name="TIn">Input type.</typeparam>
    /// <typeparam name="TOut">Output type.</typeparam>
    /// <param name="in">Input.</param>
    /// <param name="try">Function wrapped by try block</param>
    /// <returns>Result of <see cref="Either{TLeft, TRight}"/> datatype</returns>
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
    /// Initializes the context.
    /// </summary>
    /// <typeparam name="TCtxData">Type of context data.</typeparam>
    /// <typeparam name="TOut">Type of output result</typeparam>
    /// <param name="ctxData">Context data.</param>
    /// <param name="fn">Context initial transformation function.</param>
    /// <returns></returns>
    public static Context<TCtxData, TOut> InitContext<TCtxData, TOut>(
        this TCtxData ctxData,
        Context<TCtxData, TOut>.InitFn<TOut> fn) where TCtxData : struct
        => new(ctxData, fn(ctxData));
}