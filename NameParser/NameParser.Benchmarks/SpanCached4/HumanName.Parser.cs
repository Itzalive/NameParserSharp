using System.Linq;
using System.Text;

namespace NameParser.Benchmarks.SpanCached4;

/// <summary>
/// Parse a person's name into individual components.
/// Instantiation assigns to "fullName", and assignment to "fullName"
/// triggers parseFullName. After parsing the name, these instance 
/// attributes are available.
/// </summary>
public partial class HumanName
{
    #region Properties

    /// <summary>
    /// Indicates whether any values were parsed out of the provided <see cref="FullName"/>
    /// </summary>
    public bool IsUnparsable { get; private set; }

    public static bool ParseMultipleNames { get; set; }

    /// <summary>
    /// The full name without nickname
    /// </summary>
    public string FullName
    {
        get { return _FullName; }
        private set
        {
            _OriginalName = value;
            _FullName = _OriginalName;

            _TitleList = [];
            _FirstList = [];
            _MiddleList = [];
            _LastList = [];
            _SuffixList = [];
            _NicknameList = [];
            _LastBaseList = [];
            _LastPrefixList = [];

            if (!string.IsNullOrEmpty(value))
            {
                ParseFullName();
            }
        }
    }

    public string Title => string.Join(" ", _TitleList.Select(p => p.String));

    public string First => string.Join(" ", _FirstList.Select(p => p.String));

    public string Middle => string.Join(" ", _MiddleList.Select(p => p.String));

    public string Last => string.Join(" ", _LastList.Select(p => p.String));

    public string Suffix => string.Join(", ", _SuffixList.Select(p => p.String));

    public string Nickname => string.Join(" ", _NicknameList.Select(p => p.String));

    /// <summary>
    /// If <see cref="ParseMultipleNames"/> is true and the input contains "&" or "and", the additional
    /// name will be parsed out and put into a second <see cref="HumanName"/> record. For example,
    /// "John D. and Catherine T. MacArthur" should be parsed as {John, D, MacArthur} with an AdditionalName
    /// set to the parsed value {Catherine, T, MacAthur}.
    /// </summary>
    public HumanName? AdditionalName { get; private set; }

    public string LastBase => string.Join(" ", _LastBaseList.Select(p => p.String));
    public string LastPrefixes => string.Join(" ", _LastPrefixList.Select(p => p.String));

    #endregion

    private string _FullName, _OriginalName;

    private List<Piece> _TitleList;
    private List<Piece> _FirstList;
    private List<Piece> _MiddleList;
    private List<Piece> _LastList;
    private List<Piece> _SuffixList;
    private List<Piece> _NicknameList;
    private List<Piece> _LastBaseList;
    private List<Piece> _LastPrefixList;
    private Prefer prefs;

    public HumanName(string fullName, Prefer prefs = Prefer.Default)
    {
        if (fullName == null)
        {
            throw new ArgumentNullException("fullName");
        }

        this.prefs = prefs;

        FullName = fullName;
    }

    public static bool operator ==(HumanName left, HumanName right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (((object)left == null) || ((object)right == null))
        {
            return false;
        }

        return left.Title == right.Title
               && left.First == right.First
               && left.Middle == right.Middle
               && left.Last == right.Last
               && left.Suffix == right.Suffix
               &&
               (!left._NicknameList.Any() || !right._NicknameList.Any() ||
                left.Nickname == right.Nickname);
    }

    public static bool operator !=(HumanName left, HumanName right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Return the parsed name as a dictionary of its attributes.
    /// </summary>
    /// <param name="includeEmpty">Include keys in the dictionary for empty name attributes.</param>
    /// <returns></returns>
    public Dictionary<string, string> AsDictionary(bool includeEmpty = true)
    {
        var d = new Dictionary<string, string>();

        if (includeEmpty || _TitleList.Any())
        {
            d["title"] = Title;
        }

        if (includeEmpty || _FirstList.Any())
        {
            d["first"] = First;
        }

        if (includeEmpty || _MiddleList.Any())
        {
            d["middle"] = Middle;
        }

        if (includeEmpty || _LastList.Any())
        {
            d["last"] = Last;
        }

        if (includeEmpty || _LastBaseList.Any())
        {
            d["lastbase"] = LastBase;
        }

        if (includeEmpty || _LastPrefixList.Any())
        {
            d["lastprefixes"] = LastPrefixes;
        }

        if (includeEmpty || _SuffixList.Any())
        {
            d["suffix"] = Suffix;
        }

        if (includeEmpty || _NicknameList.Any())
        {
            d["nickname"] = Nickname;
        }

        return d;
    }

    public override string ToString()
    {
        var output = $"{Title} {First} {Middle} {Last} {Suffix} ({Nickname})".Replace(" ()", "").Replace(" ''", "").Replace(" \"\"", "").Trim(
            [',', ' ']);
        CollapseWhitespace(ref output);
        return output;
    }

    #region Parse helpers


    private bool AreSuffixes(IEnumerable<Piece> pieces)
    {
        return pieces.Any() && pieces.All(p => p.IsSuffix());
    }

    /// <summary>
    /// Determines whether <see cref="piece"/> is a given name component as opposed to an affix, initial or title.
    /// </summary>
    /// <param name="piece">A single word from a name</param>
    /// <returns>False if <see cref="piece"/> is a prefix (de, abu, bin), suffix (jr, iv, cpa), title (mr, pope), or initial (x, e.); true otherwise</returns>
    private bool IsRootname(Piece piece)
    {
        return !piece.IsPrefix()
               && !piece.IsSuffix()
               && !piece.IsTitle()
               && !piece.IsAnInitial();
    }

    #endregion

    #region full name parser

    /// <summary>
    /// Collapse multiple spaces into single space
    /// </summary>
    private static void CollapseWhitespace(ref string value)
    {
        StringBuilder sb = new StringBuilder(value.Length);
        var lastWhitespace = -1;
        ReadOnlySpan<char> characters = value.AsSpan();
        for (var i = 0; i < characters.Length; i++)
        {
            if (char.IsWhiteSpace(characters[i]))
            {
                if (i - lastWhitespace > 1)
                {
                    if (characters[i] == ' ')
                    {
                        sb.Append(characters[(lastWhitespace + 1)..(i + 1)]);
                    }
                    else
                    {
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
    private void PostProcessFirstnames()
    {
        if (_TitleList.Any()
            && !FirstNameTitles.Contains(Title.ToLower())
            && 1 == _FirstList.Count + _LastList.Count)
        {
            if (_FirstList.Any())
            {
                _LastList = _FirstList;
                _FirstList = [];
            }
            else
            {
                _FirstList = _LastList;
                _LastList = [];
            }
        }
    }

    /// <summary>
    /// Parse out the last name components into prefixes and a base last name
    /// in order to allow sorting. Prefixes are those in <see cref="Prefixes"/>,
    /// start off <see cref="Last"/> and are contiguous. See <seealso cref="https://en.wikipedia.org/wiki/Tussenvoegsel"/>
    /// </summary>
    private void PostProcessLastname()
    {
        // parse out 'words' from the last name
        var words = _LastList
            .SelectMany(part => part.Span.SplitToSpan(' '))
            .Select(part => new Piece(part))
            .ToArray();

        var prefixCount = 0;
        while (prefixCount < words.Length && words[prefixCount].IsPrefix())
        {
            prefixCount++;
        }

        if (this.prefs.HasFlag(Prefer.FirstOverPrefix)
            && this._FirstList.Count == 0
            && prefixCount == 1
            && words.Length > 1)
        {
            _FirstList = words.Take(1).ToList();

            _LastList = words.Skip(1).ToList();
        }
        else
        {
            _LastPrefixList = words.Take(prefixCount).ToList();
        }

        _LastBaseList = words.Skip(prefixCount).ToList();
    }

    private void PostProcessAdditionalName()
    {
        if (!ParseMultipleNames || AdditionalName == null)
        {
            return;
        }

        // Often, the secondary in a pair of names will contain the last name but not the primary.
        // (eg, John D. and Catherine T. MacArthur). In this case, we should be able to infer
        // the primary's last name from the secondary.
        if (!_LastList.Any())
        {
            _LastList = AdditionalName._LastList;
        }
        else
        {
            // for names like "Smith, John And Jane", we'd have to propagate the name backward (possibly through multiple names)
            var next = AdditionalName;
            while (next != null && !next._LastList.Any())
            {
                next._LastList = _LastList;
                next = next.AdditionalName;
            }
        }
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
    private void ParseFullName()
    {
        if (ParseMultipleNames)
        {
            if (_FullName.Contains('&'))
            {
                var split = _FullName.IndexOf('&');

                var primary = _FullName[..split];

                var secondary = _FullName[(split + 1)..];
                AdditionalName = new HumanName(secondary);

                _FullName = primary;
            }
            else if (_FullName.ToLowerInvariant().Contains(" and "))
            {
                var split = _FullName.IndexOf(" and ", StringComparison.InvariantCultureIgnoreCase);

                var primary = _FullName[..split];

                var secondary = _FullName[(split + 5)..];
                AdditionalName = new HumanName(secondary);

                _FullName = primary;
            }
        }

        // Pre-process
        FixPhd(ref _FullName);
        ParseNicknames(ref _FullName, out _NicknameList);

        CollapseWhitespace(ref _FullName);

        // break up fullName by commas
        var parts = _FullName
            .AsMemory().SplitToSpan(',')
            .Select(part => new Piece(part.Trim()))
            .ToList();

        if (parts.Count == 0)
        {
            // Edge case where the input was all in parens and has become the nickname
            // See https://github.com/aeshirey/NameParserSharp/issues/8
        }
        else if (parts.Count == 1)
        {
            // no commas, title first middle middle middle last suffix
            //            part[0]

            var pieces = ParsePieces(parts);

            for (var i = 0; i < pieces.Count; i++)
            {
                var piece = pieces[i];
                var nxt = i == pieces.Count - 1 ? null : pieces[i + 1];

                // title must have a next piece, unless it's just a title
                if (!this._FirstList.Any())
                {
                    if ((nxt != null || pieces.Count == 1) && piece.IsTitle())
                    {
                        _TitleList.Add(piece);
                    }
                    else if (pieces.Count == 1 && this._NicknameList.Any())
                    {
                        _LastList.Add(piece);
                    }
                    else
                    {
                        _FirstList.Add(piece);
                    }
                }
                else
                {
                    bool isAnInitial = piece.IsAnInitial();
                    if (AreSuffixes(pieces.Skip(i + 1)) || (nxt != null && nxt.IsRomanNumeral() && i == (pieces.Count - 2) && !isAnInitial))
                    {
                        _LastList.Add(piece);
                        _SuffixList.AddRange(pieces.Skip(i + 1));
                        break;
                    }
                    else if (nxt != null)
                    {
                        // another component exists, so this is likely a middle name
                        _MiddleList.Add(piece);
                    }
                    else if (!ParseMultipleNames || AdditionalName == null)
                    {
                        // no additional name. some last names can appear to be suffixes. try to figure this out
                        if (_LastList.Count > 0 && piece.IsSuffix())
                        {
                            _SuffixList.Add(piece);
                        }
                        else
                        {
                            _LastList.Add(piece);
                        }
                    }
                    else if (AdditionalName._LastList.Any() && isAnInitial)
                    {
                        // the additional name has a last, and this one looks like a middle. we'll save as a middle and later will copy the last name.
                        _MiddleList.Add(piece);
                    }
                    else
                    {
                        _LastList.Add(piece);
                    }
                }
            }
        }
        else
        {
            var spaceSplitPartZero = parts[0].Span.SplitToSpan(' ').Select(p => new Piece(p)).ToList();
            var spaceSplitPartOne = parts[1].Span.SplitToSpan(' ').Select(p => new Piece(p)).ToList();
            var piecesPartOne = ParsePieces(spaceSplitPartOne, 1);
            if (spaceSplitPartZero.Count > 1 && AreSuffixes(piecesPartOne))
            {
                // suffix comma: title first middle last [suffix], suffix [suffix] [, suffix]
                //               parts[0],                         parts[1:...]
                _SuffixList.AddRange(parts.Skip(1));
                var pieces = ParsePieces(spaceSplitPartZero);
                for (var i = 0; i < pieces.Count; i++)
                {
                    var piece = pieces[i];
                    var nxt = i == pieces.Count - 1 ? null : pieces[i + 1];

                    if (!_FirstList.Any())
                    {
                        if ((nxt != null || pieces.Count == 1) && piece.IsTitle())
                        {
                            _TitleList.Add(piece);
                            continue;
                        }

                        _FirstList.Add(piece);
                        continue;
                    }

                    if (AreSuffixes(pieces.Skip(i + 1)))
                    {
                        _LastList.Add(piece);
                        _SuffixList.InsertRange(0, pieces.Skip(i + 1));
                        break;
                    }

                    // correct for when we have "John D" with an AdditionalName={Catherine, T, MacArthur}.
                    // We should not see this as being First=John, Last=D; rather, First=John, Middle=D, Last=<AdditionalName.Last>
                    if (nxt != null)
                    {
                        // another component exists, so this is likely a middle name
                        _MiddleList.Add(piece);
                    }
                    else if (!ParseMultipleNames || AdditionalName == null)
                    {
                        // no additional name, so treat this as a last
                        _LastList.Add(piece);
                    }
                    else if (AdditionalName._LastList.Any() && piece.IsAnInitial())
                    {
                        // the additional name has a last, and this one looks like a middle. we'll save as a middle and later will copy the last name.
                        _MiddleList.Add(piece);
                    }
                    else
                    {
                        _LastList.Add(piece);
                    }
                }
            }
            else
            {
                // lastname comma: last [suffix], title first middles[,] suffix [,suffix]
                //                 parts[0],      parts[1],              parts[2:...]
                var pieces = piecesPartOne;

                // lastname part may have suffixes in it
                var lastnamePieces = ParsePieces(spaceSplitPartZero, 1);

                foreach (var piece in lastnamePieces)
                {
                    // the first one is always a last name, even if it look like a suffix
                    if (_LastList.Any() && piece.IsSuffix())
                    {
                        _SuffixList.Add(piece);
                    }
                    else
                    {
                        _LastList.Add(piece);
                    }
                }

                for (var i = 0; i < pieces.Count; i++)
                {
                    var piece = pieces[i];
                    var nxt = i == pieces.Count - 1 ? null : pieces[i + 1];

                    if (!_FirstList.Any())
                    {
                        if ((nxt != null || pieces.Count == 1) && piece.IsTitle())
                        {
                            _TitleList.Add(piece);
                        }
                        else
                        {
                            _FirstList.Add(piece);
                        }
                    }
                    else if (piece.IsSuffix())
                    {
                        _SuffixList.Add(piece);
                    }
                    else
                    {
                        _MiddleList.Add(piece);
                    }
                }

                if (parts.Count >= 3 && parts.Skip(2).Any())
                {
                    _SuffixList.AddRange(parts.Skip(2));
                }
            }
        }

        IsUnparsable = !_TitleList.Any()
                       && !_FirstList.Any()
                       && !_MiddleList.Any()
                       && !_LastList.Any()
                       && !_SuffixList.Any()
                       && !_NicknameList.Any();

        PostProcessFirstnames();
        PostProcessLastname();
        PostProcessAdditionalName();
    }

    private void FixPhd(ref string fullName)
    {
        var match = RegexPhd.Match(fullName);
        if (match.Success)
        {
            _SuffixList.Add(new Piece(match.Groups[1].Value));
            fullName = RegexPhd.Replace(fullName, string.Empty);
        }
    }

    private static void ParseNicknames(ref string fullName, out List<Piece> nicknameList)
    {
        var fullNameSpan = fullName.AsSpan();
#if NET8_0_OR_GREATER
        if (!fullNameSpan.ContainsAny('(', '\'', '\"'))
#else
        if (!fullNameSpan.Contains('(') && !fullNameSpan.Contains('\'') && !fullNameSpan.Contains('"'))
#endif
        {
            nicknameList = new List<Piece>(0);
            return;
        }

        nicknameList = new List<Piece>();
        foreach (var regex in new[] { RegexQuotedWord, RegexDoubleQuotes, RegexParenthesis })
        {
            var match = regex.Match(fullName);
            while (match.Success && match.Groups[0].Value.Length > 0)
            {
                // remove from the full name the nickname plus its identifying boundary (parens or quotes)
                fullName = fullName.Replace(match.Value, string.Empty);

                // keep only the nickname part
                nicknameList.Add(new Piece(match.Groups[1].Value));

                match = regex.Match(fullName);
            }
        }
    }

    /// <summary>
    /// Split parts on spaces and remove commas, join on conjunctions and lastname prefixes.
    /// </summary>
    /// <param name="parts">name part strings from the comma split</param>
    /// <param name="additionalPartsCount"></param>
    /// <returns>pieces split on spaces and joined on conjunctions</returns>
    protected List<Piece> ParsePieces(List<Piece> parts, int additionalPartsCount = 0)
    {
        var output = new List<Piece>();
        foreach (var part in parts)
        {
            output.AddRange(part.Span.SplitToSpan(' ').Select(s => new Piece(s.Trim(','))));
        }

        // If part contains periods, check if it's multiple titles or suffixes
        // together without spaces if so, add the new part with periods to the
        // constants so they get parsed correctly later
        foreach (var part in output)
        {
            part.CheckForDot();
        }

        return joinOnConjunctions(output, additionalPartsCount);
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
    /// constant so they will be parsed correctly later. E.g. after parsing the
    /// example names above, 'The Secretary of State' and 'Mr. and Mrs.' would
    /// be present in the titles constant set.
    /// </remarks>
    /// <param name="pieces">name pieces strings after split on spaces</param>
    /// <param name="additionalPartsCount"></param>
    /// <returns>new list with piece next to conjunctions merged into one piece with spaces in it.</returns>
    internal List<Piece> joinOnConjunctions(List<Piece> pieces, int additionalPartsCount = 0)
    {
        var length = pieces.Count + additionalPartsCount;

        // don't join on conjuctions if there are only 2 parts
        if (length < 3)
            return pieces;

        var rootnamePieces = pieces.Where(IsRootname).ToArray();
        var totalLength = rootnamePieces.Length + additionalPartsCount;


        // find all the conjunctions, join any conjunctions that are next to each
        // other, then join those newly joined conjunctions and any single
        // conjunctions to the piece before and after it
        var conjunctionIndexes = new List<int>();
        for (var i = 0; i < pieces.Count; i++)
            if (pieces[i].IsConjunction())
                conjunctionIndexes.Add(i);

        var contiguousConjunctionIndexRanges = conjunctionIndexes.ConsecutiveRanges();

        var toDelete = new List<int>(length);
        foreach (var (first, last) in contiguousConjunctionIndexRanges)
        {
            if (first == last) continue;
            var newPiece = new Piece(string.Join(" ", pieces.Skip(first).Take(last - first + 1).Select(p => p.Span)), isConjunction: true);
            toDelete.AddRange(Enumerable.Range(first + 1, last - first));
            pieces[first] = newPiece;
        }

        toDelete.Reverse();

        foreach (var i in toDelete)
        {
            pieces.RemoveAt(i);
        }

        if (pieces.Count == 1)
        {
            return pieces;
        }

        conjunctionIndexes = [];
        for (var i = 0; i < pieces.Count; i++)
            if (pieces[i].IsConjunction())
                conjunctionIndexes.Add(i);

        for (var n = 0; n < conjunctionIndexes.Count; n++)
        {
            var i = conjunctionIndexes[n];
            // loop through the pieces backwards, starting at the end of the list.
            // Join conjunctions to the pieces on either side of them.
            if (pieces[i].Span.Length == 1 && totalLength < 4)
            {
                // if there are only 3 total parts (minus known titles, suffixes and prefixes) 
                // and this conjunction is a single letter, prefer treating it as an initial
                // rather than a conjunction.
                // http://code.google.com/p/python-nameparser/issues/detail?id=11
                continue;
            }

            if (i == 0)
            {
                var newPiece = new Piece(string.Join(" ", pieces.Skip(i).Take(2).Select(p => p.Span)));
                if (pieces[i + 1].IsTitle())
                {
                    newPiece.OverrideIsTitle();
                }

                pieces[i] = newPiece;
                pieces.RemoveAt(i + 1);

                for (var j = n + 1; j < conjunctionIndexes.Count; j++)
                {
                    conjunctionIndexes[j] -= 1;
                }
            }
            else
            {
                var newPiece = new Piece(string.Join(" ", pieces.Skip(i - 1).Take(3).Select(p => p.Span)));
                if (pieces[i - 1].IsTitle())
                {
                    newPiece.OverrideIsTitle();
                }

                pieces[i - 1] = newPiece;
                pieces.RemoveAt(i);
                var removedCount = 1;
                if (pieces.Count > i)
                {
                    pieces.RemoveAt(i);
                    removedCount++;
                }

                for (var j = n + 1; j < conjunctionIndexes.Count; j++)
                {
                    conjunctionIndexes[j] -= removedCount;
                }
            }
        }

        // join prefixes to following lastnames: ['de la Vega'], ['van Buren']
        // skip first part to avoid counting it as a prefix, e.g. "van" is either a first name or a preposition depending on its position
        var prefixes = pieces.Where(p => p.IsPrefix()).ToArray();
        if (prefixes.Length > 0)
        {
            var i = 0;
            foreach (var prefix in prefixes)
            {
                var newI = pieces.IndexOf(prefix);
                if (newI != -1) i = newI;

                // If it's the first piece and there are more than 1 rootnames, assume it's a first name
                if (i == 0 && totalLength >= 1) continue;

                var matchPrefix = false;
                for (var j = i + 1; j < pieces.Count; j++)
                {
                    if (pieces[j].IsPrefix())
                    {
                        matchPrefix = true;
                        if (j == i + 1)
                        {
                            j++;
                        }

                        var newPiece = new Piece(string.Join(" ", pieces.Skip(i).Take(j - i).Select(p => p.Span)));
                        pieces = pieces
                            .Take(i)
                            .Concat([newPiece])
                            .Concat(pieces.Skip(j))
                            .ToList();
                        break;
                    }
                }

                if (!matchPrefix)
                {
                    // join everything after the prefix until the next suffix
                    var nextSuffix = pieces.Skip(i).Where(p => p.IsSuffix()).ToArray();

                    if (nextSuffix.Length > 0)
                    {
                        var j = pieces.IndexOf(nextSuffix[0]);
                        var newPiece = new Piece(string.Join(" ", pieces.Skip(i).Take(j - i).Select(p => p.Span)));

                        pieces = pieces
                            .Take(i)
                            .Concat([newPiece])
                            .Concat(pieces.Skip(j))
                            .ToList();
                    }
                    else
                    {
                        var newPiece = new Piece(string.Join(" ", pieces.Skip(i).Select(p => p.Span)));
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
    private ReadOnlyMemory<char> CapitalizeWord(ReadOnlyMemory<char> word, string attribute)
    {
        var wordLower = word.Trim('.').ToString().ToLower();
        if ((new Piece(word).IsPrefix() && attribute is "last" or "middle") || new Piece(word).IsConjunction())
        {
            return wordLower.AsMemory();
        }

        // "phd" => "Ph.D."; "ii" => "II"
        var exception = CapitalizationExceptions.FirstOrDefault(tup => tup.Item1 == wordLower);

        if (exception != null)
        {
            return exception.Item2;
        }

        string wordAsString = word.ToString();
        if (RegexMac.IsMatch(wordAsString))
            // special case: "macbeth" should be "MacBeth"; "mcbride" -> "McBride"
            return RegexMac.Replace(wordAsString, m => ToTitleCase(m.Groups[1].Value.AsMemory()) + ToTitleCase(m.Groups[2].Value.AsMemory())).AsMemory();

        return ToTitleCase(word).AsMemory();
    }

    private static string ToTitleCase(ReadOnlyMemory<char> s)
    {
        if (s.Length == 0)
        {
            return string.Empty;
        }

        return s[..1].ToString().ToUpper() + s[1..].ToString().ToLower();
    }

    private ReadOnlyMemory<char> CapitalizePiece(ReadOnlyMemory<char> piece, string attribute)
    {
        return RegexWord.Replace(piece.ToString(), m => CapitalizeWord(m.Value.AsMemory(), attribute).ToString()).AsMemory();
    }

    /// <summary>
    /// Attempt to normalize the input values in a human-readable way.
    /// By default, it will not adjust the case of names entered in mixed case.
    /// To run capitalization on all names pass the parameter `force=True`.
    /// For example, "juan de garcia" would normalize to "Juan de Garcia"
    /// </summary>
    public void Normalize(bool? force = null)
    {
        var name = FullName;
        if (force != true && !(name == name.ToUpper() || name == name.ToLower()))
            return;

        _TitleList = _TitleList.Select(t => new Piece(CapitalizePiece(t.Span, "title"))).ToList();
        _FirstList = _FirstList.Select(t => new Piece(CapitalizePiece(t.Span, "first"))).ToList();
        _MiddleList = _MiddleList.Select(t => new Piece(CapitalizePiece(t.Span, "middle"))).ToList();
        _LastList = _LastList.Select(t => new Piece(CapitalizePiece(t.Span, "last")))
            .ToList(); // CapitalizePiece recognizes prefixes, so its okay to normalize "van der waals" like this
        _SuffixList = _SuffixList.Select(t => new Piece(CapitalizePiece(t.Span, "suffix"))).ToList();
        _NicknameList = _NicknameList.Select(t => new Piece(CapitalizePiece(t.Span, "nickname"))).ToList();
        _LastBaseList = _LastBaseList.Select(t => new Piece(CapitalizePiece(t.Span, "last"))).ToList();
        // normalizing _LastPrefixList would effectively be a no-op, so don't bother calling it
    }

    #endregion
}