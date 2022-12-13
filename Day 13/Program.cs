using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var nodes = new List<Node>();
        var res = 0;

        while (!s.EndOfStream)
        {
            int i = 0;
            var line = s.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var node = Build(line, ref i);

            nodes.Add(node);
        }

        var d1 = new Node 
        { 
            Children = new List<Node> { new Node { Children = new List<Node> { new Node(2) } } }
        };

        var d2 = new Node 
        { 
            Children = new List<Node> { new Node { Children = new List<Node> { new Node(6) } } }
        };

        nodes.Add(d1);
        nodes.Add(d2);
       
        for (int i = 0; i < nodes.Count-1; i++)
        {
            if (Compare(nodes[i], nodes[i+1]) != true)
            {
                var tmp = nodes[i];
                nodes[i] = nodes[i+1];
                nodes[i+1] = tmp;

                i = -1;
            }
        }

        Console.WriteLine(nodes.IndexOf(d1)+1);
        Console.WriteLine(nodes.IndexOf(d2)+1);

        Console.WriteLine((nodes.IndexOf(d1)+1) * (nodes.IndexOf(d2)+1));
    }

    private static Node Build(string str, ref int i)
    {
        Node node = null;

        while (i < str.Length)
        {
            if (str[i] == '[')
            {   
                if (node == null)
                {
                    node = new Node();
                    i++;
                }
                else
                {
                    var res = Build(str, ref i);

                    if (res != null)
                    {
                        node.Children.Add(res);
                    }
                }
            }
            else if (str[i] == ']')
            {
                i++;
                return node;
            }
            else if (char.IsDigit(str[i]))
            {
                if (node == null)
                {
                    node = new Node();
                }

                var sb = new StringBuilder();

                while (char.IsDigit(str[i]))
                {
                    sb.Append(str[i]);
                    i++;
                }

                node.Children.Add(new Node(int.Parse(sb.ToString())));
            }
            else
            {
                i++;
            }
        }

        return node;
    }

    private static bool? Compare(Node left, Node right)
    {
        if (!left.IsList() && !right.IsList())
        {
            if (left.Value == right.Value)
            {
                return null;
            }

            return left.Value < right.Value;
        }

        if (left.IsList() && right.IsList())
        {
            if (left.Children.Count == 0 && right.Children.Count == 0)
            {
                return null;
            }

            if (left.Children.Count == 0 || right.Children.Count == 0)
            {
                return left.Children.Count == 0;
            }

            int i = 0;

            for (; i < Math.Min(left.Children.Count, right.Children.Count); i++)
            {
                var rc = Compare(left.Children[i], right.Children[i]);

                if (rc != null)
                {
                    return rc;
                }
            }

            if (left.Children.Count == right.Children.Count)
            {
                return null;
            }

            return i == left.Children.Count;
        }

        if (!left.IsList() && right.IsList())
        {
            if (right.Children.Count == 0)
            {
                return false;
            }

            var tmpLeft = new Node();
            tmpLeft.Children.Add(left);

            return Compare(tmpLeft, right);
        }

        if (left.IsList() && !right.IsList())
        {
            if (left.Children.Count == 0)
            {
                return true;
            }

            var tmpRight = new Node();
            tmpRight.Children.Add(right);

            return Compare(left, tmpRight);
        }

        throw new Exception();
    }

    public class Node
    {
        public List<Node> Children { get; set; }

        public int Value { get; set; }

        public Node()
        {
            Children = new List<Node>();
            Value = -1;
        }

        public Node(int v)
        {
            Children = null;
            this.Value= v;
        }

        public bool IsList()
        {
            return this.Children != null;
        }
    }
}