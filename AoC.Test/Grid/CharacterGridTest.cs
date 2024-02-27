using System.Collections;
using AoC.Grid;
using Xunit;

namespace AoC.Test.Grid;

/// <summary>
/// Unit tests for <see cref="CharacterGrid"/>.
/// </summary>
public class CharacterGridTest
{

    [Theory]
    [InlineData(0, 0, 'a', "abc", "def")]
    [InlineData(0, 1, 'b', "abc", "def")]
    [InlineData(0, 2, 'c', "abc", "def")]
    [InlineData(1, 1, 'e', "abc", "def")]
    public void CharacterIndexer_ReturnsCorrectValue(int row, int column, char expected, params string[] lines)
    {
        // arrange
        var position = new Position(row, column);
        var sut = new CharacterGrid(lines);

        // act
        var result = sut[position];

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(3, 5, "aaaaa", "bbbbb", "ccccc")]
    [InlineData(5, 2, "aa", "bb", "cc", "dd", "ee")]
    public void Constructor_SetsMembers(int maxRow, int maxColumn, params string[] content)
    {
        // act
        var result = new CharacterGrid(content);

        // assert
        Assert.Equal(maxRow, result.Region.MaxRow);
        Assert.Equal(maxColumn, result.Region.MaxColumn);
        Assert.Equal(content.Length, result.Content.Length);
        foreach (var item in result.Content)
            Assert.Contains(content, x => x == item);
    }

    [Fact]
    public void Constructor_ThrowsOnEmptyContent()
    {
        // assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new CharacterGrid(Array.Empty<string>()));
        Assert.Throws<ArgumentOutOfRangeException>(() => new CharacterGrid([string.Empty]));
    }

    [Theory]
    [ClassData(typeof(GetNeighboursAtTestData))]
    public void GetNeighboursAt_ReturnsCorrectValue(int row, int column, string[] lines, Position[] expected)
    {
        // arrange
        var position = new Position(row, column);
        var sut = new CharacterGrid(lines);

        // act
        var result = sut.GetNeighboursAt(position);

        // assert
        Assert.Equal(expected.Length, result.Count);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    [Theory]
    [InlineData(0, "aa", "aa")]
    [InlineData(0, "aa", "aa", "bb")]
    [InlineData(1, "bb", "aa", "bb")]
    public void LineIndexer_ReturnsCorrectValue(int index, string expected, params string[] lines)
    {
        // arrange
        var sut = new CharacterGrid(lines);

        // act
        var result = sut[index];

        // assert
        Assert.Equal(expected, result);
    }

    private class GetNeighboursAtTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                0, 0, new[] { "aaa", "bbb", "ccc" }, new Position[]
                {
                    new(0, 1),
                    new(1)
                }
            ];
            yield return
            [
                0, 1, new[] { "aaa", "bbb", "ccc" }, new Position[]
                {
                    new(),
                    new(0, 2),
                    new(1, 1)
                }
            ];
            yield return
            [
                1, 1, new[] { "aaa", "bbb", "ccc" }, new Position[]
                {
                    new(0, 1),
                    new(1),
                    new(1, 2),
                    new(2, 1)
                }
            ];
        }
    }
}
