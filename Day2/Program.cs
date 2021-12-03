using System;
using System.IO;

namespace Day2
{
    static class Program
    {
        static void Main()
        {
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            var lines = File.ReadAllLines(Path.Combine(path, "..", "..", "..", "input.txt"));

            Console.WriteLine($"Puzzle 1: {SolvePuzzle1(lines)}");
            Console.WriteLine($"Puzzle 2: {SolvePuzzle2(lines)}");
        }

        private static int SolvePuzzle1(string[] input)
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

        private static int SolvePuzzle2(string[] input)
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
    }
}
