var lines = ReadInputFile();

Console.WriteLine($"Puzzle 1: {SolvePuzzle1(lines)}");
Console.WriteLine($"Puzzle 2: {SolvePuzzle2(lines)}");


static int SolvePuzzle1(string[] input)
{
    int forward = 0;
    int depth = 0;

    for (int i = 0; i < input.Length; i++)
    {
        var command = input[i].Split(" ");
        switch (command[0])
        {
            case "forward":
                forward += int.Parse(command[1]);
                break;
            case "down":
                depth += int.Parse(command[1]);
                break;
            case "up":
                depth -= int.Parse(command[1]);
                break;
            default:
                break;
        }
    }

    return forward * depth;
}

static int SolvePuzzle2(string[] input)
{
    int forward = 0;
    int depth = 0;
    int aim = 0;

    for (int i = 0; i < input.Length; i++)
    {
        var command = input[i].Split(" ");
        switch (command[0])
        {
            case "forward":
                forward += int.Parse(command[1]);
                depth += int.Parse(command[1]) * aim;
                break;
            case "down":
                aim += int.Parse(command[1]);
                break;
            case "up":
                aim -= int.Parse(command[1]);
                break;
            default:
                break;
        }
    }

    return forward * depth;
}

static string[] ReadInputFile()
{
    return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt"));
}