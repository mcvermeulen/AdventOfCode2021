var lines = ReadInputFile();

var numbers = Array.ConvertAll(lines[0].Split(","), int.Parse);
var cards = ParseBingoCards(lines);

Play(cards, numbers);


static void Play(List<int[]> cards, int[] numbers)
{
    List<int> scores = new List<int>();
    foreach (var number in numbers)
    {
        Draw(cards, number);
        foreach(var score in CheckBingo(cards, number))
        {
            scores.Add(score);
        }
    }

    Console.WriteLine($"Score first winning card: {scores.First()}");
    Console.WriteLine($"Score of last card: {scores.Last()}");
}

static void Draw(List<int[]> cards, int number)
{
    foreach (var card in cards)
    {
        for (int i = 0; i < card.Length; i++)
        {
            if (card[i] == number) card[i] = -1;
        }
    }
}

static IEnumerable<int> CheckBingo(List<int[]> cards, int number)
{
    foreach (var card in cards.ToArray())
    {
        // check rows
        for (int i = 0; i <= 20; i += 5)
        {
            if (card.ToList().GetRange(i, 5).Sum() == -5)
            {
                yield return CalculateScore(card, number);
                cards.Remove(card);
            }
        }
        // check columns
        for (int i = 0; i < 5; i++)
        {
            if (card[i] == -1 && card[i + 5] == -1 && card[i + 10] == -1 && card[i + 15] == -1 && card[i + 20] == -1)
            {
                yield return CalculateScore(card, number);
                cards.Remove(card);
            }
        }
    }
}


static int CalculateScore(int[] winningCard, int winningNumber)
{
    var total = 0;
    foreach (var number in winningCard)
    {
        if (number != -1) total += number;
    }

    return total * winningNumber;
}

static List<int[]> ParseBingoCards(string[] input)
{
    List<int[]> bingoCards = new();

    for (int i = 2; i < input.Length; i += 6)
    {
        var card = new int[25];
        var index = -1;
        for (int j = i; j < i + 5; j++)
        {
            var row = input[j].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int k = 0; k < row.Length; k++)
            {
                card[++index] = int.Parse(row[k]);
            }
        }
        bingoCards.Add(card);
    }

    return bingoCards;
}

static string[] ReadInputFile()
{
    return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt"));
}