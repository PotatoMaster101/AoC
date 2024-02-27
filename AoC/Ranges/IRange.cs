namespace AoC.Ranges;

/// <summary>
/// Represents an implementation of a range.
/// </summary>
/// <typeparam name="T">The range type.</typeparam>
public interface IRange<T> : IEnumerable<T>
{
    /// <summary>
    /// Gets the maximum value in the range.
    /// </summary>
    T Max { get; }

    /// <summary>
    /// Gets the minimum value in the range.
    /// </summary>
    T Min { get; }

    /// <summary>
    /// Determines whether a value is in the range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>Whether the value is in a range.</returns>
    bool InRange(T value);

    /// <summary>
    /// Tries to get the next value in the range.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="next">The next value.</param>
    /// <returns>Whether the next value is in the range.</returns>
    bool TryGetNext(T current, out T next);

    /// <summary>
    /// Tries to get the previous value in the range.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="previous">The previous value.</param>
    /// <returns>Whether the previous value is in the range.</returns>
    bool TryGetPrevious(T current, out T previous);
}
