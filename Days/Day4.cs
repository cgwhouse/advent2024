using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent2024.Days;

public class Day4(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var result = 0;

        var matrix = new List<List<char>>();

        foreach (var line in InputFromFile)
            matrix.Add([.. line.ToCharArray()]);

        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (matrix[i][j] != 'X')
                    continue;

                ImmutableList<ImmutableList<(int Row, int Column)>> combos =
                [
                    // East
                    [(i, j + 1), (i, j + 2), (i, j + 3)],
                    // Southeast
                    [(i + 1, j + 1), (i + 2, j + 2), (i + 3, j + 3)],
                    // South
                    [(i + 1, j), (i + 2, j), (i + 3, j)],
                    // Southwest
                    [(i + 1, j - 1), (i + 2, j - 2), (i + 3, j - 3)],
                    // West
                    [(i, j - 1), (i, j - 2), (i, j - 3)],
                    // Northwest
                    [(i - 1, j - 1), (i - 2, j - 2), (i - 3, j - 3)],
                    // North
                    [(i - 1, j), (i - 2, j), (i - 3, j)],
                    // Northeast
                    [(i - 1, j + 1), (i - 2, j + 2), (i - 3, j + 3)],
                ];

                // Look for MAS in each possible direction
                foreach (var combo in combos)
                {
                    try
                    {
                        if (
                            $"{matrix[combo[0].Row][combo[0].Column]}{matrix[combo[1].Row][combo[1].Column]}{matrix[combo[2].Row][combo[2].Column]}"
                            == "MAS"
                        )
                            result++;
                    }
                    catch (ArgumentOutOfRangeException) { }
                }
            }
        }

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
