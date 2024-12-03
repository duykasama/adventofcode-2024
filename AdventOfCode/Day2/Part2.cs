namespace AdventOfCode.Day2;

public static class Part2
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
            List<int> sequence = [..parts.Select(int.Parse)];
            var trend = 0;
            var isSafe = true;
            var canSkip = true;
            var loopCount = 0;
            for (int i = 0; i < sequence.Count; i++)
            {
                ++loopCount;
                int previous = sequence[i];
                if (i + 1 >= sequence.Count) break;
                if (IsSafe(previous, sequence[i+1], ref trend)) continue; 

                if (!canSkip)
                {
                    isSafe = false;
                    break;
                }

                canSkip = false;
                if (i + 2 >= sequence.Count) break;
                if (loopCount <= 2) trend = 0;
                if (IsSafe(previous, sequence[i + 2], ref trend))
                {
                    i++;
                    continue;
                }
                if (i - 1 < 0) break;
                if (loopCount <= 2) trend = 0;
                if (IsSafe(sequence[i-1], sequence[i+1], ref trend)) continue;
                
                isSafe = false;
                break;
            }
            
            if (isSafe) safeCount++;
        }
        
        Console.WriteLine($"Result: {safeCount}");
    }

    private static bool IsSafe(int numb1, int numb2, ref int trend)
    {
        int difference = numb1 - numb2;
        int differenceAbs = Math.Abs(difference);
        if (trend == 0)
        {
            switch (difference)
            {
                case > 0:
                    trend = 1;
                    break;
                case < 0:
                    trend = -1;
                    break;
                default:
                    return false;
            }

            return differenceAbs is <= 3 and > 0;
        }
        if (difference * trend <= 0) return false;
        return differenceAbs is <= 3 and > 0;
    }
}
