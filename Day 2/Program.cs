internal class Program
{
    private static void Main(string[] args)
    {
        var list = new List<int>();

        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var score = 0;

        while (!s.EndOfStream)
        {
            var line = s.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (line[1] == "Y" && line[0] == "A" || 
                line[1] == "Z" && line[0] == "C" || 
                line[1] == "X" && line[0] == "B")
            {
                score += 1;
            }
            else if (line[1] == "Y" && line[0] == "B" ||
                line[1] == "Z" && line[0] == "A" ||
                line[1] == "X" && line[0] == "C")
            {
                score += 2;
            }
            else
            {
                score += 3;
            }

            if (line[1] == "Y" )
            {
                score+= 3;
            }
            else if (line[1] == "Z")
            {
                score += 6;
            }

        }

        Console.WriteLine(score);
    }
}