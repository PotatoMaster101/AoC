namespace AoC.Input;

/// <summary>
/// Represents an implementation of an input parser.
/// </summary>
/// <typeparam name="T">The parser output type.</typeparam>
public interface IParser<T>
{
    /// <summary>
    /// Parses the input.
    /// </summary>
    /// <param name="token">The cancellation token for cancelling this operation.</param>
    /// <returns>The parsed output.</returns>
    Task<T> Parse(CancellationToken token = default);

    /// <summary>
    /// Resets the parser.
    /// </summary>
    void Reset();
}
