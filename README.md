# NameParserSharp

Based upon [nameparser 1.1.3](https://pypi.python.org/pypi/nameparser), NameParserSharp is a C# library that parses a human name into constituent fields `Title`, `First`, `Middle`, `Last`, `Suffix`, and `Nickname` from the `HumanName` class. For example:

```c#
var jfk = new HumanName("president john 'jack' f kennedy");

// person.Title == "president"
// person.First == "john"
// person.Middle == "f"
// person.Last == "kennedy"
// person.Nickname == "jack"

var jfk_alt = new HumanName("kennedy, president john (jack) f");

Assert.IsTrue(jfk == jfk_alt);
```

NameParserSharp implements the functionality of the Python project on which it is based in a C# idiomatic way.

NameParserSharp is available as a NuGet package: `Install-Package NameParserSharp`
