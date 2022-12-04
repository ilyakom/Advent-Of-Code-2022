internal class Program
{
    private static void Main(string[] args)
    {
        StarOne();
        StarTwo();
    }

    private static void StarOne()
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var sum = 0;
        var spliters = new char[2] { ',', '-' };

        while (!s.EndOfStream)
        {
            var line = s.ReadLine().Split(spliters).Select(int.Parse).ToArray();

            if (line[0] <= line[2] && line[1] >= line[3] ||
                line[0] >= line[2] && line[1] <= line[3])
            {
                sum++;
            }
        }

        Console.WriteLine(sum);
    }

    private static void StarTwo()
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var sum = 0;
        var spliters = new char[2] { ',', '-' };

        while (!s.EndOfStream)
        {
            var line = s.ReadLine().Split(spliters).Select(int.Parse).ToArray();

            if (line[0] <= line[2] && line[1] >= line[3] ||
                line[0] >= line[2] && line[1] <= line[3] ||
                line[0] >= line[2] && line[0] <= line[3] ||
                line[1] >= line[2] && line[1] <= line[3])
            {
                sum++;
            }
        }

        Console.WriteLine(sum);
    }
}