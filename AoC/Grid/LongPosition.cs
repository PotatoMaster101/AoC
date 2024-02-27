using System.Runtime.CompilerServices;
using AoC.Helpers;

namespace AoC.Grid;

/// <summary>
/// Represents a <see cref="Position"/> where values are stored as <see cref="long"/>.
/// </summary>
/// <param name="Row">The row position.</param>
/// <param name="Column">The column position.</param>
public readonly record struct LongPosition(long Row = 0, long Column = 0) : IComparable<LongPosition>
{
    /// <summary>
    /// Gets the origin point (0, 0).
    /// </summary>
    public static readonly LongPosition Origin = new();

    /// <summary>
    /// Gets the unit X position (1, 0).
    /// </summary>
    public static readonly LongPosition UnitX = new(1);

    /// <summary>
    /// Gets the unit Y position (0, 1).
    /// </summary>
    public static readonly LongPosition UnitY = new(0, 1);

    /// <summary>
    /// Gets an array of neighbour positions.
    /// </summary>
    public LongPosition[] Neighbours =>
    [
        this with { Row = Row - 1 },
        this with { Row = Row + 1 },
        this with { Column = Column - 1 },
        this with { Column = Column + 1 }
    ];

    /// <inheritdoc cref="IComparable{T}.CompareTo"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(LongPosition other)
    {
        return Row == other.Row ? Column.CompareTo(other.Column) : Row.CompareTo(other.Row);
    }

    /// <summary>
    /// Gets the destination position.
    /// </summary>
    /// <param name="direction">The direction of the destination.</param>
    /// <param name="distance">The distance to the destination.</param>
    /// <returns>The destination position.</returns>
    public LongPosition GetDestination(Direction direction, long distance)
    {
        return direction switch
        {
            Direction.Bottom => this with { Row = Row - distance },
            Direction.Left => this with { Column = Column - distance },
            Direction.Right => this with { Column = Column + distance },
            _ => this with { Row = Row + distance }
        };
    }

    /// <summary>
    /// Returns the Manhattan distance between this position and another position.
    /// </summary>
    /// <param name="destination">The destination position to compute the Manhattan distance.</param>
    /// <returns>The Manhattan distance between this position and another position.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long GetManhattanDistance(LongPosition destination)
    {
        return Math.Abs(Row - destination.Row) + Math.Abs(Column - destination.Column);
    }

    /// <summary>
    /// Swaps the values in <see cref="Row"/> and <see cref="Column"/>.
    /// </summary>
    /// <returns>The position with the <see cref="Row"/> and <see cref="Column"/> swapped.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LongPosition Swap()
    {
        return new LongPosition(Column, Row);
    }

    /// <inheritdoc cref="object.ToString"/>
    public override string ToString()
    {
        return $"({Row}, {Column})";
    }

    /// <summary>
    /// Wraps this position so it can fit into a region.
    /// </summary>
    /// <param name="totalRows">The total number of rows in the region.</param>
    /// <param name="totalColumns">The total number of columns in the region.</param>
    /// <returns>The wrapped position.</returns>
    public LongPosition Wrap(long totalRows, long totalColumns)
    {
        var modRow = MathHelper.Mod(Row, totalRows);
        var modColumn = MathHelper.Mod(Column, totalColumns);
        return new LongPosition(modRow, modColumn);
    }

    /// <summary>
    /// The + operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongPosition operator +(LongPosition left, LongPosition right)
    {
        return new LongPosition(left.Row + right.Row, left.Column + right.Column);
    }

    /// <summary>
    /// The + operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongPosition operator +(LongPosition left, long right)
    {
        return new LongPosition(left.Row + right, left.Column + right);
    }

    /// <summary>
    /// The - operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongPosition operator -(LongPosition left, LongPosition right)
    {
        return new LongPosition(left.Row - right.Row, left.Column - right.Column);
    }

    /// <summary>
    /// The - operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongPosition operator -(LongPosition left, long right)
    {
        return new LongPosition(left.Row - right, left.Column - right);
    }

    /// <summary>
    /// The - operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongPosition operator -(LongPosition left)
    {
        return new LongPosition(-left.Row, -left.Column);
    }

    /// <summary>
    /// The * operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongPosition operator *(LongPosition left, LongPosition right)
    {
        return new LongPosition(left.Row * right.Row, left.Column * right.Column);
    }

    /// <summary>
    /// The * operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongPosition operator *(LongPosition left, long right)
    {
        return new LongPosition(left.Row * right, left.Column * right);
    }

    /// <summary>
    /// The / operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongPosition operator /(LongPosition left, LongPosition right)
    {
        return new LongPosition(left.Row / right.Row, left.Column / right.Column);
    }

    /// <summary>
    /// The / operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongPosition operator /(LongPosition left, long right)
    {
        return new LongPosition(left.Row / right, left.Column / right);
    }

    /// <summary>
    /// The explicit cast operator for <see cref="Position"/>.
    /// </summary>
    /// <param name="position">The <see cref="LongPosition"/> to cast to a <see cref="Position"/>.</param>
    /// <returns>The casted <see cref="Position"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Position(LongPosition position)
    {
        return new Position((int)position.Row, (int)position.Column);
    }
}
