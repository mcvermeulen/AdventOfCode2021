var input = ReadInputFile();

var AllPossibleDistances = Enumerable.Range(0, input.Max());

var puzzle1 = AllPossibleDistances
    .Select(x => input.Sum(x2 => Math.Abs(x2 - x)))
    .Min();

var puzzle2 = AllPossibleDistances
    .Select(x => input
        .Select(x2 => Math.Abs(x2 - x))
        .Sum(n => n * (n + 1) / 2))
    .Min();

Console.WriteLine($"Puzzle 1: {puzzle1}");
Console.WriteLine($"Puzzle 2: {puzzle2}");


static IEnumerable<int> ReadInputFile()
{
    var file = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt"));
    return file.Split(',').Select(int.Parse);
} 