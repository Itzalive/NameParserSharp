namespace NameParser.Benchmarks.SpanCached2
{
    public class Piece(ReadOnlyMemory<char> span)
    {
        private string? stringValue;
        private string? lowerString;
        private string? trimmedString;

        public Piece(string value)
            :this(value.AsMemory())
        {
            stringValue = value;
        }

        public ReadOnlyMemory<char> Span { get; } = span;

        public string String => stringValue ??= Span.ToString();

        public string LowerString => lowerString ??= String.ToLower();

        public string TrimmedLowerString => trimmedString ??= LowerString.Trim('.');
    }
}