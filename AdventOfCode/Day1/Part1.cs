namespace AdventOfCode.Day1;

public static class Part1
{
    private const string InputPath = "Day1/input.txt";
    
    public static void Solve()
    {
        using StreamReader streamReader = File.OpenText(InputPath);
        List<int> differences = [];
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

        leftNumbers.Sort();
        rightNumbers.Sort();
        
        using (List<int>.Enumerator leftEnumerator = leftNumbers.GetEnumerator())
        using (List<int>.Enumerator rightEnumerator = rightNumbers.GetEnumerator())
        {
            while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
            {
                int difference = Math.Abs(leftEnumerator.Current - rightEnumerator.Current);
                differences.Add(difference);
            }
        }

        int result = differences.Sum();
        Console.WriteLine($"Result: {result}");
    }
}