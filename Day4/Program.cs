var lines = ReadInputFile();

var numbers = Array.ConvertAll(lines[0].Split(","), int.Parse);
var cards = ParseBingoCards(lines);

var score = Play(new List<List<int>>(cards), numbers);
var scoreLast = Play(new List<List<int>>(cards), numbers, true);

Console.WriteLine($"Score first winning card: {score}");
Console.WriteLine($"Score of last card: {scoreLast}");


static int Play(List<List<int>> cards, int[] numbers, bool getScoreOfLastWinningCard = false)
{
    List<List<int>> winningCards = new();
    foreach (var number in numbers)
    {
        Draw(cards, number);
        CheckBingo(cards, winningCards);

        if (winningCards.Count > 0)
        {
            if (getScoreOfLastWinningCard)
            {
                if (cards.Count == 0)
                {
                    return CalculateScore(winningCards.Last(), number);
                }
            }
            else
            {
                return CalculateScore(winningCards.First(), number);
            }
        }
    }
    return 0;
}

static void Draw(List<List<int>> cards, int number)
{
    foreach (var card in cards)
    {
        for (int i = 0; i < card.Count; i++)
        {
            if (card[i] == number) card[i] = -1;
        }
    }
}

static void CheckBingo(List<List<int>> cards, List<List<int>> winningCards)
{
    foreach (var card in cards.ToArray())
    {
        // check rows
        for (int i = 0; i <= 20; i += 5)
        {
            if (card.GetRange(i, 5).Sum() == -5)
            {
                winningCards.Add(card);
                cards.Remove(card);
            }
        }
        // check columns
        for (int i = 0; i < 5; i++)
        {
            if (card[i] == -1 && card[i + 5] == -1 && card[i + 10] == -1 && card[i + 15] == -1 && card[i + 20] == -1)
            {
                winningCards.Add(card);
                cards.Remove(card);
            }
        }
    }
}


static int CalculateScore(List<int> winningCard, int winningNumber)
{
    var total = 0;
    foreach (var number in winningCard)
    {
        if (number != -1) total += number;
    }

    return total * winningNumber;
}

static List<List<int>> ParseBingoCards(string[] input)
{
    List<List<int>> bingoCards = new();
    for (int i = 2; i < input.Length; i += 6)
    {
        var card = new List<int>();
        for (int j = i; j < i + 5; j++)
        {
            var row = input[j].Split(" ");
            for (int k = 0; k < row.Length; k++)
            {
                if (row[k] != string.Empty)
                {
                    card.Add(int.Parse(row[k]));
                }
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