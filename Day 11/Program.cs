using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var monkeys = new List<Monkey>();
        var mon = new Monkey { AllMonkeys = monkeys };

        while (!s.EndOfStream)
        {
            var line = s.ReadLine().Trim();

            if (string.IsNullOrEmpty(line))
            {
                monkeys.Add(mon);
                mon = new Monkey
                {
                    AllMonkeys = monkeys
                };
            }
            else if (line.StartsWith("Starting items"))
            {
                foreach (var item in line.Split(':')[1].Split(',').Select(x => long.Parse(x.Trim())))
                {
                    mon.Items.Enqueue(item);
                }
            }
            else if (line.StartsWith("Monkey"))
            {
                mon.Id = int.Parse(line.Split(new char[2] {' ', ':'}, StringSplitOptions.RemoveEmptyEntries)[1]);
            }
            else if (line.StartsWith("Test"))
            {
                mon.Dividor = long.Parse(line.Split(' ').Last());
            }
            else if (line.StartsWith("If true"))
            {
                mon.TrueTo = int.Parse(line.Split(' ').Last());
            }
            else if (line.StartsWith("If false"))
            {
                mon.FalseTo = int.Parse(line.Split(' ').Last());
            }
        }

        monkeys.Add(mon);

        long preventor = 1;

        foreach (var monkey in monkeys)
        {
            preventor *= monkey.Dividor;
        }

        for (long i = 1; i <= 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    monkey.InspectNext(preventor);
                }
            }
        }

        var res = monkeys.OrderByDescending(x => x.InsCount).ToArray();

        Console.WriteLine(res[0].InsCount * res[1].InsCount);
    }
}

public class Monkey
{
    public int Id { get; set; }

    public Queue<long> Items { get; set; }

    public long Dividor { get; set; }

    public int TrueTo { get; set; }

    public int FalseTo { get; set; }

    public long InsCount { get; set; }

    public List<Monkey> AllMonkeys { get; set; }

    public Func<long, long> Operation { 
        get 
        {
            switch (Id)
            {
                case 0:
                    return (long x) => checked(x * 19);
                case 1:    
                    return (long x) => checked(x + 1);
                case 2:                
                    return (long x) => checked(x + 8);
                case 3:                
                    return (long x) => checked(x * x);
                case 4:                
                    return (long x) => checked(x + 6);
                case 5:                
                    return (long x) => checked(x * 17);
                case 6:                
                    return (long x) => checked(x + 5);
                case 7:                
                    return (long x) => checked(x + 3);
                default:
                    throw new Exception();
            }
        }}

    public Monkey()
    {
        Items = new Queue<long>();
    }

    public void InspectNext(long preventor)
    {
        InsCount++;

        var item = Items.Dequeue();
        item = Operation(item) % preventor;

        if (item % Dividor == 0)
        {
            AllMonkeys[TrueTo].Items.Enqueue(item);
        }
        else
        {
            AllMonkeys[FalseTo].Items.Enqueue(item);
        }
    }
}