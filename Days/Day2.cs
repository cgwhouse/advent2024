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

            if (nums[0] == nums[1])
                continue;

            var increasing = true;

            if (nums[0] > nums[1])
                increasing = false;

            for (int i = 0; i < nums.Count - 1; i++)
            {
                if (increasing && nums[i] >= nums[i + 1])
                    continue;

                if (!increasing && nums[i] <= nums[i + 1])
                    continue;

                if (Math.Abs(nums[i] - nums[i + 1]) < 1 || Math.Abs(nums[i] - nums[i + 1]) > 3)
                    continue;
            }

            result++;
        }

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
