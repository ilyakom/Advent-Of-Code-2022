internal class Program
{
    private static void Main(string[] args)
    {
        StarOne(SetUp());
        Console.WriteLine();
        StarTwo(SetUp());
    }

    private static List<Stack<char>> SetUp()
    {
        var stacks = new List<Stack<char>>();

        for (int i = 0; i < 9; i++)
        {
            stacks.Add(new Stack<char>());
        }

        var cals = new List<string>()
        {
            "FHBVRQDP",
            "LDZQWV",
            "HLZQGRPC",
            "RDHFJVB",
            "ZWLC",
            "JRPNTGVM",
            "JRLVMBS",
            "DPJ",
            "DCNWV"
        };

        for (int i = 0; i < cals.Count; i++)
        {
            var str = cals[i];

            foreach (var c in str)
            {
                stacks[i].Push(c);
            }
        }

        return stacks;
    }

    private static void StarOne(List<Stack<char>> stacks)
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var sum = 0;
        var spliters = new string[4] { "move", "from", "to", " " };

        while (!s.EndOfStream)
        {
            var line = s.ReadLine().Split(spliters,StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            for(int i = 0; i < line[0]; i++)
            {
                if (!stacks[line[1]-1].TryPop(out var tmp))
                {
                    break;
                }

                stacks[line[2]-1].Push(tmp);
            }
        }

        foreach(var st in stacks)
        {
            Console.Write(st.Pop());
        }
    }

    private static void StarTwo(List<Stack<char>> stacks)
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var sum = 0;
        var spliters = new string[4] { "move", "from", "to", " " };

        while (!s.EndOfStream)
        {
            var line = s.ReadLine().Split(spliters, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var tmpStack = new Stack<char>();

            for (int i = 0; i < line[0]; i++)
            {
                if (!stacks[line[1] - 1].TryPop(out var tmp))
                {
                    break;
                }

                tmpStack.Push(tmp);
            }

            while ( tmpStack.Count > 0)
            {
                stacks[line[2] - 1].Push(tmpStack.Pop());
            }
        }

        foreach (var st in stacks)
        {
            Console.Write(st.Pop());
        }
    }
}