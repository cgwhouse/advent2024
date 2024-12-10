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
                row.Add(int.Parse(n.ToString()));

            board.Add(row);
        }

        var trailheadDict = new Dictionary<(int Row, int Column), HashSet<(int Row, int Column)>>();

        // Gather trailheads as keys
        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Count; j++)
            {
                if (board[i][j] == 0)
                    trailheadDict[(i, j)] = [];
            }
        }

        foreach (var trailhead in trailheadDict.Keys)
        {
            var routeStack = new Stack<(int Row, int Column)>();
            routeStack.Push((trailhead.Row, trailhead.Column));

            while (routeStack.Count > 0)
            {
                var (Row, Column) = routeStack.Pop();
                var targetValue = board[Row][Column] + 1;

                var directionsToTry = ImmutableList.Create(
                    (Row + 1, Column),
                    (Row - 1, Column),
                    (Row, Column + 1),
                    (Row, Column - 1)
                );

                foreach (var direction in directionsToTry)
                {
                    try
                    {
                        if (board[direction.Item1][direction.Item2] == targetValue)
                        {
                            if (targetValue == 9)
                                trailheadDict[trailhead].Add((direction.Item1, direction.Item2));
                            else
                                routeStack.Push((direction.Item1, direction.Item2));
                        }
                    }
                    catch (ArgumentOutOfRangeException) { }
                }
            }
        }

        var result = 0;

        foreach (var trailhead in trailheadDict.Keys)
            result += trailheadDict[trailhead].Count;

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        var board = new List<List<int>>();

        foreach (var line in InputFromFile)
        {
            var row = new List<int>();

            foreach (var n in line)
                row.Add(int.Parse(n.ToString()));

            board.Add(row);
        }

        var trailheadDict = new Dictionary<(int Row, int Column), List<(int Row, int Column)>>();

        // Gather trailheads as keys
        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Count; j++)
            {
                if (board[i][j] == 0)
                    trailheadDict[(i, j)] = [];
            }
        }

        foreach (var trailhead in trailheadDict.Keys)
        {
            var routeStack = new Stack<(int Row, int Column)>();
            routeStack.Push((trailhead.Row, trailhead.Column));

            while (routeStack.Count > 0)
            {
                var (Row, Column) = routeStack.Pop();
                var targetValue = board[Row][Column] + 1;

                var directionsToTry = ImmutableList.Create(
                    (Row + 1, Column),
                    (Row - 1, Column),
                    (Row, Column + 1),
                    (Row, Column - 1)
                );

                foreach (var direction in directionsToTry)
                {
                    try
                    {
                        if (board[direction.Item1][direction.Item2] == targetValue)
                        {
                            if (targetValue == 9)
                                trailheadDict[trailhead].Add((direction.Item1, direction.Item2));
                            else
                                routeStack.Push((direction.Item1, direction.Item2));
                        }
                    }
                    catch (ArgumentOutOfRangeException) { }
                }
            }
        }

        var result = 0;

        foreach (var trailhead in trailheadDict.Keys)
            result += trailheadDict[trailhead].Count;

        return result.ToString();
    }
}
