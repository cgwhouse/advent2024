using System;
using System.Collections.Generic;

namespace advent2024.Days;

public class Day1(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var left = new List<int>();
        var right = new List<int>();

        foreach (var line in InputFromFile)
        {
            left.Add(int.Parse(line.Split("   ")[0]));
            right.Add(int.Parse(line.Split("   ")[1]));
        }

        left.Sort();
        right.Sort();

        var result = 0;

        for (int i = 0; i < left.Count; i++)
            result += Math.Abs(left[i] - right[i]);

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
