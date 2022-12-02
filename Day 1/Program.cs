internal class Program
{
    private static void Main(string[] args)
    {
        var list = new List<int>();

        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var localSum = 0;
        var line = string.Empty;

        while (!s.EndOfStream)
        {
            line = s.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                list.Add(localSum);
                localSum = 0;
            }
            else
            {
                localSum += int.Parse(line);
            }
        }

        Console.WriteLine(list.OrderByDescending(x => x).Take(3).Sum());
    }
}