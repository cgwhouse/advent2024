using System;
using System.Collections.Generic;
using System.Linq;

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
        var left = new List<int>();
        var right = new List<int>();

        foreach (var line in InputFromFile)
        {
            left.Add(int.Parse(line.Split("   ")[0]));
            right.Add(int.Parse(line.Split("   ")[1]));
        }

        var result = 0;

        foreach (var num in left)
            result += num * right.Where(x => x == num).Count();

        return result.ToString();
    }
}
