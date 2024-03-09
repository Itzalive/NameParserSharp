using System;
using System.Collections.Generic;
using System.Text;

namespace NameParser;

public static class EnumerableExtensions
{
    public static IEnumerable<(int First, int Last)> ConsecutiveRanges(this IEnumerable<int> source)
    {
        using (var e = source.GetEnumerator())
        {
            for (bool more = e.MoveNext(); more;)
            {
                int first = e.Current, last = first, next;
                while ((more = e.MoveNext()) && (next = e.Current) > last && next - last == 1)
                    last = next;
                yield return (first, last);
            }
        }
    }
}

public static class StringExtensions
{
    public static IEnumerable<ReadOnlyMemory<char>> SplitToSpan(this ReadOnlyMemory<char> value, char delimiter)
    {
        var last = -1;
        for (var i = 0; i < value.Length; i++)
        {
            if (value.Span[i] == delimiter && i > last + 1)
            {
                yield return value[(last + 1)..i];
                last = i;
            }
        }

        if (last < value.Length - 1)
            yield return value[(last + 1)..];
    }

    public static string RemoveCharacter(this ReadOnlyMemory<char> value, char character)
    {
        StringBuilder sb = new StringBuilder();
        ReadOnlySpan<char> span = value.Span;
        for (var i = 0; i < value.Length; i++)
        {
            if (span[i] != character)
            {
                sb.Append(character);
            }
        }
        return sb.ToString();
    }
}