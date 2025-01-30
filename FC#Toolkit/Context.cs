namespace FCsToolkit;

using FCsToolkit.DataTypes;

/// <summary>
/// Struct for processing functions in a given context
/// </summary>
/// <typeparam name="TCtxData">Type of context data.</typeparam>
/// <typeparam name="TIn">Type of input for context.</typeparam>
/// <param name="data">Context data.</param>
/// <param name="input">Input to the context.</param>
public readonly struct Context<TCtxData, TIn>(TCtxData data, TIn input)
    where TCtxData : struct
{
    /// <summary>
    /// A delegate for executing initial context funciton.
    /// </summary>
    /// <typeparam name="TOut">Function output type</typeparam>
    /// <param name="ctxData">Context data</param>
    /// <returns></returns>
    public delegate TOut InitFn<out TOut>(TCtxData ctxData);

    /// <summary>
    /// A delegate for context executed functions.
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="ctxData"></param>
    /// <param name="in"></param>
    /// <returns></returns>
    public delegate TOut CtxFn<out TOut>(TCtxData ctxData, TIn @in);

    private TCtxData Data { get; } = data;

    private TIn Input { get; } = input;

    /// <summary>
    /// Execute function in a context
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="fn"></param>
    /// <returns></returns>
    public Context<TCtxData, TOut> Next<TOut>(CtxFn<TOut> fn)
        => new(Data, fn(Data, Input));

    /// <summary>
    /// Execute final function in a context returning a final result.
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="fn"></param>
    /// <returns></returns>
    public TOut Final<TOut>(CtxFn<TOut> fn) => fn(Data, Input);
}