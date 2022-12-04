internal class Program
{
    private static void Main(string[] args)
    {
        StarOne();
        StarTwo();
    }

    private static void StarTwo()
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var sum = 0;

        while (!s.EndOfStream)
        {
            var line1 = s.ReadLine().ToHashSet<char>();
            var line2 = s.ReadLine().ToHashSet<char>();
            var line3 = s.ReadLine().ToHashSet<char>();

            foreach (var c in line2)
            {
                if (!line1.Contains(c))
                {
                    line2.Remove(c);
                }
            }

            foreach (var c in line3)
            {
                if (!line2.Contains(c))
                {
                    line3.Remove(c);
                }
            }

            Console.WriteLine("Length:" + line3.Count);

            var elem = line3.First();

            if (elem >= 'a' && elem <= 'z')
            {
                sum += elem - 'a' + 1;
                Console.WriteLine(elem - 'a' + 1);
            }
            else
            {
                sum += (elem - 'A' + 1) + ('z' - 'a' + 1);
                Console.WriteLine((elem - 'A' + 1) + ('z' - 'a' + 1));
            }

            Console.WriteLine();
        }

        Console.WriteLine(sum);
    }

    private static void StarOne()
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var sum = 0;

        while (!s.EndOfStream)
        {
            var line = s.ReadLine();
            var hs = new HashSet<char>();
            var dups = new HashSet<char>();

            for (int i = 0; i < line.Length; i++)
            {
                if (i < line.Length / 2)
                {
                    hs.Add(line[i]);
                }
                else
                {
                    if (hs.Contains(line[i]) && !dups.Contains(line[i]))
                    {
                        dups.Add(line[i]);

                        Console.WriteLine(line[i]);

                        if (line[i] >= 'a' && line[i] <= 'z')
                        {
                            sum += line[i] - 'a' + 1;
                            Console.WriteLine(line[i] - 'a' + 1);
                        }
                        else
                        {
                            sum += (line[i] - 'A' + 1) + ('z' - 'a' + 1);
                            Console.WriteLine((line[i] - 'A' + 1) + ('z' - 'a' + 1));
                        }
                    }
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine(sum);
    }
}