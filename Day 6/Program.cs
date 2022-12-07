internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine(Star(4));
        Console.WriteLine(Star(14));
    }
    
    private static int Star(int ml)
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);
        
        var line = s.ReadToEnd();

        var hs = new Dictionary<char, int>();

        for (int i = 0; i < ml; i++)
        {
            if (hs.ContainsKey(line[i]))
            {
                hs[line[i]]++;
            }
            else
            {
                hs.Add(line[i], 1);
            }
        }

        if (hs.Where(x => x.Value == 1).Count() == ml) 
        {
            return ml;
        }

        for (int i = ml; i < line.Length; i++)
        {
            if (hs[line[i-ml]] == 1)
            {
                hs.Remove(line[i-ml]);
            }
            else
            {
                hs[line[i - ml]]--;
            }

            if (hs.ContainsKey(line[i]))
            {
                hs[line[i]]++;
            }
            else
            {
                hs.Add(line[i], 1);
            }

            if (hs.Where(x => x.Value == 1).Count() == ml)
            {
                return i+1;
            }
        }

        return -1;
    }
}