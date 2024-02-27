using System.Collections;
using AoC.Grid;
using Xunit;

namespace AoC.Test.Grid;

/// <summary>
/// Unit tests for <see cref="LongPosition"/>.
/// </summary>
public class LongPositionTest
{
    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 0, -1)]
    [InlineData(0, 0, 0, 1, -1)]
    [InlineData(0, 0, 1, 1, -1)]
    [InlineData(1, 0, 0, 0, 1)]
    [InlineData(0, 1, 0, 0, 1)]
    [InlineData(1, 1, 0, 0, 1)]
    public void CompareTo_ReturnsCorrectValue(long row, long column, long otherRow, long otherColumn, long expected)
    {
        // arrange
        var sut = new LongPosition(row, column);
        var other = new LongPosition(otherRow, otherColumn);

        // act
        var result = sut.CompareTo(other);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(0, 0)]
    [InlineData(10, 5)]
    [InlineData(5, 10)]
    public void Constructor_SetsMembers(long row, long column)
    {
        // act
        var sut = new LongPosition(row, column);

        // assert
        Assert.Equal(row, sut.Row);
        Assert.Equal(column, sut.Column);
    }

    [Theory]
    [InlineData(0, 0, 10, Direction.Bottom, -10, 0)]
    [InlineData(0, 0, 10, Direction.Left, 0, -10)]
    [InlineData(0, 0, 10, Direction.Right, 0, 10)]
    [InlineData(0, 0, 10, Direction.Top, 10, 0)]
    [InlineData(10, 10, 5, Direction.Bottom, 5, 10)]
    [InlineData(10, 10, 5, Direction.Left, 10, 5)]
    [InlineData(10, 10, 5, Direction.Right, 10, 15)]
    [InlineData(10, 10, 5, Direction.Top, 15, 10)]
    public void GetDestination_ReturnsCorrectValue(long row, long column, long distance, Direction direction, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row, column);

        // act
        var result = sut.GetDestination(direction, distance);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, 2)]
    [InlineData(1, 1, 0, 0, 2)]
    [InlineData(-1, 0, 0, 1, 2)]
    [InlineData(10, 10, -10, -10, 40)]
    public void GetManhattanDistance_ReturnsCorrectValue(long row1, long column1, long row2, long column2, long expected)
    {
        // arrange
        var sut = new LongPosition(row1, column1);
        var another = new LongPosition(row2, column2);

        // act
        var result = sut.GetManhattanDistance(another);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(NeighboursTestData))]
    public void Neighbours_GetsCorrectValue(long row, long column, LongPosition[] expected)
    {
        // arrange
        var sut = new LongPosition(row, column);

        // act
        var result = sut.Neighbours;

        // assert
        Assert.Equal(expected.Length, result.Length);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

        [Theory]
    [InlineData(0, 0, 1, 1, 1, 1)]
    [InlineData(5, 5, 10, 10, 15, 15)]
    [InlineData(-10, -10, -5, -5, -15, -15)]
    [InlineData(-1, -1, 1, 1, 0, 0)]
    public void OperatorAdd_ReturnsCorrectValue(long row1, long column1, long row2, long column2, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row1, column1);
        var another = new LongPosition(row2, column2);

        // act
        var result = sut + another;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, 1)]
    [InlineData(5, 5, 10, 15, 15)]
    [InlineData(-10, -10, -5, -15, -15)]
    [InlineData(-1, -1, 1, 0, 0)]
    public void OperatorAddInt_ReturnsCorrectValue(long row1, long column1, long amount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row1, column1);

        // act
        var result = sut + amount;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(3, 3, 3, 3, 1, 1)]
    [InlineData(10, 10, 5, 5, 2, 2)]
    [InlineData(-10, -10, -5, -5, 2, 2)]
    [InlineData(-10, -10, 5, 5, -2, -2)]
    public void OperatorDivide_ReturnsCorrectValue(long row1, long column1, long row2, long column2, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row1, column1);
        var another = new LongPosition(row2, column2);

        // act
        var result = sut / another;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(3, 3, 3, 1, 1)]
    [InlineData(10, 10, 5, 2, 2)]
    [InlineData(-10, -10, -5, 2, 2)]
    [InlineData(-10, -10, 5, -2, -2)]
    public void OperatorDivideInt_ReturnsCorrectValue(long row1, long column1, long amount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row1, column1);

        // act
        var result = sut / amount;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, 0, 0)]
    [InlineData(5, 5, 10, 10, 50, 50)]
    [InlineData(-10, -10, -5, -5, 50, 50)]
    [InlineData(-1, -1, 1, 1, -1, -1)]
    public void OperatorMultiply_ReturnsCorrectValue(long row1, long column1, long row2, long column2, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row1, column1);
        var another = new LongPosition(row2, column2);

        // act
        var result = sut * another;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 0)]
    [InlineData(5, 5, 10, 50, 50)]
    [InlineData(-10, -10, -5, 50, 50)]
    [InlineData(-1, -1, 1, -1, -1)]
    public void OperatorAddMultiply_ReturnsCorrectValue(long row1, long column1, long amount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row1, column1);

        // act
        var result = sut * amount;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void OperatorExplicitCast_ReturnsCorrectValue(long row, long column)
    {
        // arrange
        var sut = new LongPosition(row, column);

        // act
        var result = (Position)sut;

        // assert
        Assert.Equal(row, result.Row);
        Assert.Equal(column, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(5, 5, -5, -5)]
    [InlineData(-5, -5, 5, 5)]
    public void OperatorNegate_ReturnsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row, column);

        // act
        var result = -sut;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, 1, -1, -1)]
    [InlineData(5, 5, 10, 10, -5, -5)]
    [InlineData(-10, -10, -5, -5, -5, -5)]
    [InlineData(-1, -1, 1, 1, -2, -2)]
    public void OperatorSubtract_ReturnsCorrectValue(long row1, long column1, long row2, long column2, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row1, column1);
        var another = new LongPosition(row2, column2);

        // act
        var result = sut - another;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 1, -1, -1)]
    [InlineData(5, 5, 10, -5, -5)]
    [InlineData(-10, -10, -5, -5, -5)]
    [InlineData(-1, -1, 1, -2, -2)]
    public void OperatorSubtractInt_ReturnsCorrectValue(long row1, long column1, long amount, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row1, column1);

        // act
        var result = sut - amount;

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Fact]
    public void Origin_GetsCorrectValue()
    {
        // act
        var result = LongPosition.Origin;

        // assert
        Assert.Equal(0, result.Row);
        Assert.Equal(0, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, 0, 0, 1)]
    [InlineData(5, 10, 10, 5)]
    [InlineData(-10, -5, -5, -10)]
    public void Swap_ReturnsCorrectValue(long row, long column, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row, column);

        // act
        var result = sut.Swap();

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }

    [Theory]
    [InlineData(0, 0, "(0, 0)")]
    [InlineData(10, 5, "(10, 5)")]
    [InlineData(-10, -5, "(-10, -5)")]
    public void ToString_ReturnsCorrectValue(long row, long column, string expected)
    {
        // arrange
        var sut = new LongPosition(row, column);

        // act
        var result = sut.ToString();

        // assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void UnitX_GetsCorrectValue()
    {
        // act
        var result = LongPosition.UnitX;

        // assert
        Assert.Equal(1, result.Row);
        Assert.Equal(0, result.Column);
    }

    [Fact]
    public void UnitY_GetsCorrectValue()
    {
        // act
        var result = LongPosition.UnitY;

        // assert
        Assert.Equal(0, result.Row);
        Assert.Equal(1, result.Column);
    }

    [Theory]
    [InlineData(0, 0, 10, 10, 0, 0)]
    [InlineData(10, 10, 10, 10, 0, 0)]
    [InlineData(-10, -10, 10, 10, 0, 0)]
    [InlineData(9, 9, 10, 10, 9, 9)]
    [InlineData(-1, -1, 10, 10, 9, 9)]
    public void Wrap_ReturnsCorrectValue(long row, long column, long totalRows, long totalColumns, long expectedRow, long expectedColumn)
    {
        // arrange
        var sut = new LongPosition(row, column);

        // act
        var result = sut.Wrap(totalRows, totalColumns);

        // assert
        Assert.Equal(expectedRow, result.Row);
        Assert.Equal(expectedColumn, result.Column);
    }


    private class NeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                0, 0, new LongPosition[]
                {
                    new(-1),
                    new(1),
                    new(0, -1),
                    new(0, 1)
                }
            ];
            yield return
            [
                10, 10, new LongPosition[]
                {
                    new(9, 10),
                    new(11, 10),
                    new(10, 9),
                    new(10, 11)
                }
            ];
            yield return
            [
                -10, -10, new LongPosition[]
                {
                    new(-9, -10),
                    new(-11, -10),
                    new(-10, -9),
                    new(-10, -11)
                }
            ];
        }
    }
}
