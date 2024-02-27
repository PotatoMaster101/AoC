using AoC.Input;
using Xunit;

namespace AoC.Test.Input;

/// <summary>
/// Unit tests for <see cref="ParserExtensions"/>
/// </summary>
public class ParserExtensionsTest
{
    [Theory]
    [InlineData("a,b,c", ",", "a", "b", "c")]
    [InlineData("a,  b ,  ,,c", ",", "a", "b", "c")]
    [InlineData("a::  b :  :::c::::::", ":", "a", "b", "c")]
    public void SplitAndTrim_ReturnsCorrectValue(string str, string delim, params string[] expected)
    {
        // act
        var result = str.SplitAndTrim(delim);

        // assert
        Assert.Equal(expected.Length, result.Length);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    [Theory]
    [InlineData("0, 0", ",", 0, 0)]
    [InlineData("-5:-10", ":", -5, -10)]
    [InlineData("10            5", " ", 10, 5)]
    public void ToLongPosition_ReturnsCorrectValue(string str, string delim, long expectedRow, long expectedColumn)
    {
        // act
        var result = str.ToLongPosition(delim);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData("0, 0", ",", 0, 0)]
    [InlineData("-5:-10", ":", -5, -10)]
    [InlineData("10            5", " ", 10, 5)]
    public void ToPosition_ReturnsCorrectValue(string str, string delim, int expectedRow, int expectedColumn)
    {
        // act
        var result = str.ToPosition(delim);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData("0, 0", ",", 0, 0)]
    [InlineData("-5.3:-10.7", ":", -5.3, -10.7)]
    [InlineData("10.23            5.88", " ", 10.23, 5.88)]
    public void ToVector2_ReturnsCorrectValue(string str, string delim, float expectedX, float expectedY)
    {
        // act
        var result = str.ToVector2(delim);

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData("0, 0, 0", ",", 0, 0, 0)]
    [InlineData("-5.3:-10.7:-0.99", ":", -5.3, -10.7, -0.99)]
    [InlineData("10.23            5.88 9.99", " ", 10.23, 5.88, 9.99)]
    public void ToVector3_ReturnsCorrectValue(string str, string delim, float expectedX, float expectedY, float expectedZ)
    {
        // act
        var result = str.ToVector3(delim);

        // assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
        Assert.Equal(expectedZ, result.Z);
    }
}
