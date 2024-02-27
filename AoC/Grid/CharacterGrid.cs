namespace AoC.Grid;

/// <summary>
/// Represents a grid of characters.
/// </summary>
public class CharacterGrid
{
    /// <summary>
    /// Gets the grid content.
    /// </summary>
    public string[] Content { get; }

    /// <summary>
    /// Gets the grid region.
    /// </summary>
    public Region Region { get; }

    /// <summary>
    /// The line indexer.
    /// </summary>
    /// <param name="i">The index of the line.</param>
    public string this[int i] => Content[i];

    /// <summary>
    /// The character indexer.
    /// </summary>
    /// <param name="position">The position of the character.</param>
    public char this[Position position] => Content.At(position);

    /// <summary>
    /// Constructs a new instance of <see cref="CharacterGrid"/>.
    /// </summary>
    /// <param name="content">The content of the grid.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="content"/> is empty.</exception>
    public CharacterGrid(string[] content)
    {
        if (content.Length == 0 || content[0].Length == 0)
            throw new ArgumentOutOfRangeException(nameof(content));

        Content = content;
        Region = new Region(content.Length, content[0].Length);
    }

    /// <summary>
    /// Returns the neighbours at a specific position.
    /// </summary>
    /// <param name="position">The position to retrieve the neighbours.</param>
    /// <returns>The neighbours at the specific position.</returns>
    public virtual IReadOnlyList<Position> GetNeighboursAt(Position position)
    {
        return position.GetValidNeighbours(Region);
    }
}
