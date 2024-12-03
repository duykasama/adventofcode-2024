namespace AdventOfCode.Day2;

public static class Part1
{
    private const string InputPath = "Day2/input.txt";
    
    public static void Solve()
    {
        using StreamReader streamReader = File.OpenText(InputPath);
        var safeCount = 0;
        
        while (true)
        {
            string? line = streamReader.ReadLine();
            if (line == null) break;
            IEnumerable<string> parts = line.Split(" ").Where(p => !string.IsNullOrWhiteSpace(p));
            IEnumerable<int> sequence = parts.Select(int.Parse);
            var trend = 0;
            bool isSafe = true;
            using var enumerator = sequence.GetEnumerator();
            bool canMove = enumerator.MoveNext();
            while (canMove)
            {
                int previous = enumerator.Current;
                canMove = enumerator.MoveNext();
                if (!canMove) break;
                if (IsSafe(previous, enumerator.Current, ref trend)) continue;
                    
                isSafe = false;
                break;
            }

            if (isSafe) safeCount++;
        }
        
        Console.WriteLine($"Result: {safeCount}");
    }

    private static bool IsSafe(int numb1, int numb2, ref int trend)
    {
        if (trend == 0)
        {
            if (numb1 - numb2 > 0) trend = 1;
            else trend = -1;
            return Math.Abs(numb1 - numb2) <= 3 && Math.Abs(numb1 - numb2) > 0;
        }
        if ((numb1 - numb2) * trend <= 0) return false;
        return Math.Abs(numb1 - numb2) <= 3 && Math.Abs(numb1 - numb2) > 0;
    }
}