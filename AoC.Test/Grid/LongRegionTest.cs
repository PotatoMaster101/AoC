using System.Collections;
using AoC.Grid;
using Xunit;

namespace AoC.Test.Grid;

/// <summary>
/// Unit tests for <see cref="LongRegion"/>
/// </summary>
public class LongRegionTest
{
    [Theory]
    [InlineData(10, 15, 0, 0, 0, 0)]
    [InlineData(5, 10, 0, 0, 0, 0)]
    [InlineData(0, 0, -5, -10, -5, -10)]
    [InlineData(-5, -10, -10, -15, -10, -15)]
    public void BottomLeft_GetsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.BottomLeft;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 15, 0, 0, 0, 15)]
    [InlineData(5, 10, 0, 0, 0, 10)]
    [InlineData(0, 0, -5, -10, -5, 0)]
    [InlineData(-5, -10, -10, -15, -10, -10)]
    public void BottomRight_GetsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.BottomRight;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 10, 0, 0, 11)]
    [InlineData(-5, -5, -10, -10, 6)]
    [InlineData(10, 10, -10, -10, 21)]
    public void Columns_GetsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, long expected)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.Columns;

        // assert
        Assert.Equal(expected, result);
    }

        [Theory]
    [InlineData(10, 10, 0, 0)]
    [InlineData(0, 0, -10, -10)]
    public void Constructor_SetsMembers(long maxRow, long maxColumn, long minRow, long minColumn)
    {
        // act
        var result = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // assert
        Assert.Equal(maxRow, result.MaxRow);
        Assert.Equal(maxColumn, result.MaxColumn);
        Assert.Equal(minRow, result.MinRow);
        Assert.Equal(minColumn, result.MinColumn);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 0, 10, 10)]
    public void Constructor_ThrowsOnOutOfRange(long maxRow, long maxColumn, long minRow, long minColumn)
    {
        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new LongRegion(maxRow, maxColumn, minRow, minColumn));
    }

    [Theory]
    [ClassData(typeof(GetColumnTestData))]
    public void GetColumn_ReturnsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, long column, LongPosition[] expected)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.GetColumn(column).ToList();

        // assert
        Assert.Equal(expected.Length, result.Count);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    [Theory]
    [ClassData(typeof(GetEnumeratorTestData))]
    public void GetEnumerator_ReturnsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, LongPosition[] expected)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.ToList();

        // assert
        Assert.Equal(expected.Length, result.Count);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    [Theory]
    [ClassData(typeof(GetRowTestData))]
    public void GetRow_ReturnsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, long column, LongPosition[] expected)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.GetRow(column).ToList();

        // assert
        Assert.Equal(expected.Length, result.Count);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    [Theory]
    [InlineData(10, 10, 0, 0, 0, 0, true)]
    [InlineData(10, 10, 0, 0, 10, 10, true)]
    [InlineData(10, 10, 0, 0, -1, -1, false)]
    [InlineData(10, 10, 0, 0, 11, 11, false)]
    [InlineData(-5, -5, -10, -10, -7, -7, true)]
    public void InRegion_ReturnsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, long row, long column, bool expected)
    {
        // arrange
        var pos = new LongPosition(row, column);
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.InRegion(pos);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 10, 0, 0)]
    [InlineData(-5, -5, -10, -10)]
    [InlineData(10, 10, -10, -10)]
    public void OperatorExplicitCast_ReturnsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = (Region)sut;

        // assert
        Assert.Equal(maxRow, result.MaxRow);
        Assert.Equal(maxColumn, result.MaxColumn);
        Assert.Equal(minRow, result.MinRow);
        Assert.Equal(minColumn, result.MinColumn);
    }

    [Theory]
    [InlineData(10, 10, 0, 0, 11)]
    [InlineData(-5, -5, -10, -10, 6)]
    [InlineData(10, 10, -10, -10, 21)]
    public void Rows_GetsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, long expected)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.Rows;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 15, 0, 0, 10, 0)]
    [InlineData(5, 10, 0, 0, 5, 0)]
    [InlineData(0, 0, -5, -10, 0, -10)]
    [InlineData(-5, -10, -10, -15, -5, -15)]
    public void TopLeft_GetsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.TopLeft;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(10, 15, 0, 0, 10, 15)]
    [InlineData(5, 10, 0, 0, 5, 10)]
    [InlineData(0, 0, -5, -10, 0, 0)]
    [InlineData(-5, -10, -10, -15, -5, -10)]
    public void TopRight_GetsCorrectValue(long maxRow, long maxColumn, long minRow, long minColumn, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongRegion(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.TopRight;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    private class GetColumnTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                5, 5, 0, 0, 0, new LongPosition[]
                {
                    new(),
                    new(1),
                    new(2),
                    new(3),
                    new(4),
                    new(5)
                }
            ];
            yield return
            [
                5, 5, 0, 0, 5, new LongPosition[]
                {
                    new(0, 5),
                    new(1, 5),
                    new(2, 5),
                    new(3, 5),
                    new(4, 5),
                    new(5, 5)
                }
            ];
            yield return [5, 5, 0, 0, 6, Array.Empty<LongPosition>()];
        }
    }

    private class GetRowTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                5, 5, 0, 0, 0, new LongPosition[]
                {
                    new(),
                    new(0, 1),
                    new(0, 2),
                    new(0, 3),
                    new(0, 4),
                    new(0, 5)
                }
            ];
            yield return
            [
                5, 5, 0, 0, 5, new LongPosition[]
                {
                    new(5),
                    new(5, 1),
                    new(5, 2),
                    new(5, 3),
                    new(5, 4),
                    new(5, 5)
                }
            ];
            yield return [5, 5, 0, 0, 6, Array.Empty<LongPosition>()];
        }
    }

    private class GetEnumeratorTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                3, 3, 0, 0, new LongPosition[]
                {
                    new(),
                    new(0, 1),
                    new(0, 2),
                    new(0, 3),
                    new(1),
                    new(1, 1),
                    new(1, 2),
                    new(1, 3),
                    new(2),
                    new(2, 1),
                    new(2, 2),
                    new(2, 3),
                    new(3),
                    new(3, 1),
                    new(3, 2),
                    new(3, 3)
                }
            ];
            yield return
            [
                1, 1, -1, -1, new LongPosition[]
                {
                    new(-1, -1),
                    new(-1),
                    new(-1, 1),
                    new (0, -1),
                    new(),
                    new(0, 1),
                    new(1, -1),
                    new(1),
                    new(1, 1)
                }
            ];
        }
    }
}
