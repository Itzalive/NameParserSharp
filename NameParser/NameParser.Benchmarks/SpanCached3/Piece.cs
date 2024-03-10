namespace NameParser.Benchmarks.SpanCached3
{
    public partial class Piece(ReadOnlyMemory<char> span)
    {
        private string? stringValue;
        private string? lowerString;
        private string? trimmedString;
        private bool? isTitle;
        private bool? isConjunction;
        private bool? isPrefix;
        private bool? isSuffix;

        public Piece(string value, bool? isTitle = null, bool? isConjunction = null, bool? isSuffix = null)
            : this(value.AsMemory())
        {
            this.stringValue = value;
            this.isTitle = isTitle;
            this.isConjunction = isConjunction;
            this.isSuffix = isSuffix;
        }

        public ReadOnlyMemory<char> Span { get; } = span;

        public string String => stringValue ??= Span.ToString();

        public string LowerString => lowerString ??= String.ToLower();

        public string TrimmedLowerString => trimmedString ??= LowerString.Trim('.');


        public bool IsTitle()
        {
            return isTitle ??= Titles.Contains(this.TrimmedLowerString);
        }

        public bool IsConjunction()
        {
            return isConjunction ??= (Conjunctions.Contains(this.LowerString) && !IsAnInitial());
        }

        public bool IsPrefix()
        {
            return Prefixes.Contains(this.TrimmedLowerString);
        }

        public bool IsRomanNumeral()
        {
            return RomanNumerals.Contains(this.LowerString);
        }

        public bool IsSuffix()
        {
            return isSuffix ??= ((Suffixes.Contains(this.TrimmedLowerString.Replace(".", string.Empty)) ||
                    SuffixesNotAcronyms.Contains(this.TrimmedLowerString)) && !IsAnInitial());
        }


        /// <summary>
        /// Words with a single period at the end, or a single uppercase letter.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if <see cref="value"/> matches the regex "^(\w\.|[A-Z])?$"</returns>
        public bool IsAnInitial()
        {
            return (Span.Length == 2 && Span.Span[1] == '.' && char.IsLetter(Span.Span[0])) || (Span.Length == 1 && char.IsUpper(Span.Span[0]));
        }

        public void CheckForDot()
        {

            // if this part has a period not at the beginning or end
            if (this.Span.Length > 3 && this.Span.Span[1..^2].Contains('.'))
            {
                // split on periods, any of the split pieces titles or suffixes?
                // ("Lt.Gov.")
                var periodChunks = this.Span.SplitToSpan('.');

                // add the part to the constant so it will be found
                if (periodChunks.Any(p => new Piece(p).IsTitle()))
                {
                    this.isTitle = true;
                }
                else if (periodChunks.Any(p => new Piece(p).IsSuffix()))
                {
                    this.isSuffix = true;
                }
            }
        }

        internal void OverrideIsTitle()
        {
            isTitle = true;
        }
    }
}