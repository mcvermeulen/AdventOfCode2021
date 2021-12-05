var input = TransformInputToLines();

Console.WriteLine($"Puzzle 1: {GetIntersections(input)}");
Console.WriteLine($"Puzzle 2: {GetIntersections(input, true)}");

static int GetIntersections(List<Line> lines, bool includeDiagonals = false)
{
    var dic = new Dictionary<Coord, int>();
    foreach (var line in lines)
    {
        var difX = line.Coord2.X - line.Coord1.X;
        var difY = line.Coord2.Y - line.Coord1.Y;
        var directionX = Math.Sign(difX);
        var directionY = Math.Sign(difY);

        if (includeDiagonals || directionX == 0 || directionY == 0)
        {
            var points =
                from i in Enumerable.Range(0, Math.Max(Math.Abs(difX), Math.Abs(difY)) + 1)
                select new Coord(line.Coord1.X + i * directionX, line.Coord1.Y + i * directionY);

            foreach (var point in points)
            {
                dic[point] = dic.GetValueOrDefault(point, 0) + 1;
            }
        }
       
    }
    return dic.Count(item => item.Value > 1);
}


static List<Line> TransformInputToLines()
{
    List<Line> input = new();
    foreach (var line in ReadInputFile())
    {
        input.Add(new Line(line));
    }
    return input;
}

static string[] ReadInputFile()
{
    return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt"));
}

record Coord(int X, int Y);

class Line {
    public Coord Coord1 { get; private set; }
    public Coord Coord2 { get; private set; }

    public bool IsHorizontal { get; private set; }
    public bool IsVertical { get; private set; }
    public bool Is45degrees { get; private set; }

    public Line(string line)
    {
        var row = (from v in line.Split(", ->".ToArray(), StringSplitOptions.RemoveEmptyEntries) select int.Parse(v)).ToArray();
 
        Coord1 = new Coord(row[0], row[1]);
        Coord2 = new Coord(row[2], row[3]);

        if (Coord1.Y == Coord2.Y) IsHorizontal = true;
        else if (Coord1.X == Coord2.X) IsVertical = true;
        else if (Math.Abs(Coord1.X - Coord2.X) == Math.Abs(Coord1.Y - Coord2.Y)) Is45degrees = true;
    }
}