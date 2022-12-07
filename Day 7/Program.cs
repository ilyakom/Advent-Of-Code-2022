internal class Program
{
    private static long overall = 70_000_000;
    private static long required = 30_000_000;
    private static long result = 0;

    private static void Main(string[] args)
    {
        Stars();
    }

    private static void Stars()
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var root = new Node();
        root.Name = "/";

        Add(root, s);

        Console.WriteLine("Star one:" + result);

        var available = overall - root.Weight;
        var toClean = required - available;

        var toDelete = Search(root, toClean, root);

        Console.WriteLine("Star two:" + toDelete.Weight);
    }

    private static void Add(Node root, StreamReader s)
    {
        while (!s.EndOfStream)
        {
            var line = s.ReadLine();

            if (line == "$ cd ..")
            {
                break;
            }

            if (line == "$ ls")
            {
                continue;
            }

            if (line.StartsWith("dir"))
            {
                var name = line.Substring(4);
                var node = new Node { Name = name };
                root.Children.Add(name, node);
            }
            else if (line.StartsWith("$ cd"))
            {
                var name = line.Substring(5);
                if (!root.Children.TryGetValue(name, out var node))
                {
                    throw new Exception();
                }

                Add(node, s);
                root.Weight += node.Weight;
            }
            else
            {
                var parts = line.Split(' ');
                var weight = long.Parse(parts[0]);
                var name = parts[1];

                var node = new Node
                {
                    Weight = weight,
                    Name = name
                };

                root.Weight += weight;
                root.Children.Add(name, node);
            }
        }

        if (root.Weight <= 100_000)
        {
            result += root.Weight;
        }
    }

    private static Node Search(Node root, long toClean, Node min)
    {
        if (root.Weight < toClean)
        {
            return min;
        }
        else
        {

            foreach (var item in root.Children)
            {
                min = Search(item.Value, toClean, min);
            }

            if (root.Weight < min.Weight)
            {
                return root;
            }
            else
            {
                return min;
            }
        }
    }
}

public class Node
{
    public long Weight { get; set; }

    public string Name { get; set; }

    public Dictionary<string, Node> Children { get; set; }

    public Node()
    {
        Children = new Dictionary<string, Node>();
    }
}