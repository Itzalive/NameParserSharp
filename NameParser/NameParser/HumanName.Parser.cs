using System.Collections.Generic;
using System.Linq;
using System;

namespace NameParser;

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

    public string Title => string.Join(" ", _TitleList);

    public string First => string.Join(" ", _FirstList);

    public string Middle => string.Join(" ", _MiddleList);

    public string Last => string.Join(" ", _LastList);

    public string Suffix => string.Join(", ", _SuffixList);

    public string Nickname => string.Join(" ", _NicknameList);

    /// <summary>
    /// If <see cref="ParseMultipleNames"/> is true and the input contains "&" or "and", the additional
    /// name will be parsed out and put into a second <see cref="HumanName"/> record. For example,
    /// "John D. and Catherine T. MacArthur" should be parsed as {John, D, MacArthur} with an AdditionalName
    /// set to the parsed value {Catherine, T, MacAthur}.
    /// </summary>
    public HumanName? AdditionalName { get; private set; }

    public string LastBase => string.Join(" ", _LastBaseList);
    public string LastPrefixes => string.Join(" ", _LastPrefixList);

    #endregion

    private string _FullName, _OriginalName;

    private List<ReadOnlyMemory<char>> _TitleList;
    private List<ReadOnlyMemory<char>> _FirstList;
    private List<ReadOnlyMemory<char>> _MiddleList;
    private List<ReadOnlyMemory<char>> _LastList;
    private List<ReadOnlyMemory<char>> _SuffixList;
    private List<ReadOnlyMemory<char>> _NicknameList;
    private List<ReadOnlyMemory<char>> _LastBaseList;
    private List<ReadOnlyMemory<char>> _LastPrefixList;
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
               (string.IsNullOrEmpty(left.Nickname) || string.IsNullOrEmpty(right.Nickname) ||
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

        if (includeEmpty || !string.IsNullOrEmpty(Title))
        {
            d["title"] = Title;
        }

        if (includeEmpty || !string.IsNullOrEmpty(First))
        {
            d["first"] = First;
        }

        if (includeEmpty || !string.IsNullOrEmpty(Middle))
        {
            d["middle"] = Middle;
        }

        if (includeEmpty || !string.IsNullOrEmpty(Last))
        {
            d["last"] = Last;
        }

        if (includeEmpty || !string.IsNullOrEmpty(LastBase))
        {
            d["lastbase"] = LastBase;
        }

        if (includeEmpty || !string.IsNullOrEmpty(LastPrefixes))
        {
            d["lastprefixes"] = LastPrefixes;
        }

        if (includeEmpty || !string.IsNullOrEmpty(Suffix))
        {
            d["suffix"] = Suffix;
        }

        if (includeEmpty || !string.IsNullOrEmpty(Nickname))
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

    private bool IsTitle(ReadOnlyMemory<char> value)
    {
        var lcValue = value.Trim('.').ToString().ToLower();
        return Titles.Contains(lcValue) || CombinedTitles.Contains(lcValue);
    }

    private bool IsConjunction(ReadOnlyMemory<char> piece)
    {
        string value = piece.ToString();
        var lcValue = value.ToLower();
        return (Conjunctions.Contains(lcValue) || CombinedConjunctions.Contains(lcValue)) && !IsAnInitial(value);
    }

    private static bool IsPrefix(ReadOnlyMemory<char> piece)
    {
        var lcValue = piece.Trim('.').ToString().ToLower();
        return Prefixes.Contains(lcValue);
    }

    private static bool IsRomanNumeral(ReadOnlyMemory<char> value)
    {
        return RegexRomanNumeral.IsMatch(value.ToString());
    }

    private bool IsSuffix(ReadOnlyMemory<char> piece)
    {
        var value = piece.ToString();
        var lcValue = value.Trim('.').ToLower();
        var removedDots = lcValue.Replace(".", string.Empty);
        return (Suffixes.Contains(removedDots) ||
                SuffixesNotAcronyms.Contains(lcValue) ||
                CombinedSuffixesNotAcronyms.Contains(lcValue)) && !IsAnInitial(value);
    }

    private bool AreSuffixes(IEnumerable<ReadOnlyMemory<char>> pieces)
    {
        return pieces.Any() && pieces.All(IsSuffix);
    }

    /// <summary>
    /// Determines whether <see cref="piece"/> is a given name component as opposed to an affix, initial or title.
    /// </summary>
    /// <param name="piece">A single word from a name</param>
    /// <returns>False if <see cref="piece"/> is a prefix (de, abu, bin), suffix (jr, iv, cpa), title (mr, pope), or initial (x, e.); true otherwise</returns>
    private bool IsRootname(ReadOnlyMemory<char> piece)
    {
        string value = piece.ToString();
        var lcPiece = value.Trim('.').ToLower();

        return !Prefixes.Contains(lcPiece)
               && !Suffixes.Contains(lcPiece)
               && !SuffixesNotAcronyms.Contains(lcPiece)
               && !CombinedSuffixesNotAcronyms.Contains(lcPiece)
               && !Titles.Contains(lcPiece)
               && !CombinedTitles.Contains(lcPiece)
               && !IsAnInitial(value);
    }

    /// <summary>
    /// Words with a single period at the end, or a single uppercase letter.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>True if <see cref="value"/> matches the regex "^(\w\.|[A-Z])?$"</returns>
    private static bool IsAnInitial(string value)
    {
        return RegexInitial.IsMatch(value);
    }

    #endregion

    #region full name parser

    /// <summary>
    /// Collapse multiple spaces into single space
    /// </summary>
    private static void CollapseWhitespace(ref string value)
    {
        value = RegexSpaces.Replace(value.Trim(), " ");
        if (value.EndsWith(","))
            value = value[..^1];
    }

    /// <summary>
    /// If there are only two parts and one is a title, assume it's a last name
    /// instead of a first name. e.g. Mr. Johnson. Unless it's a special title
    /// like "Sir", then when it's followed by a single name that name is always
    /// a first name.
    /// </summary>
    private void PostProcessFirstnames()
    {
        if (!string.IsNullOrEmpty(Title)
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
            .SelectMany(part => part.SplitToSpan(' '))
            .ToArray();

        var prefixCount = 0;
        while (prefixCount < words.Length && IsPrefix(words[prefixCount]))
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
        if (string.IsNullOrEmpty(Last))
        {
            _LastList = AdditionalName._LastList;
        }
        else
        {
            // for names like "Smith, John And Jane", we'd have to propagate the name backward (possibly through multiple names)
            var next = AdditionalName;
            while (next != null && string.IsNullOrEmpty(next.Last))
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
            .Select(part => part.Trim())
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
                var nxt = i == pieces.Count - 1 ? ReadOnlyMemory<char>.Empty : pieces[i + 1];

                // title must have a next piece, unless it's just a title
                if (!this._FirstList.Any())
                {
                    if ((nxt.Length != 0 || pieces.Count == 1) && IsTitle(piece))
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
                    bool isAnInitial = IsAnInitial(piece.ToString());
                    if (AreSuffixes(pieces.Skip(i + 1)) || (IsRomanNumeral(nxt) && i == (pieces.Count - 2) && !isAnInitial))
                    {
                        _LastList.Add(piece);
                        _SuffixList.AddRange(pieces.Skip(i + 1));
                        break;
                    }
                    else if (nxt.Length != 0)
                    {
                        // another component exists, so this is likely a middle name
                        _MiddleList.Add(piece);
                    }
                    else if (!ParseMultipleNames || AdditionalName == null)
                    {
                        // no additional name. some last names can appear to be suffixes. try to figure this out
                        if (_LastList.Count > 0 && IsSuffix(piece))
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
            var spaceSplitPartZero = parts[0].SplitToSpan(' ').ToList();
            var spaceSplitPartOne = parts[1].SplitToSpan(' ').ToList();
            var piecesPartOne = ParsePieces(spaceSplitPartOne.ToList());
            if (spaceSplitPartZero.Count > 1 && AreSuffixes(spaceSplitPartOne))
            {
                // suffix comma: title first middle last [suffix], suffix [suffix] [, suffix]
                //               parts[0],                         parts[1:...]
                _SuffixList.AddRange(parts.Skip(1));
                var pieces = ParsePieces(spaceSplitPartZero);
                for (var i = 0; i < pieces.Count; i++)
                {
                    var piece = pieces[i];
                    var nxt = i == pieces.Count - 1 ? ReadOnlyMemory<char>.Empty : pieces[i + 1];

                    if (string.IsNullOrEmpty(First))
                    {
                        if ((nxt.Length != 0 || pieces.Count == 1) && IsTitle(piece))
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
                    if (nxt.Length != 0)
                    {
                        // another component exists, so this is likely a middle name
                        _MiddleList.Add(piece);
                    }
                    else if (!ParseMultipleNames || AdditionalName == null)
                    {
                        // no additional name, so treat this as a last
                        _LastList.Add(piece);
                    }
                    else if (AdditionalName._LastList.Any() && IsAnInitial(piece.ToString()))
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
                    if (_LastList.Any() && IsSuffix(piece))
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
                    var nxt = i == pieces.Count - 1 ? ReadOnlyMemory<char>.Empty : pieces[i + 1];

                    if (!_FirstList.Any())
                    {
                        if ((nxt.Length != 0 || pieces.Count == 1) && IsTitle(piece))
                        {
                            _TitleList.Add(piece);
                        }
                        else
                        {
                            _FirstList.Add(piece);
                        }
                    }
                    else if (IsSuffix(piece))
                    {
                        _SuffixList.Add(piece);
                    }
                    else
                    {
                        _MiddleList.Add(piece);
                    }
                }

                if (parts.Count >= 3 && parts.Skip(2).Any(p => p.Length != 0))
                {
                    _SuffixList.AddRange(parts.Skip(2).Where(p => p.Length != 0));
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
            _SuffixList.Add(match.Groups[1].Value.AsMemory());
            fullName = RegexPhd.Replace(fullName, string.Empty);
        }
    }

    private static void ParseNicknames(ref string fullName, out List<ReadOnlyMemory<char>> nicknameList)
    {
        nicknameList = new List<ReadOnlyMemory<char>>();

        foreach (var regex in new[] { RegexQuotedWord, RegexDoubleQuotes, RegexParenthesis })
        {
            var match = regex.Match(fullName);
            while (match.Success && match.Groups[0].Value.Length > 0)
            {
                // remove from the full name the nickname plus its identifying boundary (parens or quotes)
                fullName = fullName.Replace(match.Value, string.Empty);

                // keep only the nickname part
                nicknameList.Add(match.Groups[1].Value.AsMemory());

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
    protected List<ReadOnlyMemory<char>> ParsePieces(List<ReadOnlyMemory<char>> parts, int additionalPartsCount = 0)
    {
        var output = new List<ReadOnlyMemory<char>>();
        foreach (var part in parts)
        {
            output.AddRange(part.SplitToSpan(' ').Select(s => s.Trim(',')));
        }

        // If part contains periods, check if it's multiple titles or suffixes
        // together without spaces if so, add the new part with periods to the
        // constants so they get parsed correctly later
        foreach (var part in output)
        {
            // if this part has a period not at the beginning or end
            if (part.Length > 3 && part.Span[1..^2].Contains('.'))
            {
                // split on periods, any of the split pieces titles or suffixes?
                // ("Lt.Gov.")
                var periodChunks = part.SplitToSpan('.');

                // add the part to the constant so it will be found
                if (periodChunks.Any(IsTitle))
                {
                    CombinedTitles.Add(part.Trim('.').ToString().ToLower());
                }
                else if (periodChunks.Any(IsSuffix))
                {
                    CombinedSuffixesNotAcronyms.Add(part.Trim('.').ToString().ToLower());
                }
            }
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
    internal List<ReadOnlyMemory<char>> joinOnConjunctions(List<ReadOnlyMemory<char>> pieces, int additionalPartsCount = 0)
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
            if (IsConjunction(pieces[i]))
                conjunctionIndexes.Add(i);

        var contiguousConjunctionIndexRanges = conjunctionIndexes.ConsecutiveRanges();

        var toDelete = new List<int>(length);
        foreach (var (first, last) in contiguousConjunctionIndexRanges)
        {
            if (first == last) continue;
            var newPiece = string.Join(" ", pieces.Skip(first).Take(last - first + 1));
            toDelete.AddRange(Enumerable.Range(first + 1, last - first));
            pieces[first] = newPiece.AsMemory();
            CombinedConjunctions.Add(newPiece.ToLower());
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
            if (IsConjunction(pieces[i]))
                conjunctionIndexes.Add(i);

        for (var n = 0; n < conjunctionIndexes.Count; n++)
        {
            var i = conjunctionIndexes[n];
            // loop through the pieces backwards, starting at the end of the list.
            // Join conjunctions to the pieces on either side of them.
            if (pieces[i].Length == 1 && totalLength < 4)
            {
                // if there are only 3 total parts (minus known titles, suffixes and prefixes) 
                // and this conjunction is a single letter, prefer treating it as an initial
                // rather than a conjunction.
                // http://code.google.com/p/python-nameparser/issues/detail?id=11
                continue;
            }

            if (i == 0)
            {
                var newPiece = string.Join(" ", pieces.Skip(i).Take(2));
                if (IsTitle(pieces[i + 1]))
                {
                    CombinedTitles.Add(newPiece.Trim('.').ToLower());
                }

                pieces[i] = newPiece.AsMemory();
                pieces.RemoveAt(i + 1);

                for (var j = n + 1; j < conjunctionIndexes.Count; j++)
                {
                    conjunctionIndexes[j] -= 1;
                }
            }
            else
            {
                var newPiece = string.Join(" ", pieces.Skip(i - 1).Take(3));
                if (IsTitle(pieces[i - 1]))
                {
                    CombinedTitles.Add(newPiece.Trim('.').ToLower());
                }

                pieces[i - 1] = newPiece.AsMemory();
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
        var prefixes = pieces.Where(IsPrefix).ToArray();
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
                    if (IsPrefix(pieces[j]))
                    {
                        matchPrefix = true;
                        if (j == i + 1)
                        {
                            j++;
                        }

                        var newPiece = string.Join(" ", pieces.Skip(i).Take(j - i));
                        pieces = pieces
                            .Take(i)
                            .Concat([newPiece.AsMemory()])
                            .Concat(pieces.Skip(j))
                            .ToList();
                        break;
                    }
                }

                if (!matchPrefix)
                {
                    // join everything after the prefix until the next suffix
                    var nextSuffix = pieces.Skip(i).Where(IsSuffix).ToArray();

                    if (nextSuffix.Length > 0)
                    {
                        var j = pieces.IndexOf(nextSuffix[0]);
                        var newPiece = string.Join(" ", pieces.Skip(i).Take(j - i));

                        pieces = pieces
                            .Take(i)
                            .Concat([newPiece.AsMemory()])
                            .Concat(pieces.Skip(j))
                            .ToList();
                    }
                    else
                    {
                        var newPiece = string.Join(" ", pieces.Skip(i));
                        pieces = pieces.Take(i).Concat([newPiece.AsMemory()]).ToList();
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
        if ((IsPrefix(word) && attribute is "last" or "middle") || IsConjunction(word))
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

        _TitleList = _TitleList.Select(t => CapitalizePiece(t, "title")).ToList();
        _FirstList = _FirstList.Select(t => CapitalizePiece(t, "first")).ToList();
        _MiddleList = _MiddleList.Select(t => CapitalizePiece(t, "middle")).ToList();
        _LastList = _LastList.Select(t => CapitalizePiece(t, "last"))
            .ToList(); // CapitalizePiece recognizes prefixes, so its okay to normalize "van der waals" like this
        _SuffixList = _SuffixList.Select(t => CapitalizePiece(t, "suffix")).ToList();
        _NicknameList = _NicknameList.Select(t => CapitalizePiece(t, "nickname")).ToList();
        _LastBaseList = _LastBaseList.Select(t => CapitalizePiece(t, "last")).ToList();
        // normalizing _LastPrefixList would effectively be a no-op, so don't bother calling it
    }

    #endregion
}