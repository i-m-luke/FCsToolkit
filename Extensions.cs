using FCS.DataTypes.Either;
using FCS.DataTypes.Option;

namespace FCS;

public static class Extensions
{
    internal static TOut Pipe<TIn, TOut>(this TIn input, Func<TIn, TOut> func)
        => func(input);    

    internal static Either<Exception, TOut> Try<TIn, TOut>(this TIn value, Func<TIn, TOut> @try)
    {
        try
        {
            return Either<Exception, TOut>.FromRight(@try(value));
        } 
        catch (Exception ex) 
        {
            return Either<Exception, TOut>.FromLeft(ex);
        }

        throw new NotSupportedException();
    }
}
