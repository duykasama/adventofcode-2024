namespace AdventOfCode.Day6;

public static class Part2
{
    private const string InputPath = "Day6/test.txt";
    private static int _xLength;
    private static int _yLength;
    private static string _currentDirection = "UP";
    private static readonly HashSet<string> PastPositions = [];
    
    public static void Solve()
    {
        string[] lines = File.ReadAllLines(InputPath);
         _xLength = lines.First().Length;
        _yLength = lines.Length;
        var matrix = new char[_xLength, _yLength];
        _currentDirection = "UP";
        
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

        while (guardPosition.X != -1 && guardPosition.Y != -1)
        {
            if (_currentDirection == "UP")
            {
                guardPosition = GoUp(guardPosition, matrix, true);
                _currentDirection = "RIGHT";
            }
            
            if (_currentDirection == "DOWN")
            {
                guardPosition = GoDown(guardPosition, matrix, true);
                _currentDirection = "LEFT";
            }
            
            if (_currentDirection == "LEFT")
            {
                guardPosition = GoLeft(guardPosition, matrix, true);
                _currentDirection = "UP";
            }

            if (_currentDirection != "RIGHT") continue;
            guardPosition = GoRight(guardPosition, matrix, true);
            _currentDirection = "DOWN";
        }

        int result = 0;
        foreach (string pastPosition in PastPositions)
        {
            _currentDirection = "UP";
            
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
            
            string[] parts = pastPosition.Split(',');
            char oldValue = matrix[int.Parse(parts[0]), int.Parse(parts[0])];
            matrix[int.Parse(parts[0]), int.Parse(parts[0])] = '#'; 
            
            while (guardPosition.X != -1 && guardPosition.Y != -1)
            {
                
                if (_currentDirection == "UP")
                {
                    guardPosition = GoUp(guardPosition, matrix, false);
                    _currentDirection = "RIGHT";
                }
                
                if (_currentDirection == "DOWN")
                {
                    guardPosition = GoDown(guardPosition, matrix, false);
                    _currentDirection = "LEFT";
                }
                
                if (_currentDirection == "LEFT")
                {
                    guardPosition = GoLeft(guardPosition, matrix, false);
                    _currentDirection = "UP";
                }

                if (_currentDirection != "RIGHT") continue;
                guardPosition = GoRight(guardPosition, matrix, false);
                _currentDirection = "DOWN";

                if ($"{guardPosition.X},{guardPosition.Y},{_currentDirection}" != pastPosition) continue;
                result++;
                break;
            }

            matrix[int.Parse(parts[0]), int.Parse(parts[0])] = oldValue;
        }
        
        
        Console.WriteLine($"Result: {result}"); // also count the last position
    }

    private static Point GoUp(Point currentPosition, char[,] matrix, bool addToPastPosition)
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
            if (addToPastPosition) PastPositions.Add($"{currentPosition.X},{i+1},{_currentDirection}");
            if (i == 0) outOfMap = true;
        }

        return outOfMap ? new Point(-1, -1) : new Point(currentPosition.X, objectPosition + 1);
    }

    private static Point GoDown(Point currentPosition, char[,] matrix, bool addToPastPosition)
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
            if (addToPastPosition) PastPositions.Add($"{currentPosition.X},{i-1},{_currentDirection}");
            if (i == _yLength-1) outOfMap = true;
        }

        return outOfMap ? new Point(-1, -1) : new Point(currentPosition.X, objectPosition - 1);
    }

    private static Point GoLeft(Point currentPosition, char[,] matrix, bool addToPastPosition)
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
            if (addToPastPosition) PastPositions.Add($"{i+1},{currentPosition.Y},{_currentDirection}");
            if (i == 0) outOfMap = true;
        }

        return outOfMap ? new Point(-1, -1) : new Point(objectPosition+1, currentPosition.Y);
    }

    private static Point GoRight(Point currentPosition, char[,] matrix, bool addToPastPosition)
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
            if (addToPastPosition) PastPositions.Add($"{i-1},{currentPosition.Y},{_currentDirection}");
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
