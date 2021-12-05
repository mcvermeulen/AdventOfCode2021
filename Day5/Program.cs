var input = TransformInputToCoordinates();
var field = CreateField(input);

var count = 0;
for (int i = 0; i < field.GetLength(0); i++)
{
    for (int j = 0; j < field.GetLength(1); j++)
    {
        if (field[i, j] > 1) count++;
    }
}
Console.WriteLine(count);


static int[,] CreateField(List<Line> input)
{
    int maxX = input.Max(x => x.Coord1.X > x.Coord2.X ? x.Coord1.X : x.Coord2.X);
    int maxY = input.Max(x => x.Coord1.Y > x.Coord2.Y ? x.Coord1.Y : x.Coord2.Y);

    var field = new int[maxY + 1, maxX + 1];

    foreach(Line line in input)
    {
        if (line.IsHorizontal)
        {
            int startX = line.Coord1.X < line.Coord2.X ? line.Coord1.X : line.Coord2.X;
            int endX = line.Coord1.X < line.Coord2.X ? line.Coord2.X : line.Coord1.X;
            for (int i = startX; i <= endX; i++)
            {
                field[line.Coord1.Y, i]++;
            }
        } 
        else if (line.IsVertical) 
        {
            int startY = line.Coord1.Y < line.Coord2.Y ? line.Coord1.Y : line.Coord2.Y;
            int endY = line.Coord1.Y < line.Coord2.Y ? line.Coord2.Y : line.Coord1.Y;
            for (int j = startY; j <= endY; j++)
            {
                field[j, line.Coord1.X]++;
            }
        } 
        else if (line.Is45degrees)
        {
            if (line.Coord1.X < line.Coord2.X)
            {
                if (line.Coord1.Y < line.Coord2.Y)
                {
                    for (int k = line.Coord1.X, l = line.Coord1.Y; k <= line.Coord2.X; k++, l++)
                    {
                        field[l, k]++;
                    }
                } else
                {
                    for (int k = line.Coord1.X, l = line.Coord1.Y; k <= line.Coord2.X; k++, l--)
                    {
                        field[l, k]++;
                    }
                }
            } else
            {
                if (line.Coord1.Y < line.Coord2.Y)
                {
                    for (int k = line.Coord1.X, l = line.Coord1.Y; k >= line.Coord2.X; k--, l++)
                    {
                        field[l, k]++;
                    }
                }
                else
                {
                    for (int k = line.Coord1.X, l = line.Coord1.Y; k >= line.Coord2.X; k--, l--)
                    {
                        field[l, k]++;
                    }
                }
            }
        }
    }

    return field;
}

static List<Line> TransformInputToCoordinates()
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