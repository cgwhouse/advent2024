using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent2024.Days;

public class Day4(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var result = 0;

        var matrix = new List<ImmutableList<char>>();

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
        var result = 0;

        var matrix = new List<ImmutableList<char>>();

        foreach (var line in InputFromFile)
            matrix.Add([.. line.ToCharArray()]);

        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (matrix[i][j] != 'A')
                    continue;

                ImmutableList<(int Row, int Column)> combos =
                [
                    // Top Left
                    (i - 1, j - 1),
                    // Bottom Right
                    (i + 1, j + 1),
                    // Top Right
                    (i - 1, j + 1),
                    // Bottom Left
                    (i + 1, j - 1),
                ];

                try
                {
                    if (
                        // Top Left and Bottom Right have to each be M and S
                        (
                            (
                                matrix[combos[0].Row][combos[0].Column] == 'M'
                                && matrix[combos[1].Row][combos[1].Column] == 'S'
                            )
                            || (
                                matrix[combos[0].Row][combos[0].Column] == 'S'
                                && matrix[combos[1].Row][combos[1].Column] == 'M'
                            )
                        )
                        &&
                        // This must also be the case for Top Right and Bottom Left
                        (
                            (
                                matrix[combos[2].Row][combos[2].Column] == 'M'
                                && matrix[combos[3].Row][combos[3].Column] == 'S'
                            )
                            || (
                                matrix[combos[2].Row][combos[2].Column] == 'S'
                                && matrix[combos[3].Row][combos[3].Column] == 'M'
                            )
                        )
                    )
                        result++;
                }
                catch (ArgumentOutOfRangeException) { }
            }
        }

        return result.ToString();
    }
}
