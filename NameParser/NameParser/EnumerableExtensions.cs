using System;
using System.Collections.Generic;

namespace NameParser {
    internal static class EnumerableExtensions {
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
        public static LineSplitEnumerator SplitToSpan(this string str, char delimiter)
        {
            // LineSplitEnumerator is a struct so there is no allocation here
            return new LineSplitEnumerator(str.AsSpan(), delimiter);
        }

        // Must be a ref struct as it contains a ReadOnlySpan<char>
        public ref struct LineSplitEnumerator
        {
            private ReadOnlySpan<char> str;
            private readonly char delimiter;

            public LineSplitEnumerator(ReadOnlySpan<char> str, char delimiter)
            {
                this.str = str;
                this.delimiter = delimiter;
                Current = default;
            }

            // Needed to be compatible with the foreach operator
            public LineSplitEnumerator GetEnumerator() => this;

            public bool MoveNext()
            {
                var span = str;
                if (span.Length == 0) // Reach the end of the string
                    return false;

                var index = span.IndexOf(delimiter);
                if (index == -1) // The string is composed of only one line
                {
                    str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
                    Current = span;
                    return true;
                }

                Current = span[..index];
                str = span[(index + 1)..];
                return true;
            }

            public ReadOnlySpan<char> Current { get; private set; }
        }
    }
}
