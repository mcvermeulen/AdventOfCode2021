var lines = ReadInputFile();

var gammaRating = GetGammaRating(lines);
var epsilonRating = GetEpsilonRating(lines);
var oxygenGeneratorRating = GetOxygenGeneratorRating(lines);
var co2ScrubberRating = GetCo2ScrubberRating(lines);

Console.WriteLine($"Power consumption: {gammaRating * epsilonRating}");
Console.WriteLine($"Life support rating: {oxygenGeneratorRating * co2ScrubberRating}");

static int GetGammaRating(string[] input)
{
    return Convert.ToInt32(GetCommonBits(input.ToList()), 2);
}

static int GetEpsilonRating(string[] input)
{
    return Convert.ToInt32(GetCommonBits(input.ToList(), true), 2);
}

static int GetOxygenGeneratorRating(string[] input)
{
    return Convert.ToInt32(GetBitCriteria(input.ToList(), false), 2);
}

static int GetCo2ScrubberRating(string[] input)
{
    return Convert.ToInt32(GetBitCriteria(input.ToList(), true), 2);
}

static string GetCommonBits(List<string> input, bool least = false)
{
    string result = string.Empty;

    for (int i = 0; i < input[0].Length; i++)
    {
        if (least)
        {
            result = string.Concat(result, InvertCommonBit(GetMostCommonBit(input, i)));
        }
        else
        {
            result = string.Concat(result, GetMostCommonBit(input, i));
        }
    }

    return result.ToString();
}

static string GetBitCriteria(List<string> input, bool invert)
{
    string result = string.Empty;

    for (int i = 0; i < input[0].Length; i++)
    {
        var compareBit = GetMostCommonBit(input, i);
        if (invert) compareBit = InvertCommonBit(compareBit);

        input = input.Where(l => int.Parse(l.Substring(i, 1)) == compareBit).ToList();
        if (input.Count == 1)
        {
            result = input.First();
            break;
        }
    }

    return result;
}

static int GetMostCommonBit(List<string> list, int position)
{
    int zeros = 0;
    int ones = 0;
    foreach (var line in list)
    {
        var x = int.Parse(line.Substring(position, 1));
        if (x == 0)
        {
            zeros++;
        }
        else
        {
            ones++;
        }
    }

    if (zeros > ones) return 0;
    else return 1;
}

static int InvertCommonBit(int bit)
{
    if (bit == 1) return 0;
    return 1;
}

static string[] ReadInputFile()
{
    return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt"));
}