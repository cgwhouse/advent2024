using System;
using System.Collections.Generic;

namespace advent2024.Days;

public class Day10(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var board = new List<List<int>>();

        foreach (var line in InputFromFile)
        {
            var row = new List<int>();

            foreach (var n in line)
                row.Add(n);

            board.Add(row);
        }

        // Keys are (x,y) of trailheads
        // Values are the 9s that can be reached from each trailhead key
        var trailheadDict = new Dictionary<(int Row, int Column), HashSet<(int Row, int Column)>>();

        // Gather trailheads as keys
        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Count; j++)
            {
                if (board[i][j] == 0)
                    trailheadDict[(i, j)] = new HashSet<(int Row, int Column)>();
            }
        }

        var result = 0;

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
