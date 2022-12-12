internal class Program
{
    private static void Main(string[] args)
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        
        var map = new char[41, 159];
        var w = new int[41, 159];
        var q = new Queue<int[]>();

        int rc = 0;
        var end = new int[2];

        while (!s.EndOfStream)
        {
            var line = s.ReadLine();

            for (int i = 0; i < line.Length; i++)
            {
                map[rc, i] = line[i];
                w[rc, i] = int.MaxValue;

                if (line[i] == 'S')
                {
                    q.Enqueue(new[] { rc, i });
                    w[rc, i] = 0;
                    map[rc, i] = 'a';
                }

                if (line[i] == 'E')
                {
                    end[0] = rc;
                    end[1] = i;
                    map[rc, i] = 'z';
                }

                if (line[i] == 'a')
                {
                    q.Enqueue(new[] { rc, i });
                    w[rc, i] = 0;
                }
            }

            rc++;
        }

        while (q.Count > 0)
        {
            var cur = q.Dequeue();

            if (cur[0] == end[0] && cur[1] == end[1])
            {
                continue;
            }
            
            if (cur[0] != 0 && 
                map[cur[0], cur[1]] - map[cur[0]-1, cur[1]] >= -1 &&
                w[cur[0] - 1, cur[1]] > w[cur[0], cur[1]] + 1)
            {
                w[cur[0] - 1, cur[1]] = w[cur[0], cur[1]] + 1;

                q.Enqueue(new [] { cur[0] - 1, cur[1] });
            }

            if (cur[0] != 40 &&
                map[cur[0], cur[1]] - map[cur[0] + 1, cur[1]] >= -1 &&
                w[cur[0] + 1, cur[1]] > w[cur[0], cur[1]] + 1)
            {
                w[cur[0] + 1, cur[1]] = w[cur[0], cur[1]] + 1;

                q.Enqueue(new[] { cur[0] + 1, cur[1] });
            }

            if (cur[1] != 0 &&
                map[cur[0], cur[1]] - map[cur[0], cur[1] - 1] >= -1 &&
                w[cur[0], cur[1] - 1] > w[cur[0], cur[1]] + 1)
            {
                w[cur[0], cur[1] - 1] = w[cur[0], cur[1]] + 1;

                q.Enqueue(new[] { cur[0], cur[1] - 1 });
            }

            if (cur[1] != 158 &&
                map[cur[0], cur[1]] - map[cur[0], cur[1] + 1] >= -1 &&
                w[cur[0], cur[1] + 1] > w[cur[0], cur[1]] + 1)
            {
                w[cur[0], cur[1] + 1] = w[cur[0], cur[1]] + 1;

                q.Enqueue(new[] { cur[0], cur[1] + 1 });
            }
        }

        Console.WriteLine(w[end[0], end[1]]);
    }
 }