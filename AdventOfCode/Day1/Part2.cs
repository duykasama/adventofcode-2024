namespace AdventOfCode.Day1;

public static class Part2
{
    private const string InputPath = "Day1/input.txt";
    
    public static void Solve()
    {
        using StreamReader streamReader = File.OpenText(InputPath);
        List<int> leftNumbers = [];
        List<int> rightNumbers = [];
        while (true)
        {
            string? line = streamReader.ReadLine();
            if (line == null) break;
            IEnumerable<string> parts = line.Split(" ").Where(p => !string.IsNullOrWhiteSpace(p));
            IEnumerable<string> partsAsArray = parts as string[] ?? parts.ToArray();
            bool validLeftNumber = int.TryParse(partsAsArray.FirstOrDefault(), out int leftNumber);
            bool validRightNumber = int.TryParse(partsAsArray.LastOrDefault(), out int rightNumber);
            if (!validLeftNumber || !validRightNumber) continue;
            leftNumbers.Add(leftNumber);
            rightNumbers.Add(rightNumber);
        }

        int totalSimilarityScore = (
                from number 
                in leftNumbers let appearancesCount = rightNumbers.Count(n => n == number)
                select number * appearancesCount
            ).Sum();

        Console.WriteLine($"Result: {totalSimilarityScore}");
    }
}