using AoC.Input;
using Xunit;

namespace AoC.Test.Input;

/// <summary>
/// Unit tests for <see cref="BaseParser{T}"/>
/// </summary>
public class BaseParserTest
{
    private const string TestFile = $"{nameof(BaseParserTest)}.txt";
    private readonly string[] _testFileLines =
    [
        "abc",
        "",
        "def",
        "",
        "",
        "ghi",
        "",
    ];

    public BaseParserTest()
    {
        File.WriteAllLinesAsync(TestFile, _testFileLines);
    }

    [Theory]
    [InlineData(true, "abc", "def", "ghi")]
    [InlineData(false, "abc", "", "", "def", "", "", "", "ghi", "", "")]
    public async Task ReadLine_ReadsLines(bool skipEmpty, params string[] expected)
    {
        // arrange
        using var parser = new DerivedParser(TestFile);

        // act
        var result = await parser.CallReadLines(skipEmpty).ToListAsync();

        // assert
        Assert.Equal(expected.Length, result.Count);
        foreach (var item in result)
            Assert.Contains(expected, x => x == item);
    }

    [Fact]
    public async Task Reset_ResetsStream()
    {
        // arrange
        using var parser = new DerivedParser(TestFile);
        await parser.CallReadLines().ToListAsync();

        // act
        parser.Reset();

        // assert
        var reader = parser.GetReader();
        Assert.Equal(0, reader.BaseStream.Position);
    }

    private class DerivedParser(string filePath) : BaseParser<int>(filePath)
    {
        public StreamReader GetReader()
        {
            return Reader;
        }

        public IAsyncEnumerable<string> CallReadLines(bool skipEmpty = true)
        {
            return ReadLines(skipEmpty);
        }

        public override Task<int> Parse(CancellationToken token = default)
        {
            return Task.FromResult(0);
        }
    }
}
