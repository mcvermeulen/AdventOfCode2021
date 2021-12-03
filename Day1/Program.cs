using System;
using System.IO;

namespace Day1
{
    static class Program
    {
        static void Main()
        {
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            var lines = File.ReadAllLines(Path.Combine(path + "..\\..\\..\\..\\input.txt"));
            var list = Array.ConvertAll(lines, int.Parse);

            Console.WriteLine($"Puzzle 1: {DetermineIncreases(list)}");
            Console.WriteLine($"Puzzle 1: {DetermineThreeMeasurementIncreases(list)}");
        }

        private static int DetermineIncreases(int[] input)
        {
            int count = 0;

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] < input[i + 1])
                {
                    count++;
                }
            }

            return count;
        }

        private static int DetermineThreeMeasurementIncreases(int[] input)
        {
            int count = 0;

            for (int i = 0; i < input.Length - 3 ; i++)
            {
                int sum1 = input[i] + input[i + 1] + input[i + 2];
                int sum2 = input[i+1] + input[i + 2] + input[i + 3];
                if (sum1 < sum2)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
