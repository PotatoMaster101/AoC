using AoC.Ranges;
using Xunit;

namespace AoC.Test.Ranges;

/// <summary>
/// Unit tests for <see cref="IntegerRange"/>.
/// </summary>
public class IntegerRangeTest
{
    [Theory]
    [InlineData(0, 10, 1)]
    [InlineData(-10, 0, 1)]
    [InlineData(10, 10, 1)]
    [InlineData(5, 7, 5)]
    public void Constructor_SetsMembers(long min, long max, long increment)
    {
        // act
        var result = new IntegerRange(min, max, increment);

        // assert
        Assert.Equal(min, result.Min);
        Assert.Equal(max, result.Max);
        Assert.Equal(increment, result.Increment);
    }

    [Theory]
    [InlineData(1, 0, 1)]
    [InlineData(-5, -10, 1)]
    [InlineData(0, 10, 0)]
    [InlineData(0, 10, -1)]
    public void Constructor_ThrowsOnOutOfRange(long min, long max, long increment)
    {
        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new IntegerRange(min, max, increment));
    }

    [Theory]
    [InlineData(0, 5, 1, 0L, 1L, 2L, 3L, 4L, 5L)]
    [InlineData(-5, 0, 1, -5L, -4L, -3L, -2L, -1L, 0L)]
    [InlineData(0, 5, 2, 0L, 2L, 4L)]
    [InlineData(-5, 0, 3, -5L, -2L)]
    public void GetEnumerator_ReturnsCorrectValue(long min, long max, long increment, params long[] expected)
    {
        // arrange
        var sut = new IntegerRange(min, max, increment);

        // act
        var result = sut.ToList();

        // assert
        Assert.Equal(expected.Length, result.Count);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    [Theory]
    [InlineData(0, 10, 1, 0, true)]
    [InlineData(0, 10, 1, 10, true)]
    [InlineData(-10, -5, 1, -7, true)]
    [InlineData(0, 10, 1, -1, false)]
    [InlineData(0, 10, 1, 11, false)]
    public void InRange_ReturnsCorrectValue(long min, long max, long increment, long value, bool expected)
    {
        // arrange
        var sut = new IntegerRange(min, max, increment);

        // act
        var result = sut.InRange(value);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 10, 1, 0, 1, true)]
    [InlineData(0, 10, 5, 0, 5, true)]
    [InlineData(0, 10, 1, -1, 0, true)]
    [InlineData(0, 10, 1, 10, 11, false)]
    [InlineData(-10, -5, 1, -10, -9, true)]
    [InlineData(-10, -5, 1, -5, -4, false)]
    [InlineData(-10, -5, 20, -5, 15, false)]
    public void TryGetNext_ReturnsCorrectValue(long min, long max, long increment, long value, long expectedValue, bool expectedResult)
    {
        // arrange
        var sut = new IntegerRange(min, max, increment);

        // act
        var result = sut.TryGetNext(value, out var resultValue);

        // assert
        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedValue, resultValue);
    }

    [Theory]
    [InlineData(-10, 0, 1, 0, -1, true)]
    [InlineData(-10, 0, 5, 0, -5, true)]
    [InlineData(-10, 0, 1, 1, 0, true)]
    [InlineData(-10, 0, 20, 0, -20, false)]
    public void TryGetPrevious_ReturnsCorrectValue(long min, long max, long increment, long value, long expectedValue, bool expectedResult)
    {
        // arrange
        var sut = new IntegerRange(min, max, increment);

        // act
        var result = sut.TryGetPrevious(value, out var resultValue);

        // assert
        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedValue, resultValue);
    }
}
