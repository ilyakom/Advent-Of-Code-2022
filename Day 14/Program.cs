internal class Program
{
    private static void Main(string[] args)
    {
        // READ DATA

        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var lines = new List<List<int[]>>();

        var minX = int.MaxValue;
        var minY = 0;
        var maxX = int.MinValue; 
        var maxY = int.MinValue;

        while (!s.EndOfStream)
        {
            var line = s.ReadLine().Split(" -> ").Select(x => x.Split(',').Select(int.Parse).ToArray()).ToList();

            var locMinx = line.Select(x => x[0]).Min();
            var locMaxx = line.Select(x => x[0]).Max();

            var locMiny = line.Select(x => x[1]).Min();
            var locMaxy = line.Select(x => x[1]).Max();

            minX = minX > locMinx ? locMinx : minX;
            maxX = maxX < locMaxx ? locMaxx : maxX;

            //minY = minY > locMiny ? locMiny : minY;
            maxY = maxY < locMaxy ? locMaxy : maxY;

            lines.Add(line);
        }

        // FILL THE MAP

        var map = new char[maxY-minY+1, maxX-minX+1];

        foreach ( var line in lines )
        {
            for (int i = 0; i < line.Count - 1; i++)
            {
                if (line[i][0] == line[i + 1][0])
                {
                    var minPoint = line[i][1] < line[i + 1][1] ? line[i] : line[i + 1];
                    var maxPoint = line[i][1] < line[i + 1][1] ? line[i + 1] : line[i];

                    for (int j = minPoint[1]; j <= maxPoint[1]; j++)
                    {
                        map[j - minY, line[i][0] - minX] = '#';
                    }
                }
                else
                {
                    var minPoint = line[i][0] < line[i + 1][0] ? line[i] : line[i + 1];
                    var maxPoint = line[i][0] < line[i + 1][0] ? line[i + 1] : line[i];

                    for (int j = minPoint[0]; j <= maxPoint[0]; j++)
                    {
                        map[minPoint[1] - minY, j - minX] = '#';
                    }
                }
            }
        }

        map[0, 500 - minX] = 'X';
        var p = new[] {0, 500 - minX};
        var res = 0;

        // DROP SAND

        while(true)
        {
            var t = map.GetLength(0);

            if (p[0] == map.GetLength(0)-1)
            {
                break;
            }

            if (map[p[0] + 1, p[1]] == '\0')
            {
                p[0]++;
                continue;
            }

            if (map[p[0] + 1, p[1] - 1] == '\0')
            {
                p[0]++;
                p[1]--;
                continue;
            }

            if (map[p[0] + 1, p[1] + 1] == '\0')
            {
                p[0]++;
                p[1]++;
                continue;
            }

            if (p[0] == 0 && p[1] == 500 - minX)
            {
                // border ~inifinite line added manually
                res++;
                break;
            }

            res++;
            map[p[0], p[1]] = 'O';
            p[0] = 0;
            p[1] = 500 - minX;
        }

        
        Draw(map);

        Console.WriteLine(res);
    }

    private static void Draw(char[,] m)
    {
        for (int i = 0; i < m.GetLength(0); i++)
        {
            for (int j = 0; j < m.GetLength(1); j++)
            {
                Console.Write(m[i, j] == '\0' ? '.' : m[i, j]);
            }

            Console.WriteLine();
        }
    }
}