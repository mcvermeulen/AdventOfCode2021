var input = ReadInput();

Console.WriteLine($"Puzzle 1: {Puzzle1(input)}");
Console.WriteLine($"Puzzle 2: {Puzzle2(input)}");

static int Puzzle1(string[] input)
{
    var digits = input.Select(x => x.Split('|')[1]).Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    var result = 0;

    result = digits.Sum(x => x.Count(y => y.Length == 2 || y.Length == 3 || y.Length == 4 || y.Length == 7));

    return result;
}

static int Puzzle2(string[] input)
{
    var sequences = input.Select(x => x.Split('|')[0]).Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();
    var digits = input.Select(x => x.Split('|')[1]).Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

    var result = 0;

    for (int i = 0; i < sequences.Length; i++)
    {
        var mapping = GetDigitMapping(sequences[i]);

        var parsednumber = string.Empty;
        foreach (var number in digits[i])
        {
            foreach (var map in mapping)
            {
                if (map.Value.Length == number.Length && map.Value.All(number.Contains))
                {
                    parsednumber += map.Key;
                    break;
                }
            }
        }

        result += int.Parse(parsednumber);
    }

    return result;
}

static Dictionary<string, string[]> GetDigitMapping(string[] sequence)
{
    var mapping = new Dictionary<string, string[]>();

    var ab = sequence.First(x => x.Length == 2);
    // segment A is used for 8 numbers, segment B is used for 9 numbers
    var a = sequence.Count(y => y.Contains(ab[..1])) == 8 ? ab[..1] : ab.Substring(1, 1);
    var b = ab.Replace(a, "");
    var d = sequence.First(x => x.Length == 3).Replace(a, "").Replace(b, "");
    var ef = sequence.First(x => x.Length == 4).Replace(a, "").Replace(b, "");
    // segment E is used for 6 numbers, segment F is used for 7 numbers 
    var e = sequence.Count(y => y.Contains(ef[..1])) == 6 ? ef[..1] : ef.Substring(1, 1);
    var f = ef.Replace(e, "");
    var gc = sequence.First(x => x.Length == 7).Replace(a, "").Replace(b, "").Replace(d, "").Replace(ef[..1], "").Replace(ef.Substring(1, 1), "");
    // segment G is used for 4 numbers, segment C is used for 7 numbers
    var g = sequence.Count(y => y.Contains(gc.Substring(0, 1))) == 4 ? gc[..1] : gc.Substring(1, 1);
    var c = gc.Replace(g, "");

    mapping.Add("1", new string[] { a, b });
    mapping.Add("2", new string[] { d, a, f, g, c });
    mapping.Add("3", new string[] { d, a, f, b, c });
    mapping.Add("4", new string[] { e, f, a, b });
    mapping.Add("5", new string[] { d, e, f, b, c });
    mapping.Add("6", new string[] { e, f, g, c, b, d });
    mapping.Add("7", new string[] { d, a, b });
    mapping.Add("8", new string[] { a, b, c, d, e, f, g });
    mapping.Add("9", new string[] { a, b, d, e, f, c });
    mapping.Add("0", new string[] { a, b, c, g, e, d });

    return mapping;
}

static string[] ReadInput()
{
    return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt"));
}
