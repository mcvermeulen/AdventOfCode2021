var input = ReadInputFile()[0].Split(',').Select(int.Parse);

Console.WriteLine($"Puzzle 1: {CalculateFishPopulation(input, 80)}");
Console.WriteLine($"Puzzle 2: {CalculateFishPopulation(input, 256)}");

// Still way to slow, there must be a faster method.
static long CalculateFishPopulation(IEnumerable<int> input, int days)
{
    var groups = input.GroupBy(x => x).Select(group => new { group.Key, Count = group.Count() });
    long total = 0;
    foreach (var group in groups)
    {
        total += (CountChilds(group.Key, days) + 1) * group.Count;
    }
    return total;
}

static long CountChilds(int initAge, int days)
{
    long childs, count = 0;
    var daysLeft = days - initAge;
    if (daysLeft > 0)
    {
        childs = count = (int)Math.Ceiling(daysLeft / 7.0);

        for (int i = 0; i < childs; i++)
        {
            count += CountChilds(8, daysLeft - 1 - (i * 7));
        }
    }

    return count;
}

static string[] ReadInputFile()
{
    return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt"));
}
