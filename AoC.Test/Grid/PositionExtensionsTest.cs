using System.Collections;
using AoC.Grid;
using Xunit;

namespace AoC.Test.Grid;

/// <summary>
/// Unit tests for <see cref="PositionExtensions"/>.
/// </summary>
public class PositionExtensionsTest
{
    [Theory]
    [InlineData('a', 0, 0, "abc", "abc")]
    [InlineData('b', 0, 1, "abc", "abc")]
    [InlineData('a', 1, 0, "abc", "abc")]
    public void At_ReturnsCorrectChar(char expected, int row, int column, params string[] sut)
    {
        // arrange
        var pos = new Position(row, column);

        // act
        var result = sut.At(pos);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData('a', 0, 0, "abc", "abc")]
    [InlineData('b', 0, 1, "abc", "abc")]
    [InlineData('a', 1, 0, "abc", "abc")]
    public void At_ReturnsCorrectValue(char expected, int row, int column, params string[] grid)
    {
        // arrange
        var pos = new Position(row, column);
        var sut = new List<List<char>>(grid.Length);
        sut.AddRange(grid.Select(text => (List<char>)[..text]));

        // act
        var result = sut.At(pos);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetNeighboursTestData))]
    public void GetValidNeighbours_ReturnsCorrectValues(int maxRow, int maxColumn, int minRow, int minColumn, int row, int column, Position[] expected)
    {
        // arrange
        var sut = new Position(row, column);
        var region = new Region(maxRow, maxColumn, minRow, minColumn);

        // act
        var result = sut.GetValidNeighbours(region);

        // assert
        Assert.Equal(expected.Length, result.Count);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    private class GetNeighboursTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                10, 10, 0, 0, 0, 0, new Position[]
                {
                    new(1),
                    new(0, 1)
                }
            ];
            yield return
            [
                10, 10, 0, 0, 10, 10, new Position[]
                {
                    new(10, 9),
                    new(9, 10)
                }
            ];
            yield return
            [
                10, 10, 0, 0, 5, 5, new Position[]
                {
                    new(5, 6),
                    new(6, 5),
                    new(4, 5),
                    new(5, 4)
                }
            ];
            yield return
            [
                10, 10, 0, 0, -1, 0, new Position[]
                {
                    new()
                }
            ];
        }
    }
}
