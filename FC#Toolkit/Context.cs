namespace FCsToolkit;

using FCsToolkit.DataTypes;

/// <summary>
/// Struct for processing functions in a given context
/// </summary>
/// <typeparam name="TContextData">Type of context data.</typeparam>
/// <typeparam name="TIn">Type of input for context.</typeparam>
/// <param name="data">Context data.</param>
/// <param name="input">Input to the context.</param>
public readonly struct Context<TContextData, TIn>(TContextData data, TIn input)
    where TContextData : struct
{
    /// <summary>
    /// A delegate for executing initial context funciton.
    /// </summary>
    /// <typeparam name="TOut">Function output type</typeparam>
    /// <param name="ctxData">Context data</param>
    /// <returns></returns>
    public delegate TOut InitFn<out TOut>(TContextData ctxData);

    /// <summary>
    /// A delegate for context executed functions.
    /// </summary>
    /// <typeparam name="TOut">Function output type</typeparam>
    /// <param name="ctxData">Context data</param>
    /// <param name="in">Input value.</param>
    /// <returns></returns>
    public delegate TOut ContextFn<out TOut>(TContextData ctxData, TIn @in);

    /// <summary>
    /// Gets the context data.
    /// </summary>
    private TContextData Data { get; } = data;

    /// <summary>
    /// Gets an input for the next function call.
    /// </summary>
    private TIn Input { get; } = input;

    /// <summary>
    /// Executes function in a context enabling function chaning in the context.
    /// </summary>
    /// <typeparam name="TOut">Type of output result.</typeparam>
    /// <param name="fn">Function for input transformation.</param>
    /// <returns></returns>
    public Context<TContextData, TOut> Next<TOut>(ContextFn<TOut> fn)
        => new(Data, fn(Data, Input));

    /// <summary>
    /// Executes final function in a context returning a final result.
    /// </summary>
    /// <typeparam name="TOut">Type of output result.</typeparam>
    /// <param name="fn">Function for input transformation.</param>
    /// <returns></returns>
    public TOut Final<TOut>(ContextFn<TOut> fn) => fn(Data, Input);
}