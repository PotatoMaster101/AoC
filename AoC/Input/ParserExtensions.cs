using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using AoC.Grid;

namespace AoC.Input;

/// <summary>
/// Extension methods for parsing input.
/// </summary>
public static class ParserExtensions
{
    /// <summary>
    /// Splits a string and trims the entries.
    /// </summary>
    /// <param name="str">The string to split.</param>
    /// <param name="delim">The split delimiter.</param>
    /// <returns>The split trimmed entries.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string[] SplitAndTrim(this string str, string delim = ",")
    {
        return str.Split(delim, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// Parses a string to a <see cref="LongPosition"/>.
    /// </summary>
    /// <param name="str">The string to parse.</param>
    /// <param name="delim">The parse delimiter.</param>
    /// <returns>The parsed <see cref="LongPosition"/>.</returns>
    public static LongPosition ToLongPosition(this string str, string delim = ",")
    {
        var splits = str.SplitAndTrim(delim);
        return new LongPosition(long.Parse(splits[0]), long.Parse(splits[1]));
    }

    /// <summary>
    /// Parses a string to a <see cref="Position"/>.
    /// </summary>
    /// <param name="str">The string to parse.</param>
    /// <param name="delim">The parse delimiter.</param>
    /// <returns>The parsed <see cref="Position"/>.</returns>
    public static Position ToPosition(this string str, string delim = ",")
    {
        var splits = str.SplitAndTrim(delim);
        return new Position(int.Parse(splits[0]), int.Parse(splits[1]));
    }

    /// <summary>
    /// Parses a string to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="str">The string to parse.</param>
    /// <param name="delim">The parse delimiter.</param>
    /// <returns>The parsed <see cref="Vector2"/>.</returns>
    public static Vector2 ToVector2(this string str, string delim = ",")
    {
        var splits = str.SplitAndTrim(delim);
        return new Vector2(
            float.Parse(splits[0], CultureInfo.InvariantCulture),
            float.Parse(splits[1], CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Parses a string to a <see cref="Vector3"/>.
    /// </summary>
    /// <param name="str">The string to parse.</param>
    /// <param name="delim">The parse delimiter.</param>
    /// <returns>The parsed <see cref="Vector3"/>.</returns>
    public static Vector3 ToVector3(this string str, string delim = ",")
    {
        var splits = str.SplitAndTrim(delim);
        return new Vector3(
            float.Parse(splits[0], CultureInfo.InvariantCulture),
            float.Parse(splits[1], CultureInfo.InvariantCulture),
            float.Parse(splits[2], CultureInfo.InvariantCulture));
    }
}
