namespace AdventOfCode.Day5;

public static class Part1
{
    private const string InputPath = "Day5/input.txt";
    public static void Solve()
    {
        string[] lines = File.ReadAllLines(InputPath);
        Dictionary<int, List<int>> rules = new();
        List<List<int>> correctUpdates = [];
        bool metSectionSeparator = false;
        
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line)) metSectionSeparator = true;
            
            if (!metSectionSeparator)
            {
                string[] parts = line.Split('|');
                int left = int.Parse(parts[0]);
                int right = int.Parse(parts[1]);

                if (rules.ContainsKey(right))
                {
                    rules.TryGetValue(right, out List<int>? deps);
                    deps?.Add(left);
                }
                else
                {
                    rules.Add(right, [left]);
                }
                
            }
            else
            {
                List<int> pages = line.Split(',').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();
                bool isValid = true;
                
                for (int i = 0; i < pages.Count-1; i++)
                {
                    
                    int currentPage = pages[i];
                    for (int j = i+1; j < pages.Count; j++)
                    {
                        int subsequentPage = pages[j];
                        rules.TryGetValue(currentPage, out List<int>? pageRules);
                        if (pageRules?.Contains(subsequentPage) ?? false) isValid = false;
                    }
                    
                }
                
                if (isValid) correctUpdates.Add(pages);
            }
        }

        int result = 0;
        foreach (var correctUpdate in correctUpdates.Where(u => u.Count != 0))
        {
            var middleIndex = (int)Math.Floor(correctUpdate.Count/2.0);
            result += correctUpdate.ToArray()[middleIndex];
        }
        
        Console.WriteLine($"Result: {result}");
    }
}