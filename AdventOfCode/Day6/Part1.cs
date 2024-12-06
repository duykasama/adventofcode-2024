namespace AdventOfCode.Day6;

public static class Part1
{
    private const string InputPath = "Day6/input.txt";
    private static int _xLength;
    private static int _yLength;
    private static readonly HashSet<string> PastPositions = [];
    
    public static void Solve()
    {
        string[] lines = File.ReadAllLines(InputPath);
         _xLength = lines.First().Length;
        _yLength = lines.Length;
        var matrix = new char[_xLength, _yLength];
        var currentDirection = "UP";
        
        Point guardPosition = new Point(-1, -1);

        for (int i = 0; i < _yLength; i++)
        {
            char[] chars = lines[i].ToCharArray();
            for (int j = 0; j < chars.Length; j++)
            {
                if (chars[j] != '.' && chars[j] != '#')
                {
                    guardPosition = new Point(j, i);
                }
                matrix[j, i] = chars[j];
            }
        }

        PastPositions.Add($"{guardPosition.X},{guardPosition.Y}");
        while (guardPosition.X != -1 && guardPosition.Y != -1)
        {
            if (currentDirection == "UP")
            {
                guardPosition = GoUp(guardPosition, matrix);
                currentDirection = "RIGHT";
            }
            
            if (currentDirection == "DOWN")
            {
                guardPosition = GoDown(guardPosition, matrix);
                currentDirection = "LEFT";
            }
            
            if (currentDirection == "LEFT")
            {
                guardPosition = GoLeft(guardPosition, matrix);
                currentDirection = "UP";
            }

            if (currentDirection != "RIGHT") continue;
            guardPosition = GoRight(guardPosition, matrix);
            currentDirection = "DOWN";
        }
        
        Console.WriteLine($"Result: {PastPositions.Count+1}"); // also count the last position
    }

    private static Point GoUp(Point currentPosition, char[,] matrix)
    {
        bool outOfMap = false;
        int objectPosition = -1;
        for (int i = currentPosition.Y-1; i >= 0; i--)
        {
            if (matrix[currentPosition.X, i] == '#')
            {
                objectPosition = i;
                break;
            }
            PastPositions.Add($"{currentPosition.X},{i+1}");
            if (i == 0) outOfMap = true;
        }

        return outOfMap ? new Point(-1, -1) : new Point(currentPosition.X, objectPosition + 1);
    }

    private static Point GoDown(Point currentPosition, char[,] matrix)
    {
        bool outOfMap = false;
        int objectPosition = -1;
        for (int i = currentPosition.Y+1; i < _yLength; i++)
        {
            if (matrix[currentPosition.X, i] == '#')
            {
                objectPosition = i;
                break;
            }
            PastPositions.Add($"{currentPosition.X},{i-1}");
            if (i == _yLength-1) outOfMap = true;
        }

        return outOfMap ? new Point(-1, -1) : new Point(currentPosition.X, objectPosition - 1);
    }

    private static Point GoLeft(Point currentPosition, char[,] matrix)
    {
        bool outOfMap = false;
        int objectPosition = -1;
        for (int i = currentPosition.X-1; i >= 0; i--)
        {
            if (matrix[i, currentPosition.Y] == '#')
            {
                objectPosition = i;
                break;
            }
            PastPositions.Add($"{i+1},{currentPosition.Y}");
            if (i == 0) outOfMap = true;
        }

        return outOfMap ? new Point(-1, -1) : new Point(objectPosition+1, currentPosition.Y);
    }

    private static Point GoRight(Point currentPosition, char[,] matrix)
    {
        bool outOfMap = false;
        int objectPosition = -1;
        for (int i = currentPosition.X+1; i < _xLength; i++)
        {
            if (matrix[i, currentPosition.Y] == '#')
            {
                objectPosition = i;
                break;
            }
            PastPositions.Add($"{i-1},{currentPosition.Y}");
            if (i == _xLength-1) outOfMap = true;
        }

        return outOfMap ? new Point(-1, -1) : new Point(objectPosition-1, currentPosition.Y);
    }

    private struct Point {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}