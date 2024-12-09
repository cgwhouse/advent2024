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
            var desiredResult = long.Parse(line.Split(':')[0]);

            Console.WriteLine(desiredResult);
        }

        var test = GenerateOperators(3);

        foreach (var thing in test)
        {
            Console.WriteLine(string.Join("", thing));
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
