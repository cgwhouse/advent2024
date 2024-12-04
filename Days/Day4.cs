using System;
using System.Collections.Generic;

namespace advent2024.Days;

public class Day4(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var result = 0;

        var matrix = new List<List<string>>();

        foreach (var line in InputFromFile)
            matrix.Add([.. line.Split("")]);

        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (matrix[i][j] != "X")
                    continue;

                // Look for MAS in each possible direction

                // East
                try
                {
                    if ($"{matrix[i][j + 1]}{matrix[i][j + 2]}{matrix[i][j + 3]}" == "MAS")
                        result += 1;
                }
                catch (Exception) { }

                // Southeast
                try
                {
                    if (
                        $"{matrix[i + 1][j + 1]}{matrix[i + 2][j + 2]}{matrix[i + 3][j + 3]}"
                        == "MAS"
                    )
                        result += 1;
                }
                catch (Exception) { }

                // South
                try
                {
                    if ($"{matrix[i + 1][j]}{matrix[i + 2][j]}{matrix[i + 3][j]}" == "MAS")
                        result += 1;
                }
                catch (Exception) { }

                // Southwest
                try
                {
                    if (
                        $"{matrix[i + 1][j - 1]}{matrix[i + 2][j - 2]}{matrix[i + 3][j - 3]}"
                        == "MAS"
                    )
                        result += 1;
                }
                catch (Exception) { }

                // West
                try
                {
                    if ($"{matrix[i][j - 1]}{matrix[i][j - 2]}{matrix[i][j - 3]}" == "MAS")
                        result += 1;
                }
                catch (Exception) { }

                // Northwest
                try
                {
                    if (
                        $"{matrix[i - 1][j - 1]}{matrix[i - 2][j - 2]}{matrix[i - 3][j - 3]}"
                        == "MAS"
                    )
                        result += 1;
                }
                catch (Exception) { }

                // North
                try
                {
                    if ($"{matrix[i - 1][j]}{matrix[i - 2][j]}{matrix[i - 3][j]}" == "MAS")
                        result += 1;
                }
                catch (Exception) { }

                // Northeast
                try
                {
                    if (
                        $"{matrix[i - 1][j + 1]}{matrix[i - 2][j + 2]}{matrix[i - 3][j + 3]}"
                        == "MAS"
                    )
                        result += 1;
                }
                catch (Exception) { }
            }
        }

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
