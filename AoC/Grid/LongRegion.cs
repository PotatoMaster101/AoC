using System.Collections;
using System.Runtime.CompilerServices;

namespace AoC.Grid;

/// <summary>
/// Represents a <see cref="Region"/> where values are stored as <see cref="long"/>.
/// </summary>
public readonly record struct LongRegion : IEnumerable<LongPosition>
{
    /// <summary>
    /// Gets the bottom left position.
    /// </summary>
    public LongPosition BottomLeft => new(MinRow, MinColumn);

    /// <summary>
    /// Gets the bottom right position.
    /// </summary>
    public LongPosition BottomRight => new(MinRow, MaxColumn);

    /// <summary>
    /// Gets the total number of columns.
    /// </summary>
    public long Columns => MaxColumn - MinColumn + 1;

    /// <summary>
    /// Gets the maximum column position.
    /// </summary>
    public long MaxColumn { get; }

    /// <summary>
    /// Gets the maximum row position.
    /// </summary>
    public long MaxRow { get; }

    /// <summary>
    /// Gets the minimum column position.
    /// </summary>
    public long MinColumn { get; }

    /// <summary>
    /// Gets the minimum row position.
    /// </summary>
    public long MinRow { get; }

    /// <summary>
    /// Gets the total number of rows.
    /// </summary>
    public long Rows => MaxRow - MinRow + 1;

    /// <summary>
    /// Gets the top left position.
    /// </summary>
    public LongPosition TopLeft => new(MaxRow, MinColumn);

    /// <summary>
    /// Gets the top right position.
    /// </summary>
    public LongPosition TopRight => new(MaxRow, MaxColumn);

    /// <summary>
    /// Constructs a new <see cref="LongRegion"/>.
    /// </summary>
    /// <param name="maxRow">The maximum row position.</param>
    /// <param name="maxColumn">The maximum column position.</param>
    /// <param name="minRow">The minimum row position.</param>
    /// <param name="minColumn">The minimum column position.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when a min value is greater or equal to a max value.</exception>
    public LongRegion(long maxRow, long maxColumn, long minRow = 0, long minColumn = 0)
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
    public IEnumerable<LongPosition> GetColumn(long column)
    {
        if (column < MinColumn || column > MaxColumn)
            yield break;
        for (var row = MinRow; row <= MaxRow; row++)
            yield return new LongPosition(row, column);
    }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public IEnumerator<LongPosition> GetEnumerator()
    {
        for (var r = MinRow; r <= MaxRow; r++)
            for (var c = MinColumn; c <= MaxColumn; c++)
                yield return new LongPosition(r, c);
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
    public IEnumerable<LongPosition> GetRow(long row)
    {
        if (row < MinRow || row > MaxRow)
            yield break;
        for (var column = MinColumn; column <= MaxColumn; column++)
            yield return new LongPosition(row, column);
    }

    /// <summary>
    /// Determines whether a position is in this region.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>Whether the position is in this region.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool InRegion(LongPosition position)
    {
        return position.Row >= MinRow && position.Row <= MaxRow && position.Column >= MinColumn && position.Column <= MaxColumn;
    }

    /// <summary>
    /// The explicit cast operator for <see cref="Region"/>.
    /// </summary>
    /// <param name="region">The <see cref="LongRegion"/> to cast to a <see cref="Region"/>.</param>
    /// <returns>The casted <see cref="Region"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Region(LongRegion region)
    {
        return new Region((int)region.MaxRow, (int)region.MaxColumn, (int)region.MinRow, (int)region.MinColumn);
    }
}
