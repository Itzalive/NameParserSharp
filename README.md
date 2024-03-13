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

The same extensive suite of tests as python-nameparser is used to confirm it meets the same specification.

## Performance
There have been performance improvements to the library over the Python implementation to take advantage of some of the latest .NET features.

| Method                    | Runtime  | Mean     | Ratio | Allocated | Alloc Ratio |
|-------------------------- |--------- |---------:|------:|----------:|------------:|
| [NameParserSharp](https://github.com/aeshirey/NameParserSharp) (Based on python-nameparser V0.3.6) | .NET 6.0 | 4.980 us |  0.69 |    9.3 KB |        1.78 |
| [python-nameparser](https://pypi.org/project/nameparser/) V1.1.3 direct port          | .NET 6.0 | 7.171 us |  1.00 |   5.21 KB |        1.00 |
| Itzalive.NameParserSharp     | .NET 6.0 | 2.834 us |  0.39 |   4.83 KB |        0.93 |
| [NameParserSharp](https://github.com/aeshirey/NameParserSharp) (Based on python-nameparser V0.3.6) | .NET 8.0 | 4.446 us |  0.61 |   9.01 KB |        1.73 |
| [python-nameparser](https://pypi.org/project/nameparser/) V1.1.3 direct port          | .NET 8.0 | 4.404 us |  0.61 |   5.21 KB |        1.00 |
| Itzalive.NameParserSharp     | .NET 8.0 | 1.999 us |  0.28 |   4.54 KB |        0.87 |


## Using NameParserSharp

NameParserSharp is available as a NuGet package: `Install-Package Itzalive.NameParserSharp`


## Still to port
- Emoji character support
- Extracting initials

