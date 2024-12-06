namespace AdventOfCode.Day2;

public static class Part2
{
    private const string InputPath = "Day2/input.txt";
    
    public static void Solve()
    {
        IEnumerable<string> lines = File.ReadLines(InputPath);
        var safeCount = 0;
        
        foreach (string line in lines)
        {
            List<int> numbers = line.Split(' ').Select(int.Parse).ToList();

            if (IsValidSequence(numbers)) safeCount++;
        }
        
        Console.WriteLine($"Result: {safeCount}");
    }

    private static bool IsValidSequence(List<int> numbers)
    {
        if (CheckSequence(numbers))
            return true;

        for (int i = 0; i < numbers.Count; i++)
        {
            var tempList = new List<int>(numbers);
            tempList.RemoveAt(i);
            if (CheckSequence(tempList))
                return true;
        }

        return false;

        bool CheckSequence(List<int> sequence)
        {
            if (!IsIncreasingOrDecreasing(sequence)) return false;
            
            for (int i = 0; i < sequence.Count - 1; i++)
            {
                int diff = Math.Abs(sequence[i + 1] - sequence[i]);
                if (diff is < 1 or > 3)
                    return false;
            }
            return true;
        }

        bool IsIncreasingOrDecreasing(List<int> sequence)
        {
            var copy = new List<int>(sequence);
            copy.Sort();
            if (IsSame(sequence, copy)) return true;
            copy.Reverse();
            return IsSame(sequence, copy);

            bool IsSame(List<int> l1, List<int> l2)
            {
                return !l1.Where((t, i) => t != l2[i]).Any();
            }
        }
    }
}
