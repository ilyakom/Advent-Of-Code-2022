internal class Program
{
    private static void Main(string[] args)
    {
        Star();
    }

    private static void Star()
    {
        var map = Read();
        var res = 0;

        for (int i = 0; i < 99; i++)
        {
            for (int j = 0; j < 99; j++)
            {
                var l = CheckTop(map, i + 1, j, map[i, j]) *
                    CheckBottom(map, i - 1, j, map[i, j]) *
                    CheckLeft(map, i, j + 1, map[i, j]) *
                    CheckRight(map, i, j - 1, map[i, j]);

                if (l > res)
                {
                    res = l;
                }
            }
        }

        Console.WriteLine(res);
    }

    private static int[,] Read()
    {
        var map = new int[99, 99];

        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var lineNum = 0;
        while (!s.EndOfStream)
        {
            var colNum = 0;
            foreach (var item in s.ReadLine().Select(x => x - '0'))
            {
                map[lineNum, colNum] = item;
                colNum++;
            }
            lineNum++;
        }

        return map;
    }

    private static int CheckTop(int[,] map, int r, int c, int max) 
    {   
        if (r > 98)
        {
            return 0;
        }

        return map[r, c] < max ? 1 + CheckTop(map, r + 1, c, max) : 1;
    }

    private static int CheckLeft(int[,] map, int r, int c, int max)
    {
        if (c > 98)
        {
            return 0;
        }

        return map[r, c] < max ? 1 + CheckLeft(map, r, c+1, max) : 1;
    }

    private static int CheckBottom(int[,] map, int r, int c, int max)
    {
        if (r < 0)
        {
            return 0;
        }

        return map[r, c] < max ? 1 + CheckBottom(map, r-1, c, max) : 1;
    }

    private static int CheckRight(int[,] map, int r, int c, int max)
    {
        if (c < 0)
        {
            return 0;
        }
       
        return map[r, c] < max ? 1 + CheckRight(map, r, c-1, max) : 1;
    }
}