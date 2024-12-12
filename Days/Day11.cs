using System.Collections.Generic;
using System.Linq;

namespace advent2024.Days;

public class Day11(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        return Solve(numberOfBlinks: 25);
    }

    protected override string SolveSecond()
    {
        return Solve(numberOfBlinks: 75);
    }

    private string Solve(int numberOfBlinks)
    {
        var currentCounts = new Dictionary<long, long>();

        // Build initial set of stones
        foreach (var stone in InputFromFile.First().Split(" ").Select(long.Parse).ToList())
        {
            if (!currentCounts.TryAdd(stone, 1))
                currentCounts[stone] += 1;
        }

        // Blink X number of times
        for (int i = 0; i < numberOfBlinks; i++)
        {
            // We will start building the next generation
            var nextCounts = new Dictionary<long, long>();

            foreach (var stone in currentCounts.Keys)
            {
                if (stone == 0)
                {
                    if (!nextCounts.TryAdd(1, currentCounts[stone]))
                        nextCounts[1] += currentCounts[stone];
                }
                else if (stone.ToString().Length % 2 == 0)
                {
                    var first = long.Parse(stone.ToString()[..(stone.ToString().Length / 2)]);
                    var second = long.Parse(stone.ToString()[(stone.ToString().Length / 2)..]);

                    if (!nextCounts.TryAdd(first, currentCounts[stone]))
                        nextCounts[first] += currentCounts[stone];

                    if (!nextCounts.TryAdd(second, currentCounts[stone]))
                        nextCounts[second] += currentCounts[stone];
                }
                else
                {
                    var calc = stone * 2024;

                    if (!nextCounts.TryAdd(calc, currentCounts[stone]))
                        nextCounts[stone] += currentCounts[stone];
                }
            }

            currentCounts = nextCounts;
        }

        return currentCounts.Values.Sum(x => x).ToString();
    }
}
