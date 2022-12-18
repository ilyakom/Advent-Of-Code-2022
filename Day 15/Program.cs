using System.Runtime.CompilerServices;

internal class Program
{
    private const long Min = 0;
    private const long Max = 4_000_000;

    private static void Main(string[] args)
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var splitters = new[] { ":", "," };
        var sensors = new List<Sensor>();
        var beacons = new HashSet<Beacon>();

        var lines = new List<long[]>[Max-Min+1];

        while (!s.EndOfStream)
        {
            var line = s.ReadLine().Split(splitters, StringSplitOptions.RemoveEmptyEntries);

            var sensor = new Sensor
            {
                X = long.Parse(line[0][("Sensor at x=".Length)..]),
                Y = long.Parse(line[1][(" y=".Length)..])
            };

            var bx = long.Parse(line[2][(" closest beacon is at x=".Length)..]);
            var by = long.Parse(line[3][(" y=".Length)..]);

            beacons.Add(new Beacon { X= bx, Y = by });

            sensor.R = Math.Abs(sensor.X - bx) + Math.Abs(sensor.Y - by);

            if (sensor.Y + sensor.R >= Min || sensor.Y - sensor.R <= Max)
            {
                for (long i = Math.Max(Min, sensor.Y - sensor.R); i <= Math.Min(Max, sensor.Y + sensor.R); i++)
                {
                    InsertToLines(sensor, i, lines);
                }
            }

            sensors.Add(sensor);
        }

        // Find coordinates
        for (int l = 0; l < lines.Length; l++)
        {
            if (lines[l] != null && lines[l].Select(x => x[1] - x[0] + 1).Sum() != Max+1)
            {
                Console.WriteLine($"{Max+1} is not {lines[l].Select(x => x[1] - x[0] + 1).Sum()}");
                Console.WriteLine("Y:" + l);

                var ordered = lines[l].OrderBy(x => x[0]).ToArray();

                if (ordered[0][0] != Min)
                {
                    Console.WriteLine($"0 element != {Min}. Paste {l}");
                }

                if (ordered.Last()[1] != Max)
                {
                    Console.WriteLine($"It's last. So: { Max * 4_000_000 + l}");
                }

                for (int i = 0; i < ordered.Length-1; i++)
                {
                    if (ordered[i][1] + 1 != ordered[i + 1][0])
                    {
                        Console.WriteLine($"X: {ordered[i][1] + 1}");
                        Console.WriteLine((ordered[i][1] + 1) * 4_000_000 + l);
                        return;
                    }
                }
            }
        }

    }

    private static void InsertToLines(Sensor sensor, long line, List<long[]>[] lines)
    {
        var l = 2 * sensor.R;
        l -= 2 * Math.Abs(line - sensor.Y);

        if (sensor.X - l / 2 > Max || sensor.X + l / 2 < Min)
        {
            return;
        }

        var newLine = new[] {
                    sensor.X - l / 2 >= Min ? sensor.X - l / 2 : Min,
                    sensor.X + l / 2 <= Max ? sensor.X + l / 2 : Max};

        var toAdd = new Queue<long[]>();
        toAdd.Enqueue(newLine);

        if (lines[line] == null)
        {
            lines[line] = new List<long[]>();
        }

        while (toAdd.Count > 0)
        {
            var ln = GetToInsert(toAdd, lines[line]);

            if (ln != null)
            {
                lines[line].Add(ln);
            }
        }
    }

    private static long[] GetToInsert(Queue<long[]> toAdd, List<long[]> lines)
    {
        var newLine = toAdd.Dequeue();

        foreach (var item in lines)
        {
            var added = false;

            if (newLine[0] >= item[0] && newLine[1] <= item[1])
            {
                return null;
            }

            if (newLine[0] < item[0] && newLine[1] >= item[0])
            {
                toAdd.Enqueue(new[] { newLine[0], item[0] - 1 });
                added = true;
            }

            if (newLine[1] > item[1] && newLine[0] <= item[1])
            {
                toAdd.Enqueue(new[] { item[1] + 1, newLine[1] });
                added = true;
            }

            if (added)
            {
                return null;
            }
        }



        return newLine;
    }
}

public class Sensor : Point
{
    public long R { get; set; }

    public long Square => R * R * 2;
}

public class Beacon : Point
{
    public override int GetHashCode()
    {
        return HashCode.Combine(X,Y);
    }

    public override bool Equals(object? obj)
    {
        var those = obj as Beacon;

        return those.X == this.X && those.Y == this.Y;
    }
}

public abstract class Point
{
    public long X { get; set; }
    public long Y { get; set; } 
}