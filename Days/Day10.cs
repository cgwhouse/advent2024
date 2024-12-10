using System;
using System.Collections.Generic;
using System.Collections.Immutable;

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

        // TODO: iterate through each trailhead and try to figure out its score
        foreach (var trailhead in trailheadDict.Keys)
        {
            // create collection of possible routes
            var routeStack = new Stack<(int Row, int Column)>();
            routeStack.Push((trailhead.Row, trailhead.Column));

            while (routeStack.Count > 0)
            {
                var current = routeStack.Pop();
                var targetValue = board[current.Row][current.Column] + 1;

                var directionsToTry = ImmutableList.Create(
                    (current.Row + 1, current.Column),
                    (current.Row - 1, current.Column),
                    (current.Row, current.Column + 1),
                    (current.Row, current.Column - 1)
                );

                foreach (var direction in directionsToTry)
                {
                    try
                    {
                        if (board[direction.Item1][direction.Item2] == targetValue)
                        {
                            if (targetValue == 9)
                                trailheadDict[trailhead].Add((direction.Item1, direction.Item2));
                        }
                        else
                            routeStack.Push((direction.Item1, direction.Item2));
                    }
                    catch (ArgumentOutOfRangeException) { }
                }

                // Grab thing off the stack, look at the spots around it for the value + 1
                // if we find any with the target value, add them to the stack with the next target value
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
