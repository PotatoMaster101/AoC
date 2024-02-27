using System.Collections;
using System.Runtime.CompilerServices;

namespace AoC.Ranges;

/// <summary>
/// Represents a range of integers.
/// </summary>
public readonly record struct IntegerRange : IRange<long>
{
    /// <summary>
    /// Gets the increment value.
    /// </summary>
    public long Increment { get; }

    /// <inheritdoc cref="IRange{T}.Max"/>
    public long Max { get; }

    /// <inheritdoc cref="IRange{T}.Min"/>
    public long Min { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="IntegerRange"/>.
    /// </summary>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="increment">The increment value.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="min"/> is greater than <paramref name="max"/>, or <paramref name="increment"/> is 0 or negative.
    /// </exception>
    public IntegerRange(long min, long max, long increment = 1)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(increment);

        Max = max;
        Min = min;
        Increment = increment;
    }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public IEnumerator<long> GetEnumerator()
    {
        for (var i = Min; i <= Max; i += Increment)
            yield return i;
    }

    /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc cref="IRange{T}.InRange"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool InRange(long value)
    {
        return value >= Min && value <= Max;
    }

    /// <inheritdoc cref="IRange{T}.TryGetNext"/>
    public bool TryGetNext(long current, out long next)
    {
        next = current + Increment;
        return InRange(next);
    }

    /// <inheritdoc cref="IRange{T}.TryGetPrevious"/>
    public bool TryGetPrevious(long current, out long previous)
    {
        previous = current - Increment;
        return InRange(previous);
    }
}
