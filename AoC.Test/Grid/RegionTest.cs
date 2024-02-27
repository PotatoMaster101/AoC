using System.Collections;
using AoC.Grid;
using Xunit;

namespace AoC.Test.Grid;

/// <summary>
/// Unit tests for <see cref="Region"/>.
/// </summary>
public class RegionTest
{
    [Theory]
    [InlineData(10, 15, 0, 0, 0, 0)]
    [InlineData(5, 10, 0, 0, 0, 0)]
    [InlineData(0, 0, -5, -10, -5, -10)]
    [InlineData(-5, -10, -10, -15, -10, -15)]
    public void BottomLeft_GetsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, int expectedRow, int expectedColumn)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

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
    public void BottomRight_GetsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, int expectedRow, int expectedColumn)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

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
    public void Columns_GetsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, int expected)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.Columns;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 10, 0, 0)]
    [InlineData(0, 0, -10, -10)]
    public void Constructor_SetsMembers(int maxRow, int maxColumn, int minRow, int minColumn)
    {
        // act
        var result = new Region(maxRow, maxColumn, minRow, minColumn);

        // assert
        Assert.Equal(maxRow, result.MaxRow);
        Assert.Equal(maxColumn, result.MaxColumn);
        Assert.Equal(minRow, result.MinRow);
        Assert.Equal(minColumn, result.MinColumn);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 0, 10, 10)]
    public void Constructor_ThrowsOnOutOfRange(int maxRow, int maxColumn, int minRow, int minColumn)
    {
        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Region(maxRow, maxColumn, minRow, minColumn));
    }

    [Theory]
    [ClassData(typeof(GetColumnTestData))]
    public void GetColumn_ReturnsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, int column, Position[] expected)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.GetColumn(column).ToList();

        // assert
        Assert.Equal(expected.Length, result.Count);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    [Theory]
    [ClassData(typeof(GetEnumeratorTestData))]
    public void GetEnumerator_ReturnsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, Position[] expected)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.ToList();

        // assert
        Assert.Equal(expected.Length, result.Count);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    [Theory]
    [ClassData(typeof(GetRowTestData))]
    public void GetRow_ReturnsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, int column, Position[] expected)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

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
    public void InRegion_ReturnsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, int row, int column, bool expected)
    {
        // arrange
        var pos = new Position(row, column);
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.InRegion(pos);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 10, 0, 0)]
    [InlineData(-5, -5, -10, -10)]
    [InlineData(10, 10, -10, -10)]
    public void OperatorImplicitCast_ReturnsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

        // act
        LongRegion result = sut;

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
    public void Rows_GetsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, int expected)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

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
    public void TopLeft_GetsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, int expectedRow, int expectedColumn)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

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
    public void TopRight_GetsCorrectValue(int maxRow, int maxColumn, int minRow, int minColumn, int expectedRow, int expectedColumn)
    {
        // arrange
        var sut = new Region(maxRow, maxColumn, minRow, minColumn);

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
                5, 5, 0, 0, 0, new Position[]
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
                5, 5, 0, 0, 5, new Position[]
                {
                    new(0, 5),
                    new(1, 5),
                    new(2, 5),
                    new(3, 5),
                    new(4, 5),
                    new(5, 5)
                }
            ];
            yield return [5, 5, 0, 0, 6, Array.Empty<Position>()];
        }
    }

    private class GetRowTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                5, 5, 0, 0, 0, new Position[]
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
                5, 5, 0, 0, 5, new Position[]
                {
                    new(5),
                    new(5, 1),
                    new(5, 2),
                    new(5, 3),
                    new(5, 4),
                    new(5, 5)
                }
            ];
            yield return [5, 5, 0, 0, 6, Array.Empty<Position>()];
        }
    }

    private class GetEnumeratorTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                3, 3, 0, 0, new Position[]
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
                1, 1, -1, -1, new Position[]
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
