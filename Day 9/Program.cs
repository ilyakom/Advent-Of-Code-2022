internal class Program
{
    private static void Main(string[] args)
    {
        using var reader = File.OpenRead("./input.txt");
        using var s = new StreamReader(reader);

        var snake = new Point[10];

        for (int i = 0;i < snake.Length; i++)
        {
            snake[i] = new Point();
        }

        var hs = new HashSet<Point> { new Point { x = 0, y = 0 } };

        while (!s.EndOfStream)
        {
            var line = s.ReadLine().Split(' ');

            var dir = line[0];
            var cnt = int.Parse(line[1]);

            for (int i = 0; i < cnt; i++)
            {
                switch (dir)
                {
                    case "R":
                        snake[0].x++;
                        break;
                    case "L":
                        snake[0].x--;
                        break;
                    case "U":
                        snake[0].y++;
                        break;
                    case "D":
                        snake[0].y--;
                        break;
                    default:
                        break;
                }

                Move(snake);

                hs.Add(snake[snake.Length-1]);
            }
        }

        Console.WriteLine(hs.Count);
    }

    private static void Move(Point[] snake)
    {
        for (int i = 1; i < snake.Length; i++)
        {
            var isXMove = Math.Abs(snake[i - 1].x - snake[i].x) > 1;
            var isYMove = Math.Abs(snake[i - 1].y - snake[i].y) > 1;

            if (isXMove && isYMove)
            {
                snake[i].x = snake[i - 1].x - Math.Sign(snake[i - 1].x - snake[i].x);
                snake[i].y = snake[i - 1].y - Math.Sign(snake[i - 1].y - snake[i].y);
            }
            else if (isXMove)
            {
                snake[i].x = snake[i-1].x - Math.Sign(snake[i-1].x - snake[i].x);

                if (snake[i-1].y != snake[i].y)
                {
                    snake[i].y = snake[i-1].y;
                }
            }
            else if (isYMove)
            {
                snake[i].y = snake[i-1].y - Math.Sign(snake[i-1].y - snake[i].y);

                if (snake[i-1].x != snake[i].x)
                {
                    snake[i].x = snake[i-1].x;
                }
            }
            else
            {
                return;
            }
        }
    }

    class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as Point;

            return this.x == other.x && this.y == other.y;
        }
    }
}