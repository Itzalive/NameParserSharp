namespace NameParserSharp;

using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

/// <summary>
/// Parse a person's name into individual components.
/// Instantiation assigns to "fullName", and assignment to "fullName"
/// triggers parseFullName. After parsing the name, these instance 
/// attributes are available.
/// </summary>
public partial class HumanName {
    #region Properties

    /// <summary>
    /// Indicates whether any values were parsed out of the provided <see cref="FullName"/>
    /// </summary>
    public bool IsUnparsable { get; private set; }

    /// <summary>
    /// The full name without nickname
    /// </summary>
    public string FullName {
        get {
            return this.fullName;
        }
        private init {
            this.originalName = value;
            this.fullName = this.originalName;

            this.titleList      = [];
            this.firstList      = [];
            this.middleList     = [];
            this.lastList       = [];
            this.suffixList     = [];
            this.nicknameList   = [];
            this.lastBaseList   = [];
            this.lastPrefixList = [];

            if (!string.IsNullOrEmpty(value)) {
                this.ParseFullName();
            }
        }
    }

    public string Title => string.Join(" ", this.titleList.Select(p => p.String));

    public string First => string.Join(" ", this.firstList.Select(p => p.String));

    public string Middle => string.Join(" ", this.middleList.Select(p => p.String));

    public string Last => string.Join(" ", this.lastList.Select(p => p.String));

    public string Suffix => string.Join(", ", this.suffixList.Select(p => p.String));

    public string Nickname => string.Join(" ", this.nicknameList.Select(p => p.String));

    public string LastBase => string.Join(" ", this.lastBaseList.Select(p => p.String));

    public string LastPrefixes => string.Join(" ", this.lastPrefixList.Select(p => p.String));

    #endregion

    private string fullName;

    private readonly string originalName;

    private List<NamePiece> titleList;

    private List<NamePiece> firstList;

    private List<NamePiece> middleList;

    private List<NamePiece> lastList;

    private List<NamePiece> suffixList;

    private List<NamePiece> nicknameList;

    private List<NamePiece> lastBaseList;

    private List<NamePiece> lastPrefixList;

    private readonly Prefer preferences;

    public HumanName(string fullName, Prefer preferences = Prefer.Default) {
        this.preferences  = preferences;
        this.FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
    }

    public static bool operator ==(HumanName left, HumanName right) {
        if (ReferenceEquals(left, right)) {
            return true;
        }

        if (((object)left == null) || ((object)right == null)) {
            return false;
        }

        return left.Title == right.Title
               && left.First == right.First
               && left.Middle == right.Middle
               && left.Last == right.Last
               && left.Suffix == right.Suffix
               && (left.nicknameList.Count == 0 || right.nicknameList.Count == 0 || left.Nickname == right.Nickname);
    }

    public static bool operator !=(HumanName left, HumanName right) {
        return !(left == right);
    }

    /// <summary>
    /// Return the parsed name as a dictionary of its attributes.
    /// </summary>
    /// <param name="includeEmpty">Include keys in the dictionary for empty name attributes.</param>
    /// <returns></returns>
    public Dictionary<string, string> AsDictionary(bool includeEmpty = true) {
        var d = new Dictionary<string, string>();

        if (includeEmpty || this.titleList.Count != 0) {
            d["title"] = this.Title;
        }

        if (includeEmpty || this.firstList.Count != 0) {
            d["first"] = this.First;
        }

        if (includeEmpty || this.middleList.Count != 0) {
            d["middle"] = this.Middle;
        }

        if (includeEmpty || this.lastList.Count != 0) {
            d["last"] = this.Last;
        }

        if (includeEmpty || this.lastBaseList.Count != 0) {
            d["lastbase"] = this.LastBase;
        }

        if (includeEmpty || this.lastPrefixList.Count != 0) {
            d["lastprefixes"] = this.LastPrefixes;
        }

        if (includeEmpty || this.suffixList.Count != 0) {
            d["suffix"] = this.Suffix;
        }

        if (includeEmpty || this.nicknameList.Count != 0) {
            d["nickname"] = this.Nickname;
        }

        return d;
    }

    public override string ToString() {
        var output = $"{this.Title} {this.First} {this.Middle} {this.Last} {this.Suffix} ({this.Nickname})".Replace(" ()", "").Replace(" ''", "").Replace(" \"\"", "").Trim([',', ' ']);
        CollapseWhitespace(ref output);
        return output;
    }

    #region Parse helpers

    private static bool AreSuffixes(IEnumerable<NamePiece> pieces) {
        return pieces.Any() && pieces.All(p => p.IsSuffix());
    }

    /// <summary>
    /// Determines whether <see cref="piece"/> is a given name component as opposed to an affix, initial or title.
    /// </summary>
    /// <param name="piece">A single word from a name</param>
    /// <returns>False if <see cref="piece"/> is a prefix (de, abu, bin), suffix (jr, iv, cpa), title (mr, pope), or initial (x, e.); true otherwise</returns>
    private bool IsRootname(NamePiece piece) {
        return !piece.IsPrefix() && !piece.IsSuffix() && !piece.IsTitle() && !piece.IsAnInitial();
    }

    #endregion

    #region full name parser

    /// <summary>
    /// Collapse multiple spaces into single space
    /// </summary>
    private static void CollapseWhitespace(ref string value) {
        var sb = new StringBuilder(value.Length);
        var lastWhitespace = -1;
        var characters = value.AsSpan();
        for (var i = 0; i < characters.Length; i++) {
            if (char.IsWhiteSpace(characters[i])) {
                if (i - lastWhitespace > 1) {
                    if (characters[i] == ' ') {
                        sb.Append(characters[(lastWhitespace + 1)..(i + 1)]);
                    }
                    else {
                        sb.Append(characters[(lastWhitespace + 1)..i]);
                        sb.Append(' ');
                    }
                }

                lastWhitespace = i;
            }
        }

        sb.Append(characters[(lastWhitespace + 1)..]);
        value = sb.ToString().Trim(',');
    }

    /// <summary>
    /// If there are only two parts and one is a title, assume it's a last name
    /// instead of a first name. e.g. Mr. Johnson. Unless it's a special title
    /// like "Sir", then when it's followed by a single name that name is always
    /// a first name.
    /// </summary>
    private void PostProcessFirstnames() {
        if (this.titleList.Count != 0 && !FirstNameTitles.Contains(this.Title.ToLower()) && 1 == this.firstList.Count + this.lastList.Count) {
            if (this.firstList.Count != 0) {
                this.lastList      = this.firstList;
                this.firstList = [];
            }
            else {
                this.firstList = this.lastList;
                this.lastList  = [];
            }
        }
    }

    /// <summary>
    /// Parse out the last name components into prefixes and a base last name
    /// in order to allow sorting. Prefixes are those in <see cref="Prefixes"/>,
    /// start off <see cref="Last"/> and are contiguous. See <seealso cref="https://en.wikipedia.org/wiki/Tussenvoegsel"/>
    /// </summary>
    private void PostProcessLastname() {
        // parse out 'words' from the last name
        var words = this.lastList.SelectMany(part => part.Span.SplitToSpan(' ')).Select(part => new NamePiece(part)).ToArray();

        var prefixCount = 0;
        while (prefixCount < words.Length && words[prefixCount].IsPrefix()) {
            prefixCount++;
        }

        if (this.preferences.HasFlag(Prefer.FirstOverPrefix) && this.firstList.Count == 0 && prefixCount == 1 && words.Length > 1) {
            this.firstList = words.Take(1).ToList();

            this.lastList = words.Skip(1).ToList();
        }
        else {
            this.lastPrefixList = words.Take(prefixCount).ToList();
        }

        this.lastBaseList = words.Skip(prefixCount).ToList();
    }

    /// <summary>
    /// The main parse method for the parser. This method is run upon assignment to the
    /// fullName attribute or instantiation.
    /// 
    /// Basic flow is to hand off to `pre_process` to handle nicknames. It
    /// then splits on commas and chooses a code path depending on the number of commas.
    /// `parsePieces` then splits those parts on spaces and
    /// `joinOnConjunctions` joins any pieces next to conjunctions. 
    /// </summary>
    private void ParseFullName() {
        // Pre-process
        this.FixPhd();
        this.ParseNicknames();

        CollapseWhitespace(ref this.fullName);

        // break up fullName by commas
        var parts = this.fullName.AsMemory().SplitToSpan(',').Select(part => new NamePiece(part.Trim())).ToList();

        if (parts.Count == 0) {
            // Edge case where the input was all in parens and has become the nickname
            // See https://github.com/aeshirey/NameParserSharp/issues/8
        }
        else if (parts.Count == 1) {
            // no commas, title first middle middle middle last suffix
            //            part[0]

            var pieces = this.ParsePieces(parts);

            for (var i = 0; i < pieces.Count; i++) {
                var piece = pieces[i];
                var nxt = i == pieces.Count - 1 ? null : pieces[i + 1];

                // title must have a next piece, unless it's just a title
                if (this.firstList.Count == 0) {
                    if ((nxt != null || pieces.Count == 1) && piece.IsTitle()) {
                        this.titleList.Add(piece);
                    }
                    else if (pieces.Count == 1 && this.nicknameList.Count != 0) {
                        this.lastList.Add(piece);
                    }
                    else {
                        this.firstList.Add(piece);
                    }
                }
                else {
                    bool isAnInitial = piece.IsAnInitial();
                    if (AreSuffixes(pieces.Skip(i + 1)) || (nxt != null && nxt.IsRomanNumeral() && i == (pieces.Count - 2) && !isAnInitial)) {
                        this.lastList.Add(piece);
                        this.suffixList.AddRange(pieces.Skip(i + 1));
                        break;
                    }
                    else if (nxt != null) {
                        // another component exists, so this is likely a middle name
                        this.middleList.Add(piece);
                    }
                    else {
                        this.lastList.Add(piece);
                    }
                }
            }
        }
        else {
            var spaceSplitPartZero = parts[0].Span.SplitToSpan(' ').Select(p => new NamePiece(p)).ToList();
            var spaceSplitPartOne = parts[1].Span.SplitToSpan(' ').Select(p => new NamePiece(p)).ToList();
            var piecesPartOne = this.ParsePieces(spaceSplitPartOne, 1);
            if (spaceSplitPartZero.Count > 1 && AreSuffixes(piecesPartOne)) {
                // suffix comma: title first middle last [suffix], suffix [suffix] [, suffix]
                //               parts[0],                         parts[1:...]
                this.suffixList.AddRange(parts.Skip(1));
                var pieces = this.ParsePieces(spaceSplitPartZero);
                for (var i = 0; i < pieces.Count; i++) {
                    var piece = pieces[i];
                    var nxt = i == pieces.Count - 1 ? null : pieces[i + 1];

                    if (this.firstList.Count == 0) {
                        if ((nxt != null || pieces.Count == 1) && piece.IsTitle()) {
                            this.titleList.Add(piece);
                            continue;
                        }

                        this.firstList.Add(piece);
                        continue;
                    }

                    if (AreSuffixes(pieces.Skip(i + 1))) {
                        this.lastList.Add(piece);
                        this.suffixList.InsertRange(0, pieces.Skip(i + 1));
                        break;
                    }

                    // correct for when we have "John D" with an AdditionalName={Catherine, T, MacArthur}.
                    // We should not see this as being First=John, Last=D; rather, First=John, Middle=D, Last=<AdditionalName.Last>
                    if (nxt != null) {
                        // another component exists, so this is likely a middle name
                        this.middleList.Add(piece);
                    }
                    else {
                        this.lastList.Add(piece);
                    }
                }
            }
            else {
                // lastname comma: last [suffix], title first middles[,] suffix [,suffix]
                //                 parts[0],      parts[1],              parts[2:...]
                var pieces = piecesPartOne;

                // lastname part may have suffixes in it
                var lastnamePieces = this.ParsePieces(spaceSplitPartZero, 1);

                foreach (var piece in lastnamePieces) {
                    // the first one is always a last name, even if it looks like a suffix
                    if (this.lastList.Count != 0 && piece.IsSuffix()) {
                        this.suffixList.Add(piece);
                    }
                    else {
                        this.lastList.Add(piece);
                    }
                }

                for (var i = 0; i < pieces.Count; i++) {
                    var piece = pieces[i];
                    var nxt = i == pieces.Count - 1 ? null : pieces[i + 1];

                    if (this.firstList.Count == 0) {
                        if ((nxt != null || pieces.Count == 1) && piece.IsTitle()) {
                            this.titleList.Add(piece);
                        }
                        else {
                            this.firstList.Add(piece);
                        }
                    }
                    else if (piece.IsSuffix()) {
                        this.suffixList.Add(piece);
                    }
                    else {
                        this.middleList.Add(piece);
                    }
                }

                if (parts.Count >= 3 && parts.Skip(2).Any()) {
                    this.suffixList.AddRange(parts.Skip(2));
                }
            }
        }

        this.IsUnparsable = this.titleList.Count == 0 && this.firstList.Count == 0 && this.middleList.Count == 0 && this.lastList.Count == 0 && this.suffixList.Count == 0 && this.nicknameList.Count == 0;

        this.PostProcessFirstnames();
        this.PostProcessLastname();
    }

    private void FixPhd() {
        var match = RegexPhd.Match(this.fullName);
        if (match.Success) {
            this.suffixList.Add(new NamePiece(match.Groups[1].Value));
            this.fullName = RegexPhd.Replace(this.fullName, string.Empty);
        }
    }

    private void ParseNicknames() {
        var fullNameSpan = this.fullName.AsSpan();
#if NET8_0_OR_GREATER
        if (!fullNameSpan.ContainsAny('(', '\'', '\"'))
#else
        if (!fullNameSpan.Contains('(') && !fullNameSpan.Contains('\'') && !fullNameSpan.Contains('"'))
#endif
        {
            return;
        }

        foreach (var regex in new[] { RegexQuotedWord, RegexDoubleQuotes, RegexParenthesis }) {
            var match = regex.Match(this.fullName);
            while (match.Success && match.Groups[0].Value.Length > 0) {
                // remove from the full name the nickname plus its identifying boundary (parens or quotes)
                this.fullName = this.fullName.Replace(match.Value, string.Empty);

                // keep only the nickname part
                this.nicknameList.Add(new NamePiece(match.Groups[1].Value));

                match = regex.Match(this.fullName);
            }
        }
    }

    /// <summary>
    /// Split parts on spaces and remove commas, join on conjunctions and lastname prefixes.
    /// </summary>
    /// <param name="parts">name part strings from the comma split</param>
    /// <param name="additionalPartsCount"></param>
    /// <returns>pieces split on spaces and joined on conjunctions</returns>
    private List<NamePiece> ParsePieces(List<NamePiece> parts, int additionalPartsCount = 0) {
        var output = new List<NamePiece>();
        foreach (var part in parts) {
            output.AddRange(part.Span.SplitToSpan(' ').Select(s => new NamePiece(s.Trim(','))));
        }

        // If part contains periods, check if it's multiple titles or suffixes
        // together without spaces if so, add the new part with periods to the
        // constants so that they get parsed correctly later
        foreach (var part in output) {
            part.CheckForDot();
        }

        return this.joinOnConjunctions(output, additionalPartsCount);
    }

    /// <summary>
    /// Join conjunctions to surrounding pieces. Title- and prefix-aware. e.g.:
    ///
    ///     ['Mr.', 'and'. 'Mrs.', 'John', 'Doe'] ==>
    ///                     ['Mr. and Mrs.', 'John', 'Doe']
    ///
    ///     ['The', 'Secretary', 'of', 'State', 'Hillary', 'Clinton'] ==>
    ///                     ['The Secretary of State', 'Hillary', 'Clinton']
    /// </summary>
    /// <remarks>
    /// When joining titles, saves newly formed piece to the instance's titles
    /// constant so that they will be parsed correctly later. E.g. after parsing the
    /// example names above, 'The Secretary of State' and 'Mr. and Mrs.' would
    /// be present in the titles constant set.
    /// </remarks>
    /// <param name="pieces">name pieces strings after split on spaces</param>
    /// <param name="additionalPartsCount"></param>
    /// <returns>new list with piece next to conjunctions merged into one piece with spaces in it.</returns>
    internal List<NamePiece> joinOnConjunctions(List<NamePiece> pieces, int additionalPartsCount = 0) {
        var length = pieces.Count + additionalPartsCount;

        // don't join on conjunctions if there are only 2 parts
        if (length < 3)
            return pieces;

        var rootnamePieces = pieces.Where(this.IsRootname).ToArray();
        var totalLength = rootnamePieces.Length + additionalPartsCount;

        // find all the conjunctions, join any conjunctions that are next to each
        // other, then join those newly joined conjunctions and any single
        // conjunctions to the piece before and after it
        var conjunctionIndexes = new List<int>();
        for (var i = 0; i < pieces.Count; i++) {
            if (pieces[i].IsConjunction())
                conjunctionIndexes.Add(i);
        }

        var contiguousConjunctionIndexRanges = conjunctionIndexes.ConsecutiveRanges();

        var toDelete = new List<int>(length);
        foreach (var (first, last) in contiguousConjunctionIndexRanges) {
            if (first == last) continue;
            var newPiece = new NamePiece(string.Join(" ", pieces.Skip(first).Take(last - first + 1).Select(p => p.Span)), isConjunction: true);
            toDelete.AddRange(Enumerable.Range(first + 1, last - first));
            pieces[first] = newPiece;
        }

        toDelete.Reverse();

        foreach (var i in toDelete) {
            pieces.RemoveAt(i);
        }

        if (pieces.Count == 1) {
            return pieces;
        }

        conjunctionIndexes = [];
        for (var i = 0; i < pieces.Count; i++)
            if (pieces[i].IsConjunction())
                conjunctionIndexes.Add(i);

        for (var n = 0; n < conjunctionIndexes.Count; n++) {
            var i = conjunctionIndexes[n];
            // loop through the pieces backwards, starting at the end of the list.
            // Join conjunctions to the pieces on either side of them.
            if (pieces[i].Span.Length == 1 && totalLength < 4) {
                // if there are only 3 total parts (minus known titles, suffixes and prefixes) 
                // and this conjunction is a single letter, prefer treating it as an initial
                // rather than a conjunction.
                // http://code.google.com/p/python-nameparser/issues/detail?id=11
                continue;
            }

            if (i == 0) {
                var newPiece = new NamePiece(string.Join(" ", pieces.Skip(i).Take(2).Select(p => p.Span)));
                if (pieces[i + 1].IsTitle()) {
                    newPiece.OverrideIsTitle();
                }

                pieces[i] = newPiece;
                pieces.RemoveAt(i + 1);

                for (var j = n + 1; j < conjunctionIndexes.Count; j++) {
                    conjunctionIndexes[j] -= 1;
                }
            }
            else {
                var newPiece = new NamePiece(string.Join(" ", pieces.Skip(i - 1).Take(3).Select(p => p.Span)));
                if (pieces[i - 1].IsTitle()) {
                    newPiece.OverrideIsTitle();
                }

                pieces[i - 1] = newPiece;
                pieces.RemoveAt(i);
                var removedCount = 1;
                if (pieces.Count > i) {
                    pieces.RemoveAt(i);
                    removedCount++;
                }

                for (var j = n + 1; j < conjunctionIndexes.Count; j++) {
                    conjunctionIndexes[j] -= removedCount;
                }
            }
        }

        // join prefixes to following lastnames: ['de la Vega'], ['van Buren']
        // skip first part to avoid counting it as a prefix, e.g. "van" is either a first name or a preposition depending on its position
        var prefixes = pieces.Where(p => p.IsPrefix()).ToArray();
        if (prefixes.Length > 0) {
            var i = 0;
            foreach (var prefix in prefixes) {
                var newI = pieces.IndexOf(prefix);
                if (newI != -1) i = newI;

                // If it's the first piece and there are more than 1 rootnames, assume it's a first name
                if (i == 0 && totalLength >= 1) continue;

                var matchPrefix = false;
                for (var j = i + 1; j < pieces.Count; j++) {
                    if (!pieces[j].IsPrefix()) {
                        continue;
                    }

                    matchPrefix = true;
                    if (j == i + 1) {
                        j++;
                    }

                    var newPiece = new NamePiece(string.Join(" ", pieces.Skip(i).Take(j - i).Select(p => p.Span)));
                    pieces = pieces.Take(i).Concat([newPiece]).Concat(pieces.Skip(j)).ToList();
                    break;
                }

                if (!matchPrefix) {
                    // join everything after the prefix until the next suffix
                    var nextSuffix = pieces.Skip(i).Where(p => p.IsSuffix()).ToArray();

                    if (nextSuffix.Length > 0) {
                        var j = pieces.IndexOf(nextSuffix[0]);
                        var newPiece = new NamePiece(string.Join(" ", pieces.Skip(i).Take(j - i).Select(p => p.Span)));

                        pieces = pieces.Take(i).Concat([newPiece]).Concat(pieces.Skip(j)).ToList();
                    }
                    else {
                        var newPiece = new NamePiece(string.Join(" ", pieces.Skip(i).Select(p => p.Span)));
                        pieces = pieces.Take(i).Concat([newPiece]).ToList();
                    }
                }
            }
        }

        return pieces;
    }

    #endregion

    #region Capitalization Support

    /// <summary>
    /// Capitalize a single word in a context-sensitive manner. Values such as "and", "der" and "bin" are unmodified, but "smith" -> "Smith", and "phd" -> "Ph.D."
    /// </summary>
    private static string CapitalizeWord(string word, string attribute) {
        if ((new NamePiece(word).IsPrefix() && attribute is "last" or "middle") || new NamePiece(word).IsConjunction())
        {
            return word;
        }

        // "phd" => "Ph.D."; "ii" => "II"
        var exception = CapitalizationExceptions.FirstOrDefault(tup => tup.Item1 == word);

        if (exception != null) {
            return exception.Item2;
        }

        if (RegexMac.IsMatch(word))
            // special case: "macbeth" should be "MacBeth"; "mcbride" -> "McBride"
            return RegexMac.Replace(word, m => string.Concat(ToTitleCase(m.Groups[1].Value), ToTitleCase(m.Groups[2].Value)));

        return ToTitleCase(word);
    }

    private static string ToTitleCase(string s) {
        if (s.Length == 0) {
            return string.Empty;
        }

        return char.ToUpper(s[0]) + s[1..];
    }

    private static string CapitalizePiece(NamePiece piece, string attribute) {
        return RegexWord.Replace(piece.LowerString, m => CapitalizeWord(m.Value, attribute).ToString());
    }

    /// <summary>
    /// Attempt to normalize the input values in a human-readable way.
    /// By default, it will not adjust the case of names entered in mixed case.
    /// To run capitalization on all names pass the parameter `force=True`.
    /// For example, "juan de garcia" would normalize to "Juan de Garcia"
    /// </summary>
    public void Normalize(bool? force = null) {
        var name = this.FullName;
        if (force != true && !(name == name.ToUpper() || name == name.ToLower()))
            return;

        this.titleList  = this.titleList.Select(t => new NamePiece(CapitalizePiece(t, "title"))).ToList();
        this.firstList  = this.firstList.Select(t => new NamePiece(CapitalizePiece(t, "first"))).ToList();
        this.middleList = this.middleList.Select(t => new NamePiece(CapitalizePiece(t, "middle"))).ToList();
        this.lastList = this.lastList.Select(t => new NamePiece(CapitalizePiece(t, "last")))
                            .ToList(); // CapitalizePiece recognizes prefixes, so its okay to normalize "van der waals" like this
        this.suffixList   = this.suffixList.Select(t => new NamePiece(CapitalizePiece(t, "suffix"))).ToList();
        this.nicknameList = this.nicknameList.Select(t => new NamePiece(CapitalizePiece(t, "nickname"))).ToList();
        this.lastBaseList = this.lastBaseList.Select(t => new NamePiece(CapitalizePiece(t, "last"))).ToList();
        // normalizing _LastPrefixList would effectively be a no-op, so don't bother calling it
    }

    #endregion
}