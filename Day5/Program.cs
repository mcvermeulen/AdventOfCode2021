//var lines = Common.Files.ReadInputFile();

// See https://aka.ms/new-console-template for more information
test();

static void test()
{
    Console.WriteLine("Hello, World!");
}

static string[] ReadInputFile()
{
    return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt"));
}