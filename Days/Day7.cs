using System.Collections.Generic;
using System.Linq;

namespace advent2024.Days;

public class Day7(int day) : BaseDay(day)
{
    private static readonly char[] PossibleOperatorsFirst = ['+', '*'];
    private static readonly char[] PossibleOperatorsSecond = ['+', '*', '|'];

    protected override string SolveFirst()
    {
        long result = 0;

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
            var opCombos = GenerateOperators(nums.Count - 1, PossibleOperatorsFirst);

            // Apply this set of ops to nums
            foreach (var combo in opCombos)
            {
                var opQueue = new Queue<char>(combo);
                var numQueue = new Queue<long>(nums);

                var curr = numQueue.Dequeue();

                while (numQueue.Count != 0)
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
                    result += desiredResult;
                    break;
                }
            }
        }

        return result.ToString();
    }

    private static List<char[]> GenerateOperators(int numberOfOperators, char[] possibleOperators)
    {
        var result = new List<char[]>();

        Dive("", 0, numberOfOperators, ref result, possibleOperators);

        return result.Where(x => x.Length == numberOfOperators).ToList();
    }

    // I had to steal this from SO :(
    private static void Dive(
        string prefix,
        int level,
        int maxLength,
        ref List<char[]> result,
        char[] possibleOperators
    )
    {
        level++;

        foreach (char o in possibleOperators)
        {
            result.Add((prefix + o).ToCharArray());

            if (level < maxLength)
                Dive(prefix + o, level, maxLength, ref result, possibleOperators);
        }
    }

    protected override string SolveSecond()
    {
        long result = 0;

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
            var opCombos = GenerateOperators(nums.Count - 1, PossibleOperatorsSecond);

            // Apply this set of ops to nums
            foreach (var combo in opCombos)
            {
                var opQueue = new Queue<char>(combo);
                var numQueue = new Queue<long>(nums);

                var curr = numQueue.Dequeue();

                while (numQueue.Count != 0)
                {
                    var nextOp = opQueue.Dequeue();
                    var nextNum = numQueue.Dequeue();

                    if (nextOp == '+')
                        curr += nextNum;
                    else if (nextOp == '*')
                        curr *= nextNum;
                    else
                        curr = long.Parse($"{curr}{nextNum}");
                }

                if (curr == desiredResult)
                {
                    result += desiredResult;
                    break;
                }
            }
        }

        return result.ToString();
    }
}
