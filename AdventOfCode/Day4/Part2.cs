using System.Text;

namespace AdventOfCode.Day4;

public static class Part2
{
    private const string InputPath = "Day4/input.txt";
    
    public static void Solve()
    {
        string[] lines = File.ReadAllLines(InputPath);
        int xLength = lines.First().Length;
        int yLength = lines.Length;
        var matrix = new char[xLength, yLength];
        var xmasCount = 0;

        for (var i = 0; i < yLength; i++)
        {
            char[] chars = lines[i].ToCharArray();
            for (var j = 0; j < chars.Length; j++)
            {
                matrix[j, i] = chars[j];
            }
        }

        for (var i = 0; i < xLength - 2; i++)
        {
            for (var j = 0; j < yLength - 2; j++)
            {
                var innerMatrix = new char[3, 3];
                
                for (var k = 0; k < 3; k++)
                {
                    for (var l = 0; l < 3; l++)
                    {
                        innerMatrix[k, l] = matrix[i + k, j + l];
                    }
                }

                if (IsXmas(innerMatrix)) xmasCount++;
            }
        }
        
        Console.WriteLine($"Result: {xmasCount}");
    }
    
    private static bool IsXmas(char[,] matrix)
    {
        var diagonal1 = new StringBuilder().Append(matrix[0, 0]).Append(matrix[1, 1]).Append(matrix[2, 2]).ToString();
        var diagonal2 = new StringBuilder().Append(matrix[0, 2]).Append(matrix[1, 1]).Append(matrix[2, 0]).ToString();

        return IsMas(diagonal1) && IsMas(diagonal2);
    }

    private static bool IsMas(string s)
    {
        return string.Equals(s, "MAS", StringComparison.OrdinalIgnoreCase) || 
               string.Equals(s, "SAM", StringComparison.OrdinalIgnoreCase);
    }
}