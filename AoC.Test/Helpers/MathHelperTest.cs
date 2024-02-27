using AoC.Helpers;
using Xunit;

namespace AoC.Test.Helpers;

/// <summary>
/// Unit tests for <see cref="MathHelper"/>
/// </summary>
public class MathHelperTest
{
    [Theory]
    [InlineData(8, 16, 8)]
    [InlineData(7, 16, 1)]
    public void ComputeGcd_ReturnsCorrectValue(long a, long b, long expected)
    {
        // act
        var result = MathHelper.ComputeGcd(a, b);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(16409L, 16409L)]
    [InlineData(269L, 16409L, 19637L, 18023L, 15871L, 14257L, 12643L)]
    public void ComputeGcd_List_ReturnsCorrectValue(long expected, params long[] input)
    {
        // act
        var result = MathHelper.ComputeGcd(input);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(8, 16, 16)]
    [InlineData(12, 15, 60)]
    public void ComputeLcm_ReturnsCorrectValue(long a, long b, long expected)
    {
        // act
        var result = MathHelper.ComputeLcm(a, b);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(16409L, 16409L)]
    [InlineData(11795205644011L, 16409L, 19637L, 18023L, 15871L, 14257L, 12643L)]
    public void ComputeLcm_List_ReturnsCorrectValue(long expected, params long[] input)
    {
        // act
        var result = MathHelper.ComputeLcm(input);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 5, 0)]
    [InlineData(5, 2, 1)]
    [InlineData(-10, -5, 0)]
    [InlineData(-5, -9, -5)]
    [InlineData(-9, -5, -4)]
    public void Mod_ReturnsCorrectValue(long a, long b, long expected)
    {
        // act
        var result = MathHelper.Mod(a, b);

        // assert
        Assert.Equal(expected, result);
    }
}
