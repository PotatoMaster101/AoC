using System.Runtime.CompilerServices;

namespace AoC.Grid;

/// <summary>
/// Extensions for <see cref="Position"/>.
/// </summary>
public static class PositionExtensions
{
    /// <summary>
    /// Returns a character at the specified position in a character grid.
    /// </summary>
    /// <param name="grid">The character grid.</param>
    /// <param name="position">The position of the character.</param>
    /// <returns>The character at the specified position.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static char At(this IReadOnlyList<string> grid, Position position)
    {
        return grid[position.Row][position.Column];
    }

    /// <summary>
    /// Returns an object at the specified position in a grid.
    /// </summary>
    /// <param name="grid">The grid.</param>
    /// <param name="position">The position of the object.</param>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <returns>The object at the specified position.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T At<T>(this IReadOnlyList<IReadOnlyList<T>> grid, Position position)
    {
        return grid[position.Row][position.Column];
    }

    /// <summary>
    /// Returns a list of valid neighbours.
    /// </summary>
    /// <param name="position">The center position of the neighbours.</param>
    /// <param name="region">The region containing the neighbours.</param>
    /// <returns>The list of valid neighbours.</returns>
    public static IReadOnlyList<Position> GetValidNeighbours(this Position position, Region region)
    {
        ReadOnlySpan<Position> neighbours = stackalloc Position[]
        {
            position with { Row = position.Row - 1 },
            position with { Row = position.Row + 1 },
            position with { Column = position.Column - 1 },
            position with { Column = position.Column + 1 }
        };
        var result = new List<Position>(neighbours.Length);

        // ReSharper disable once ForCanBeConvertedToForeach
        // ReSharper disable once LoopCanBeConvertedToQuery
        for (var i = 0; i < neighbours.Length; i++)
            if (region.InRegion(neighbours[i]))
                result.Add(neighbours[i]);
        return result;
    }
}
