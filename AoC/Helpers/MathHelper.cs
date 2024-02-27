using System.Runtime.CompilerServices;

namespace AoC.Helpers;

/// <summary>
/// Helper methods for math.
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// Calculates the greatest common divisor (GCD) of 2 numbers.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <returns>The greatest common divisor (GCD) of 2 numbers.</returns>
    public static long ComputeGcd(long a, long b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);

        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    /// <summary>
    /// Calculates the greatest common divisor (GCD) on a list of numbers.
    /// </summary>
    /// <param name="numbers">The list of numbers to calculate LCM.</param>
    /// <returns>The greatest common divisor (GCD) on a list of numbers.</returns>
    public static long ComputeGcd(IReadOnlyList<long> numbers)
    {
        return RunListOperation(numbers, ComputeGcd);
    }

    /// <summary>
    /// Calculates the least common multiple (LCM) of 2 numbers.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <returns>The least common multiple (LCM) of 2 numbers.</returns>
    public static long ComputeLcm(long a, long b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        return a / ComputeGcd(a, b) * b;
    }

    /// <summary>
    /// Calculates the least common multiple (LCM) on a list of numbers.
    /// </summary>
    /// <param name="numbers">The list of numbers to calculate LCM.</param>
    /// <returns>The least common multiple (LCM) on a list of numbers.</returns>
    public static long ComputeLcm(IReadOnlyList<long> numbers)
    {
        return RunListOperation(numbers, ComputeLcm);
    }

    /// <summary>
    /// Computes the MOD of 2 numbers.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <returns>The MOD of 2 numbers.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Mod(long a, long b)
    {
        return (a % b + b) % b;
    }

    /// <summary>
    /// Runs a list operation.
    /// </summary>
    /// <param name="values">The list of values.</param>
    /// <param name="operation">The operation to run.</param>
    /// <typeparam name="T">The type of values.</typeparam>
    /// <returns>The result of the list operation.</returns>
    private static T RunListOperation<T>(IReadOnlyList<T> values, Func<T, T, T> operation) where T : struct
    {
        if (values.Count == 0)
            return default;
        if (values.Count == 1)
            return values[0];

        var result = operation(values[0], values[1]);
        for (var i = 2; i < values.Count; i++)
            result = operation(result, values[i]);
        return result;
    }
}
