using System;
using System.Collections.Generic;

namespace advent2024.Days;

public class Day2(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var result = 0;

        foreach (var line in InputFromFile)
        {
            var nums = new List<int>();
            foreach (var n in line.Split(' '))
                nums.Add(int.Parse(n));

            if (ReportIsSafe(nums))
                result++;
        }

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        var result = 0;

        foreach (var line in InputFromFile)
        {
            var nums = new List<int>();
            foreach (var n in line.Split(' '))
                nums.Add(int.Parse(n));

            if (ReportIsSafe(nums))
                result++;
            else
            {
                for (int i = 0; i < nums.Count; i++)
                {
                    // Build a copy of nums without nums[i]
                    var testNums = new List<int>();

                    for (int j = 0; j < nums.Count; j++)
                    {
                        if (j == i)
                            continue;

                        testNums.Add(nums[j]);
                    }

                    if (ReportIsSafe(testNums))
                    {
                        result++;
                        break;
                    }
                }
            }
        }

        return result.ToString();
    }

    private bool ReportIsSafe(List<int> nums)
    {
        if (nums[0] == nums[1])
            return false;

        var increasing = true;

        if (nums[0] > nums[1])
            increasing = false;

        var valid = true;

        for (int i = 0; i < nums.Count - 1; i++)
        {
            var diff = Math.Abs(nums[i] - nums[i + 1]);

            if (
                increasing && nums[i] >= nums[i + 1]
                || !increasing && nums[i] <= nums[i + 1]
                || diff < 1
                || diff > 3
            )
            {
                valid = false;
                break;
            }
        }

        return valid;
    }
}
