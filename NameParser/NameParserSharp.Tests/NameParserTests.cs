namespace NameParserSharp.Tests;

using NameParserSharp;
using System;

using Xunit;

public partial class NameParserTests {
    [Fact]
    public void NullInput() {
        Assert.Throws<ArgumentNullException>(() => new HumanName(null));
    }

    [Fact]
    public void BlankInput() {
        var parsed = new HumanName(string.Empty);
        Assert.Equal(string.Empty, parsed.First);
        Assert.Equal(string.Empty, parsed.Middle);
        Assert.Equal(string.Empty, parsed.Last);
        Assert.Equal(string.Empty, parsed.Title);
        Assert.Equal(string.Empty, parsed.Nickname);
        Assert.Equal(string.Empty, parsed.Suffix);
    }

    [Fact]
    public void Jfk() {
        var jfk = new HumanName("president john 'jack' fitzgerald kennedy");

        Assert.Equal("president", jfk.Title);
        Assert.Equal("john", jfk.First);
        Assert.Equal("fitzgerald", jfk.Middle);
        Assert.Equal("kennedy", jfk.Last);
        Assert.Equal(string.Empty, jfk.Suffix);
        Assert.Equal("jack", jfk.Nickname);
        Assert.Equal("president john fitzgerald kennedy (jack)", jfk.ToString());
        Assert.Equal("kennedy", jfk.LastBase);
        Assert.Equal(string.Empty, jfk.LastPrefixes);

        jfk.Normalize();

        Assert.Equal("President", jfk.Title);
        Assert.Equal("John", jfk.First);
        Assert.Equal("Fitzgerald", jfk.Middle);
        Assert.Equal("Kennedy", jfk.Last);
        Assert.Equal(string.Empty, jfk.Suffix);
        Assert.Equal("Jack", jfk.Nickname);
        Assert.Equal("President John Fitzgerald Kennedy (Jack)", jfk.ToString());
        Assert.Equal("Kennedy", jfk.LastBase);
        Assert.Equal(string.Empty, jfk.LastPrefixes);
    }

    [Fact]
    public void Nixon() {
        var nixon = new HumanName("mr president richard (dick) nixon");

        Assert.Equal("mr president", nixon.Title);
        Assert.Equal("richard", nixon.First);
        Assert.Equal(string.Empty, nixon.Middle);
        Assert.Equal("nixon", nixon.Last);
        Assert.Equal(string.Empty, nixon.Suffix);
        Assert.Equal("dick", nixon.Nickname);
        Assert.Equal("mr president richard nixon (dick)", nixon.ToString());
        Assert.Equal("nixon", nixon.LastBase);
        Assert.Equal(string.Empty, nixon.LastPrefixes);

        nixon.Normalize();

        Assert.Equal("Mr President", nixon.Title);
        Assert.Equal("Richard", nixon.First);
        Assert.Equal(string.Empty, nixon.Middle);
        Assert.Equal("Nixon", nixon.Last);
        Assert.Equal(string.Empty, nixon.Suffix);
        Assert.Equal("Dick", nixon.Nickname);
        Assert.Equal("Mr President Richard Nixon (Dick)", nixon.ToString());
        Assert.Equal("Nixon", nixon.LastBase);
        Assert.Equal(string.Empty, nixon.LastPrefixes);
    }

    [Fact]
    public void TitleFirstOrLastName() {
        var mrJones = new HumanName("Mr. Jones");
        Assert.Equal("Mr.", mrJones.Title);
        Assert.Equal(string.Empty, mrJones.First);
        Assert.Equal(string.Empty, mrJones.Middle);
        Assert.Equal("Jones", mrJones.Last);
        Assert.Equal(string.Empty, mrJones.Suffix);
        Assert.Equal(string.Empty, mrJones.Nickname);
        Assert.Equal("Jones", mrJones.LastBase);
        Assert.Equal(string.Empty, mrJones.LastPrefixes);

        var uncleAdam = new HumanName("Uncle Adam");
        Assert.Equal("Uncle", uncleAdam.Title);
        Assert.Equal("Adam", uncleAdam.First);
        Assert.Equal(string.Empty, uncleAdam.Middle);
        Assert.Equal(string.Empty, uncleAdam.Last);
        Assert.Equal(string.Empty, uncleAdam.Suffix);
        Assert.Equal(string.Empty, uncleAdam.Nickname);
        Assert.Equal(string.Empty, uncleAdam.LastBase);
        Assert.Equal(string.Empty, uncleAdam.LastPrefixes);
    }

    [Fact]
    public void DifferentInputsSameValues() {
        var fml = new HumanName("john x smith");
        var lfm = new HumanName("smith, john x");

        Assert.True(fml == lfm);
    }

    [Fact]
    public void NicknameAtBeginningDoubleQuote() {
        var parsed = new HumanName("\"TREY\" ROBERT HENRY BUSH III");

        Assert.Equal(parsed.First, "ROBERT");
        Assert.Equal(parsed.Middle, "HENRY");
        Assert.Equal(parsed.Last, "BUSH");
        Assert.Equal(parsed.Nickname, "TREY");
        Assert.Equal(parsed.Suffix, "III");
    }

    [Fact]
    public void NicknameAtBeginningSingleQuote() {
        var parsed = new HumanName("'TREY' ROBERT HENRY BUSH III");

        Assert.Equal(parsed.First, "ROBERT");
        Assert.Equal(parsed.Middle, "HENRY");
        Assert.Equal(parsed.Last, "BUSH");
        Assert.Equal(parsed.Nickname, "TREY");
        Assert.Equal(parsed.Suffix, "III");
    }

    [Fact]
    public void LastBaseAndPrefixes() {
        var parsed = new HumanName("John Smith");
        Assert.Equal("Smith", parsed.Last);
        Assert.Equal(string.Empty, parsed.LastPrefixes);
        Assert.Equal("Smith", parsed.LastBase);

        parsed = new HumanName("johannes van der waals");
        Assert.Equal("johannes", parsed.First);
        Assert.Equal("van der", parsed.LastPrefixes); // specifically, the prefixes to the last name
        Assert.Equal("waals", parsed.LastBase); // only the base component of the last name
        Assert.Equal("van der waals", parsed.Last); // the full last name, combined

        parsed.Normalize();
        Assert.Equal("Johannes", parsed.First);
        Assert.Equal("van der", parsed.LastPrefixes);
        Assert.Equal("Waals", parsed.LastBase);
        Assert.Equal("van der Waals", parsed.Last);
    }

    [Fact]
    // https://github.com/aeshirey/NameParserSharp/issues/8
    public void Parens() {
        var johnSmith = new HumanName("(John Smith)");
        Assert.Equal(string.Empty, johnSmith.First);
        Assert.Equal(string.Empty, johnSmith.Last);
        Assert.Equal("John Smith", johnSmith.Nickname);
    }

    [Fact]
    public void FirstMiddleLastSuffixNoCommas() {
        var john = new HumanName("John Quincy Smith III");
        Assert.Equal("John", john.First);
        Assert.Equal("Quincy", john.Middle);
        Assert.Equal("Smith", john.Last);
        Assert.Equal("III", john.Suffix);

        var robert = new HumanName("Robert Lee Elder III");
        Assert.Equal("Robert", robert.First);
        Assert.Equal("Lee", robert.Middle);
        Assert.Equal("Elder", robert.Last);
        Assert.Equal("III", robert.Suffix);
    }

    [Fact]
    public void TwoCommaWithMiddleName() {
        var parsed = new HumanName("Surname, John Middle, III");

        Assert.Equal(parsed.First, "John");
        Assert.Equal(parsed.Middle, "Middle");
        Assert.Equal(parsed.Last, "Surname");
        Assert.Equal(parsed.Suffix, "III");
    }

    [Fact]
    public void FirstLastPrefixesLastSuffixNoCommas() {
        var valeriano = new HumanName("Valeriano De Leon JR.");

        Assert.Equal("Valeriano", valeriano.First);
        Assert.Equal("De", valeriano.LastPrefixes);
        Assert.Equal("De Leon", valeriano.Last);
        Assert.Equal("JR.", valeriano.Suffix);

        var quincy = new HumanName("Quincy De La Rosa Sr");
        Assert.Equal("Quincy", quincy.First);
        Assert.Equal("De La", quincy.LastPrefixes);
        Assert.Equal("De La Rosa", quincy.Last);
        Assert.Equal("Sr", quincy.Suffix);
    }

    [InlineData("VAN L JOHNSON", "VAN", "L", "JOHNSON")]
    [InlineData("VAN JOHNSON", "VAN", "", "JOHNSON")]
    [InlineData("JOHNSON, VAN L", "VAN", "L", "JOHNSON")]
    [Theory]
    // https://github.com/aeshirey/NameParserSharp/issues/15
    public void PrefixAsFirstName(string full, string first, string middle, string last) {
        var sut = new HumanName(full);

        Assert.Equal(first, sut.First);
        Assert.Equal(middle, sut.Middle);
        Assert.Equal(last, sut.Last);
    }

    [Fact]
    public void Conjunctions() {
        var mice = new HumanName("mrs and mrs mickey and minnie mouse");
    }

    /// <summary>
    /// https://github.com/aeshirey/NameParserSharp/issues/18
    /// </summary>
    [Fact]
    public void AddToLists() {
        var asIs = new HumanName("Mr. John Smith 2nd");
        Assert.Equal("Mr.", asIs.Title);
        Assert.Equal("John", asIs.First);
        Assert.Equal("Smith", asIs.Middle);
        Assert.Equal("2nd", asIs.Last);
        Assert.Equal("", asIs.Suffix);

        HumanName.Suffixes.Add("2nd");
        var with2Nd = new HumanName("Mr. John Smith 2nd");
        Assert.Equal("Mr.", with2Nd.Title);
        Assert.Equal("John", with2Nd.First);
        Assert.Equal("Smith", with2Nd.Last);
        Assert.Equal("2nd", with2Nd.Suffix);
    }

    /// <summary>
    /// https://github.com/aeshirey/NameParserSharp/issues/20
    /// </summary>
    [Fact]
    public void FirstNameIsPrefix() {
        // Default behavior
        var parsedPrefix = new HumanName("Mr. Del Richards");
        Assert.Equal(parsedPrefix.Title, "Mr.");
        Assert.Equal(parsedPrefix.First, "");
        Assert.Equal(parsedPrefix.Last, "Del Richards");
        Assert.Equal(parsedPrefix.LastPrefixes, "Del");

        // A single prefix should be treated as a first name when no first exists
        var parsedFirst = new HumanName("Mr. Del Richards", Prefer.FirstOverPrefix);
        Assert.Equal(parsedFirst.Title, "Mr.");
        Assert.Equal(parsedFirst.First, "Del");
        Assert.Equal(parsedFirst.Last, "Richards");
        Assert.Equal(parsedFirst.LastPrefixes, "");
    }
}