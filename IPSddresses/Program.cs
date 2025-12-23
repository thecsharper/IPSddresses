var ips = Solution.RestoreIpAddresses("2542540123");
Console.WriteLine(string.Join("\n", ips));
// Output:
// 254.25.40.123
// 254.254.0.123

Console.WriteLine();

ips = Solution.RestoreIpAddresses("0000");
Console.WriteLine(string.Join("\n", ips));
// 0.0.0.0

ips = Solution.RestoreIpAddresses("101023");
Console.WriteLine(string.Join("\n", ips));
// 1.0.10.23
// 1.0.102.3
// 10.1.0.23
// 10.10.2.3
// 101.0.2.3

public class Solution
{
    public static IList<string> RestoreIpAddresses(string input)
    {
        var result = new List<string>();
        if (input.Length < 4 || input.Length > 12)
        {
            return result;
        }

        Backtrack(input, 0, [], result);

        return result;
    }

    private static void Backtrack(string input, int start, List<string> current, List<string> result)
    {
        // If we have 4 parts and we've used the entire string → valid IP
        if (current.Count == 4)
        {
            if (start == input.Length)
            {
                result.Add(string.Join(".", current));
            }

            return;
        }

        // Try taking 1, 2, or 3 digits for the current segment
        for (var len = 1; len <= 3; len++)
        {
            if (start + len > input.Length)
            {
                break;
            }

            var segment = input.Substring(start, len);

            // Skip invalid cases:
            // - Leading zero not allowed unless the number is just "0"
            if (segment.StartsWith("0") && segment.Length > 1)
            {
                continue;
            }

            // - Number must be <= 255
            if (int.Parse(segment) > 255)
            {
                continue;
            }

            // Add and recurse
            current.Add(segment);
            
            Backtrack(input, start + len, current, result);
            
            current.RemoveAt(current.Count - 1); // backtrack
        }
    }
}