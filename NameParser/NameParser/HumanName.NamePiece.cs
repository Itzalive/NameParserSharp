namespace NameParser;

using System;
using System.Linq;

public partial class HumanName {
    internal partial class NamePiece(ReadOnlyMemory<char> span) {
        private string? stringValue;

        private string? lowerString;

        private string? trimmedString;

        private bool? isTitle;

        private bool? isConjunction;

        private bool? isPrefix;

        private bool? isSuffix;

        public NamePiece(string value, bool? isTitle = null, bool? isConjunction = null, bool? isSuffix = null)
            : this(value.AsMemory()) {
            this.stringValue   = value;
            this.isTitle       = isTitle;
            this.isConjunction = isConjunction;
            this.isSuffix      = isSuffix;
        }

        public ReadOnlyMemory<char> Span { get; } = span;

        public string String => this.stringValue ??= this.Span.ToString();

        public string LowerString => this.lowerString ??= this.String.ToLower();

        public string TrimmedLowerString => this.trimmedString ??= this.LowerString.Trim('.');

        public bool IsTitle() {
            return this.isTitle ??= Titles.Contains(this.TrimmedLowerString);
        }

        public bool IsConjunction() {
            return this.isConjunction ??= (Conjunctions.Contains(this.LowerString) && !this.IsAnInitial());
        }

        public bool IsPrefix() {
            return Prefixes.Contains(this.TrimmedLowerString);
        }

        public bool IsRomanNumeral() {
            return RomanNumerals.Contains(this.LowerString);
        }

        public bool IsSuffix() {
            return this.isSuffix ??= ((Suffixes.Contains(this.TrimmedLowerString.Replace(".", string.Empty)) || SuffixesNotAcronyms.Contains(this.TrimmedLowerString))
                                      && !this.IsAnInitial());
        }

        /// <summary>
        /// Words with a single period at the end, or a single uppercase letter.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if <see cref="value"/> matches the regex "^(\w\.|[A-Z])?$"</returns>
        public bool IsAnInitial() {
            return (this.Span.Length == 2 && this.Span.Span[1] == '.' && char.IsLetter(this.Span.Span[0])) || (this.Span.Length == 1 && char.IsUpper(this.Span.Span[0]));
        }

        public void CheckForDot() {
            // if this part has a period not at the beginning or end
            if (this.Span.Length > 3 && this.Span.Span[1..^2].Contains('.')) {
                // split on periods, any of the split pieces titles or suffixes?
                // ("Lt.Gov.")
                var periodChunks = this.Span.SplitToSpan('.').Select(p => new NamePiece(p));

                // add the part to the constant so it will be found
                if (periodChunks.Any(p => p.IsTitle())) {
                    this.isTitle = true;
                }
                else if (periodChunks.Any(p => p.IsSuffix())) {
                    this.isSuffix = true;
                }
            }
        }

        internal void OverrideIsTitle() {
            this.isTitle = true;
        }
    }
}