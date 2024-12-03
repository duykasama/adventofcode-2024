using System.Text.RegularExpressions;

namespace AdventOfCode.Day3;

public static partial class Part2
{
    private const string InputPath = "Day3/input.txt";
    
    [GeneratedRegex(@"mul\(\d+,\d+\)|do\(\)|don't\(\)")]
    private static partial Regex MyRegex();
    
    public static void Solve()
    { 
        string fileContent = File.ReadAllText(InputPath);
        var result = 0;
        var disabled = false;
        
        MatchCollection matches = MyRegex().Matches(fileContent);
        foreach (Match match in matches)
        {
            disabled = match.Value switch
            {
                "don't()" => true,
                "do()" => false,
                _ => disabled
            };
            if (disabled) continue;
            result += CalculateResult(match.Value);
        }

        Console.WriteLine($"Result: {result}");
    }

    private static int CalculateResult(string instruction)
    {
        if (instruction.Length < "mul(0,0)".Length) return 0;
        string middlePart = instruction["mul(".Length..^1];
        string[] parameters = middlePart.Split(",");
        if (parameters.Length >= 2 && int.TryParse(parameters[0], out var param1) && int.TryParse(parameters[1], out var param2))
        {
            return param1 * param2;
        }

        return 0;
    }
}
