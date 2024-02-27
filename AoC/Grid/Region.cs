using System.Collections;
using System.Runtime.CompilerServices;

namespace AoC.Grid;

/// <summary>
/// Represents a region on a grid.
/// </summary>
public readonly record struct Region : IEnumerable<Position>
{
    /// <summary>
    /// Gets the bottom left position.
    /// </summary>
    public Position BottomLeft => new(MinRow, MinColumn);

    /// <summary>
    /// Gets the bottom right position.
    /// </summary>
    public Position BottomRight => new(MinRow, MaxColumn);

    /// <summary>
    /// Gets the total number of columns.
    /// </summary>
    public int Columns => MaxColumn - MinColumn + 1;

    /// <summary>
    /// Gets the maximum column position.
    /// </summary>
    public int MaxColumn { get; }

    /// <summary>
    /// Gets the maximum row position.
    /// </summary>
    public int MaxRow { get; }

    /// <summary>
    /// Gets the minimum column position.
    /// </summary>
    public int MinColumn { get; }

    /// <summary>
    /// Gets the minimum row position.
    /// </summary>
    public int MinRow { get; }

    /// <summary>
    /// Gets the total number of rows.
    /// </summary>
    public int Rows => MaxRow - MinRow + 1;

    /// <summary>
    /// Gets the top left position.
    /// </summary>
    public Position TopLeft => new(MaxRow, MinColumn);

    /// <summary>
    /// Gets the top right position.
    /// </summary>
    public Position TopRight => new(MaxRow, MaxColumn);

    /// <summary>
    /// Constructs a new <see cref="Region"/>.
    /// </summary>
    /// <param name="maxRow">The maximum row position.</param>
    /// <param name="maxColumn">The maximum column position.</param>
    /// <param name="minRow">The minimum row position.</param>
    /// <param name="minColumn">The minimum column position.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when a min value is greater or equal to a max value.</exception>
    public Region(int maxRow, int maxColumn, int minRow = 0, int minColumn = 0)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minRow, maxRow);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minColumn, maxColumn);

        MaxColumn = maxColumn;
        MaxRow = maxRow;
        MinColumn = minColumn;
        MinRow = minRow;
    }

    /// <summary>
    /// Returns the positions on a column.
    /// </summary>
    /// <param name="column">The column to get the positions.</param>
    /// <returns>The positions on a column.</returns>
    public IEnumerable<Position> GetColumn(int column)
    {
        if (column < MinColumn || column > MaxColumn)
            yield break;
        for (var row = MinRow; row <= MaxRow; row++)
            yield return new Position(row, column);
    }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public IEnumerator<Position> GetEnumerator()
    {
        for (var r = MinRow; r <= MaxRow; r++)
            for (var c = MinColumn; c <= MaxColumn; c++)
                yield return new Position(r, c);
    }

    /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Returns the positions on a row.
    /// </summary>
    /// <param name="row">The row to get the positions.</param>
    /// <returns>The positions on a row.</returns>
    public IEnumerable<Position> GetRow(int row)
    {
        if (row < MinRow || row > MaxRow)
            yield break;
        for (var column = MinColumn; column <= MaxColumn; column++)
            yield return new Position(row, column);
    }

    /// <summary>
    /// Determines whether a position is in this region.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>Whether the position is in this region.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool InRegion(Position position)
    {
        return position.Row >= MinRow && position.Row <= MaxRow && position.Column >= MinColumn && position.Column <= MaxColumn;
    }

    /// <summary>
    /// The implicit cast operator for <see cref="LongRegion"/>.
    /// </summary>
    /// <param name="region">The <see cref="Region"/> to cast to a <see cref="LongRegion"/>.</param>
    /// <returns>The casted <see cref="LongRegion"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LongRegion(Region region)
    {
        return new LongRegion(region.MaxRow, region.MaxColumn, region.MinRow, region.MinColumn);
    }
}
