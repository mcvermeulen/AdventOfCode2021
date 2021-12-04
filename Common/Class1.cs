namespace Common
{
    public class Files
    {
        public static string[] ReadInputFile()
        {
            return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "input.txt"));
        }
    }
}