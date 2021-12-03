using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day3
{
    static class Program
    {
        static void Main()
        {
            var lines = File.ReadAllLines("input.txt");

            var gammaRating = GetGammaRating(lines);
            var epsilonRating = GetEpsilonRating(lines);
            var oxygenGeneratorRating = GetOxygenGeneratorRating(lines);
            var co2ScrubberRating = GetCo2ScrubberRating(lines);

            Console.WriteLine($"Power consumption: {gammaRating * epsilonRating}");
            Console.WriteLine($"Life support rating: {oxygenGeneratorRating * co2ScrubberRating}");
        }

        private static int GetGammaRating(string[] input)
        {
            return Convert.ToInt32(GetCommonBits(input.ToList()), 2);
        }

        private static int GetEpsilonRating(string[] input)
        {
            return Convert.ToInt32(GetCommonBits(input.ToList(), true), 2);
        }

        private static int GetOxygenGeneratorRating(string[] input)
        {
            return Convert.ToInt32(GetBitCriteria(input.ToList(), false), 2);
        }

        private static int GetCo2ScrubberRating(string[] input)
        {
            return Convert.ToInt32(GetBitCriteria(input.ToList(), true), 2);
        }

        private static string GetCommonBits(List<string> input, bool least = false)
        {
            StringBuilder result = new();

            for (int i = 0; i < input[0].Length; i++)
            {
                if (least)
                {
                    result.Append(InvertCommonBit(GetMostCommonBit(input, i)));
                }
                else
                {
                    result.Append(GetMostCommonBit(input, i));
                }
            }

            return result.ToString();
        }

        private static string GetBitCriteria(List<string> input, bool invert)
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

        private static int GetMostCommonBit(List<string> list, int position)
        {
            int zeros = 0;
            int ones = 0;
            foreach (var line in list) 
            {
                var x = int.Parse(line.Substring(position, 1));
                if (x == 0)
                {
                    zeros++;
                } else
                {
                    ones++;
                }
            }

            if (zeros > ones) return 0;
            else return 1;
        }

        private static int InvertCommonBit(int bit)
        {
            if (bit == 1) return 0;
            return 1;
        }

    }
}
