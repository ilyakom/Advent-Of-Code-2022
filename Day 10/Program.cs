internal class Program
{
    private static void Main(string[] args)
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var cycle = 0;
        var register = 1;

        while (!s.EndOfStream)
        {
            var line = s.ReadLine();

            if (line == "noop")
            {
                Draw(cycle, register);
                cycle++;
                NewLine(ref cycle);
            }
            else
            {
                var num = int.Parse(line.Split(" ").Last());

                Draw(cycle, register);
                cycle++;
                NewLine(ref cycle);

                Draw(cycle, register);
                cycle++;
                NewLine(ref cycle);

                register += num;
            }
        }
    }

    private static void NewLine(ref int cycle)
    {
        if (cycle == 40)
        {
            cycle = 0;
            Console.WriteLine();
        }
    }

    private static void Draw(int cycle, int register)
    {
        if (cycle >= register - 1 && cycle <= register + 1)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write('.');
        }
    }
}