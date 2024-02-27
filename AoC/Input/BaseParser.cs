using System.Runtime.CompilerServices;

namespace AoC.Input;

/// <summary>
/// Base implementation of an input parser.
/// </summary>
/// <param name="filePath">The path to the input file.</param>
public abstract class BaseParser<T>(string filePath) : IParser<T>, IDisposable
{
    /// <summary>
    /// The reader for input file.
    /// </summary>
    protected readonly StreamReader Reader = new(filePath);

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="IParser{T}.Parse"/>
    public abstract Task<T> Parse(CancellationToken token = default);

    /// <inheritdoc cref="IParser{T}.Reset"/>
    public void Reset()
    {
        Reader.BaseStream.Position = 0;
        Reader.DiscardBufferedData();
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    /// <param name="disposing">Whether this instance is getting disposed.</param>
    // ReSharper disable once VirtualMemberNeverOverridden.Global
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            Reader.Dispose();
    }

    /// <summary>
    /// Reads the lines from the input.
    /// </summary>
    /// <param name="skipEmpty">Whether to skip empty lines.</param>
    /// <param name="token">The cancellation token for cancelling this operation.</param>
    /// <returns>The lines from the input.</returns>
    protected async IAsyncEnumerable<string> ReadLines(bool skipEmpty = true, [EnumeratorCancellation] CancellationToken token = default)
    {
        while (!Reader.EndOfStream)
        {
            var line = await Reader.ReadLineAsync(token).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(line))
                yield return line;
            if (!skipEmpty)
                yield return string.Empty;
        }
    }
}
