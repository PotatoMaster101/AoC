using System.Runtime.CompilerServices;
using AoC.Helpers;

namespace AoC.Grid;

/// <summary>
/// Represents a position on a grid.
/// </summary>
/// <param name="Row">The row position.</param>
/// <param name="Column">The column position.</param>
public readonly record struct Position(int Row = 0, int Column = 0) : IComparable<Position>
{
    /// <summary>
    /// Gets the origin point (0, 0).
    /// </summary>
    public static readonly Position Origin = new();

    /// <summary>
    /// Gets the unit X position (1, 0).
    /// </summary>
    public static readonly Position UnitX = new(1);

    /// <summary>
    /// Gets the unit Y position (0, 1).
    /// </summary>
    public static readonly Position UnitY = new(0, 1);

    /// <summary>
    /// Gets an array of neighbour positions.
    /// </summary>
    public Position[] Neighbours =>
    [
        this with { Row = Row - 1 },
        this with { Row = Row + 1 },
        this with { Column = Column - 1 },
        this with { Column = Column + 1 }
    ];

    /// <inheritdoc cref="IComparable{T}.CompareTo"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Position other)
    {
        return Row == other.Row ? Column.CompareTo(other.Column) : Row.CompareTo(other.Row);
    }

    /// <summary>
    /// Gets the destination position.
    /// </summary>
    /// <param name="direction">The direction of the destination.</param>
    /// <param name="distance">The distance to the destination.</param>
    /// <returns>The destination position.</returns>
    public Position GetDestination(Direction direction, int distance)
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
    public int GetManhattanDistance(Position destination)
    {
        return Math.Abs(Row - destination.Row) + Math.Abs(Column - destination.Column);
    }

    /// <summary>
    /// Swaps the values in <see cref="Row"/> and <see cref="Column"/>.
    /// </summary>
    /// <returns>The position with the <see cref="Row"/> and <see cref="Column"/> swapped.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Position Swap()
    {
        return new Position(Column, Row);
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
    public Position Wrap(int totalRows, int totalColumns)
    {
        var modRow = (int)MathHelper.Mod(Row, totalRows);
        var modColumn = (int)MathHelper.Mod(Column, totalColumns);
        return new Position(modRow, modColumn);
    }

    /// <summary>
    /// The + operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position operator +(Position left, Position right)
    {
        return new Position(left.Row + right.Row, left.Column + right.Column);
    }

    /// <summary>
    /// The + operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position operator +(Position left, int right)
    {
        return new Position(left.Row + right, left.Column + right);
    }

    /// <summary>
    /// The - operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position operator -(Position left, Position right)
    {
        return new Position(left.Row - right.Row, left.Column - right.Column);
    }

    /// <summary>
    /// The - operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position operator -(Position left, int right)
    {
        return new Position(left.Row - right, left.Column - right);
    }

    /// <summary>
    /// The - operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position operator -(Position left)
    {
        return new Position(-left.Row, -left.Column);
    }

    /// <summary>
    /// The * operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position operator *(Position left, Position right)
    {
        return new Position(left.Row * right.Row, left.Column * right.Column);
    }

    /// <summary>
    /// The * operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position operator *(Position left, int right)
    {
        return new Position(left.Row * right, left.Column * right);
    }

    /// <summary>
    /// The / operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position operator /(Position left, Position right)
    {
        return new Position(left.Row / right.Row, left.Column / right.Column);
    }

    /// <summary>
    /// The / operator.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Position operator /(Position left, int right)
    {
        return new Position(left.Row / right, left.Column / right);
    }

    /// <summary>
    /// The implicit cast operator for <see cref="LongPosition"/>.
    /// </summary>
    /// <param name="position">The <see cref="Position"/> to cast to a <see cref="LongPosition"/>.</param>
    /// <returns>The casted <see cref="LongPosition"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LongPosition(Position position)
    {
        return new LongPosition(position.Row, position.Column);
    }
}
