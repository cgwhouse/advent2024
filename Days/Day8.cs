using System;
using System.Collections.Generic;

namespace advent2024.Days;

public class Day8(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var board = new List<char[]>();
        foreach (var line in InputFromFile)
        {
            board.Add(line.ToCharArray());
        }

        var stationDict = new Dictionary<char, List<(int, int)>>();

        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] != '.')
                {
                    var stationChar = board[i][j];

                    if (stationDict.ContainsKey(stationChar))
                        stationDict[stationChar].Add((i, j));
                    else
                        stationDict[stationChar] = new List<(int, int)>() { (i, j) };
                }
            }
        }

        var antinodes = new List<(int, int)>();

        foreach (var stationChar in stationDict.Keys)
        {
            // Need at least 2 stations for an antinode
            if (stationDict[stationChar].Count < 2)
                continue;

            // Take each combo of stations of this type and determine their antinodes
            var stationCoords = stationDict[stationChar];
            for (int i = 0; i < stationCoords.Count; i++)
            {
                for (int j = 0; j < stationCoords.Count; j++)
                {
                    if (j == i)
                        continue;

                    antinodes.AddRange(DetermineAntinodes(stationCoords[i], stationCoords[j]));
                }
            }
        }

        var uniqueAntinodeLocations = new HashSet<(int, int)>();

        foreach (var possibleAntinode in antinodes)
        {
            try
            {
                var test = board[possibleAntinode.Item1][possibleAntinode.Item2];
                uniqueAntinodeLocations.Add((possibleAntinode.Item1, possibleAntinode.Item2));
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        Console.WriteLine("here");

        return uniqueAntinodeLocations.Count.ToString();
    }

    private List<(int, int)> DetermineAntinodes(
        (int Row, int Column) firstPoint,
        (int Row, int Column) secondPoint
    )
    {
        if (firstPoint.Row == 2 && firstPoint.Column == 5 && secondPoint.Row == 4 && secondPoint.Column == 4)
        {
            Console.WriteLine("here");
        }

        var result = new List<(int, int)>();
        // (x, y) is row, column indexes in the list of lists?
        //
        // points (3, 4) and (5, 5) create antinodes (1, 3) and (7, 6)
        var rowDiff = Math.Abs(firstPoint.Row - secondPoint.Row); // 2
        var colDiff = Math.Abs(firstPoint.Column - secondPoint.Column); // 1

        // Take first point and create the antinode using it as a frame of reference
        // Start at the second point and "move to" the first point, but double the values
        int firstAntiRow;
        int firstAntiColumn;

        if (firstPoint.Row < secondPoint.Row)
        {
            firstAntiRow = secondPoint.Row - (rowDiff * 2);
        }
        else if (firstPoint.Row > secondPoint.Row)
        {
            firstAntiRow = secondPoint.Row + (rowDiff * 2);
        }
        else
        {
            firstAntiRow = firstPoint.Row;
        }

        if (firstPoint.Column < secondPoint.Column)
        {
            firstAntiColumn = secondPoint.Column - (colDiff * 2);
        }
        else if (firstPoint.Column > secondPoint.Column)
        {
            firstAntiColumn = secondPoint.Column + (colDiff * 2);
        }
        else
        {
            firstAntiColumn = firstPoint.Column;
        }

        result.Add((firstAntiRow, firstAntiColumn));

        // Now start at the first point and move to the second point
        int secondAntiRow;
        int secondAntiColumn;

        if (secondPoint.Row < firstPoint.Row)
        {
            secondAntiRow = firstPoint.Row - (rowDiff * 2);
        }
        else if (secondPoint.Row > firstPoint.Row)
        {
            secondAntiRow = firstPoint.Row + (rowDiff * 2);
        }
        else
        {
            secondAntiRow = firstPoint.Row;
        }

        if (secondPoint.Column < firstPoint.Column)
        {
            secondAntiColumn = firstPoint.Column - (colDiff * 2);
        }
        else if (secondPoint.Column > firstPoint.Column)
        {
            secondAntiColumn = firstPoint.Column + (colDiff * 2);
        }
        else
        {
            secondAntiColumn = firstPoint.Column;
        }

        result.Add((secondAntiRow, secondAntiColumn));

        return result;
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
