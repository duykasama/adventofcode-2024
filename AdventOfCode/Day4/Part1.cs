using System.Text;

namespace AdventOfCode.Day4;

public static class Part1
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

        // count by rows
        for (var i = 0; i < yLength; i++)
        {
            for (var j = 0; j < xLength; j++)
            {
                if (j + 3 >= xLength) break;
                
                char one = matrix[j,i];
                char two = matrix[j+1,i];
                char three = matrix[j+2,i];
                char four = matrix[j+3,i];

                StringBuilder stringBuilder = new StringBuilder().Append(one).Append(two).Append(three).Append(four);
                
                if (IsXmas(stringBuilder.ToString())) xmasCount++;
            }
        }
        
        // count by columns
        for (var i = 0; i < xLength; i++)
        {
            for (var j = 0; j < yLength; j++)
            {
                if (j + 3 >= yLength) break;
                
                char one = matrix[i,j];
                char two = matrix[i,j+1];
                char three = matrix[i,j+2];
                char four = matrix[i,j+3];

                StringBuilder stringBuilder = new StringBuilder().Append(one).Append(two).Append(three).Append(four);
                
                if (IsXmas(stringBuilder.ToString())) xmasCount++;
            }
        }
        
        // count diagonally 1
        for (var i = 3; i < xLength; i++)
        {
            int w = i;
            var h = 0;
            List<char> chars = [];
            while (w >= 0)
            {
                char c = matrix[w, h];
                chars.Add(c);
                w--;
                h++;
            }

            for (var j = 0; j < chars.Count; j++)
            {
                if (j + 3 >= chars.Count) break;
                
                StringBuilder stringBuilder = new StringBuilder().Append(chars[j]).Append(chars[j+1]).Append(chars[j+2]).Append(chars[j+3]);
                
                if (IsXmas(stringBuilder.ToString())) xmasCount++;
            }
        }
        for (int i = xLength - 4; i > 0; i--)
        {
            int w = i;
            int h = yLength - 1;
            List<char> chars = [];
            while (w < xLength)
            {
                char c = matrix[w, h];
                chars.Add(c);
                w++;
                h--;
            }

            for (var j = 0; j < chars.Count; j++)
            {
                if (j + 3 >= chars.Count) break;
                
                StringBuilder stringBuilder = new StringBuilder().Append(chars[j]).Append(chars[j+1]).Append(chars[j+2]).Append(chars[j+3]);
                
                if (IsXmas(stringBuilder.ToString())) xmasCount++;
            }
        }
        
        // count diagonally 2
        for (var i = 3; i < xLength; i++)
        {
            int w = i;
            int h = yLength - 1;
            List<char> chars = [];
            while (w >= 0)
            {
                char c = matrix[w, h];
                chars.Add(c);
                w--;
                h--;
            }

            for (var j = 0; j < chars.Count; j++)
            {
                if (j + 3 >= chars.Count) break;
                
                StringBuilder stringBuilder = new StringBuilder().Append(chars[j]).Append(chars[j+1]).Append(chars[j+2]).Append(chars[j+3]);
                
                if (IsXmas(stringBuilder.ToString())) xmasCount++;
            }
        }
        for (int i = xLength - 4; i > 0; i--)
        {
            int w = i;
            var h = 0;
            List<char> chars = [];
            while (w < xLength)
            {
                char c = matrix[w, h];
                chars.Add(c);
                w++;
                h++;
            }

            for (var j = 0; j < chars.Count; j++)
            {
                if (j + 3 >= chars.Count) break;
                
                StringBuilder stringBuilder = new StringBuilder().Append(chars[j]).Append(chars[j+1]).Append(chars[j+2]).Append(chars[j+3]);
                
                if (IsXmas(stringBuilder.ToString())) xmasCount++;
            }
        }
        
        Console.WriteLine($"Result: {xmasCount}");
    }

    private static bool IsXmas(string s)
    {
        return string.Equals(s, "XMAS", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(s, "SAMX", StringComparison.OrdinalIgnoreCase);
    }
}