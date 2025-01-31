namespace FCsToolkitTests;

using System;

using NUnit.Framework;

using FCsToolkit;
using FCsToolkit.DataTypes.Either;
using FCsToolkit.DataTypes.Option;

public class ExtensionsTests
{
    [Test]
    public void Pipe_WhenTransformingInput_ThenReturnsTransformedValue()
    {
        // Arrange
        static string ConvertToString(int x) => x.ToString();

        const int input = 5;

        // Act
        var result = input.Pipe(ConvertToString);

        // Assert
        Assert.That(result, Is.EqualTo("5"));
    }

    [Test]
    public void PipeO_WhenInputIsValid_ThenReturnsSomeWithValue()
    {
        // Arrange
        static Option<string> ConvertToOptionString(int x)
            => Option<string>.Some(x.ToString());

        const int input = 42;

        // Act
        var result = input.PipeO(ConvertToOptionString);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result is Some<string>, Is.True);
            Assert.That(
                result.Match(
                    isSome: v => v,
                    isNone: () => "none"),
                Is.EqualTo("42"));
        }
    }

    [Test]
    public void PipeO_WhenInputIsInvalid_ThenReturnsNone()
    {
        // Arrange
        static Option<string> ConvertToOptionString(int x)
            => x >= 0
                ? Option<string>.Some(x.ToString())
                : Option<string>.None();

        const int input = -1;

        // Act
        var result = input.PipeO(ConvertToOptionString);

        // Assert
        Assert.That(result is None<string>, Is.True);
    }

    [Test]
    public void PipeE_WhenInputIsPositive_ThenReturnsRightWithDoubledValue()
    {
        // Arrange
        const string whenLeftText = "Left";

        static Either<string, int> DoubleIfPositive(int x)
            => x > 0
                ? Either<string, int>.FromRight(x * 2)
                : Either<string, int>.FromLeft(whenLeftText);

        const int input = 10;

        // Act
        var result = input.PipeE(DoubleIfPositive);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result is Right<string, int>, Is.True);
            Assert.That(
                result.Match(
                    isRight: x => x,
                    isLeft: _ => 0), Is.EqualTo(20));
        }
    }

    [Test]
    public void PipeE_WhenInputIsNegative_ThenReturnsLeftWithErrorMessage()
    {
        // Arrange
        const string whenLeftText = "Left";

        static Either<string, int> DoubleIfPositive(int x) =>
            x > 0
                ? Either<string, int>.FromRight(x * 2)
                : Either<string, int>.FromLeft(whenLeftText);

        const int input = -5;

        // Act
        var result = input.PipeE(DoubleIfPositive);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result is Left<string, int>, Is.True);
            Assert.That(
                result.Match(
                    isRight: _ => string.Empty,
                    isLeft: error => error),
                Is.EqualTo(whenLeftText));
        }
    }

    [Test]
    public void Try_WhenOperationSucceeds_ThenReturnsRightWithResult()
    {
        // Arrange
        static int Divide100(int x) => 100 / x;

        const int input = 10;

        // Act
        var result = input.Try(Divide100);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result is Right<Exception, int>, Is.True);
            Assert.That(
                result.Match(
                    isRight: x => x,
                    isLeft: _ => 0),
                Is.EqualTo(10));
        }
    }

    [Test]
    public void Try_WhenOperationThrows_ThenReturnsLeftWithException()
    {
        // Arrange
        static int Divide100(int x) => 100 / x;

        const string whenLeftText = "Left";
        const int input = 0;

        // Act
        var result = input.Try(Divide100);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result is Left<Exception, int>, Is.True);
            Assert.That(
                result.Match(
                    isRight: x => x.ToString(),
                    isLeft: ex => whenLeftText),
                Is.EqualTo(whenLeftText));
        }
    }

    [Test]
    public void Compose_WhenCombiningTwoFunctions_ThenReturnsComposedResult()
    {
        // Arrange
        var doubleX = (int x) => x * 2;
        static string ConvertToString(int x) => x.ToString();

        const int input = 5;

        // Act
        var composed = doubleX.Compose(ConvertToString);
        var result = composed(input);

        // Assert
        Assert.That(result, Is.EqualTo("10"));
    }

    private readonly struct TestContext
    {
        public int Value { get; init; }
    }

    [Test]
    public void InitContext_WhenInitializingWithValue_ThenCreatesContextWithTransformedResult()
    {
        Assert.Ignore("TODO");

        // Arrange
        static string InitFn(TestContext data) => data.Value.ToString();

        var ctxData = new TestContext { Value = 42 };

        // Act
        var context = ctxData.InitContext(InitFn);

        // ...
    }
}