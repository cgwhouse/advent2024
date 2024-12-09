using System;
using System.Collections.Generic;
using System.Linq;

namespace advent2024.Days;

public class Day7(int day) : BaseDay(day)
{
    private static readonly char[] PossibleOperators = { '+', '*' };

    protected override string SolveFirst()
    {
        var result = 0;

        foreach (var line in InputFromFile)
        {
            var pieces = line.Split(':');

            var desiredResult = long.Parse(pieces[0]);

            var nums = new List<long>();
            foreach (var s in pieces[1].Split(' '))
                if (long.TryParse(s, out var n))
                    nums.Add(n);

            // Try each possible combo of operators on nums,
            // if we find a desiredResult mark as success and keep going
            var opCombos = GenerateOperators(nums.Count() - 1);

            // Apply this set of ops to nums
            foreach (var combo in opCombos)
            {
                var opQueue = new Queue<char>(combo);
                var numQueue = new Queue<long>(nums);

                var curr = numQueue.Dequeue();

                while (numQueue.Any())
                {
                    var nextOp = opQueue.Dequeue();
                    var nextNum = numQueue.Dequeue();

                    if (nextOp == '+')
                        curr += nextNum;
                    else
                        curr *= nextNum;
                }

                if (curr == desiredResult)
                {
                    result++;
                    break;
                }
            }
        }

        return result.ToString();
    }

    private static List<char[]> GenerateOperators(int numberOfOperators)
    {
        var result = new List<char[]>();

        Dive("", 0, numberOfOperators, ref result);

        return result.Where(x => x.Count() == numberOfOperators).ToList();
    }

    private static void Dive(string prefix, int level, int maxlength, ref List<char[]> result)
    {
        level++;

        foreach (char o in PossibleOperators)
        {
            result.Add((prefix + o).ToCharArray());

            if (level < maxlength)
                Dive(prefix + o, level, maxlength, ref result);
        }
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
