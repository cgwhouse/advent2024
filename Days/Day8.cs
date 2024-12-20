using System;
using System.Collections.Generic;

namespace advent2024.Days;

public class Day8(int day) : BaseDay(day)
{
    private const int NumberOfLoops = 100;

    protected override string SolveFirst()
    {
        var board = new List<char[]>();
        foreach (var line in InputFromFile)
            board.Add(line.ToCharArray());

        var stationDict = new Dictionary<char, List<(int, int)>>();

        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] != '.')
                {
                    var stationChar = board[i][j];

                    if (stationDict.TryGetValue(stationChar, out var stationList))
                        stationList.Add((i, j));
                    else
                        stationDict[stationChar] = [(i, j)];
                }
            }
        }

        var possibleAntinodes = new List<(int Row, int Column)>();

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

                    possibleAntinodes.AddRange(
                        DetermineAntinodesPart1(stationCoords[i], stationCoords[j])
                    );
                }
            }
        }

        var uniqueAntinodeLocations = new HashSet<(int Row, int Column)>();

        foreach (var (Row, Column) in possibleAntinodes)
        {
            try
            {
                var _ = board[Row][Column];

                uniqueAntinodeLocations.Add((Row, Column));
            }
            catch (Exception ex)
                when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException) { }
        }

        return uniqueAntinodeLocations.Count.ToString();
    }

    private static List<(int Row, int Column)> DetermineAntinodesPart1(
        (int Row, int Column) firstPoint,
        (int Row, int Column) secondPoint
    )
    {
        var result = new List<(int, int)>();

        var rowDiff = Math.Abs(firstPoint.Row - secondPoint.Row);
        var colDiff = Math.Abs(firstPoint.Column - secondPoint.Column);

        // Take first point and create the antinode using it as a frame of reference
        // Start at the second point and "move to" the first point, but double the values
        int firstAntiRow;
        int firstAntiColumn;

        if (firstPoint.Row < secondPoint.Row)
            firstAntiRow = secondPoint.Row - (rowDiff * 2);
        else if (firstPoint.Row > secondPoint.Row)
            firstAntiRow = secondPoint.Row + (rowDiff * 2);
        else
            firstAntiRow = firstPoint.Row;

        if (firstPoint.Column < secondPoint.Column)
            firstAntiColumn = secondPoint.Column - (colDiff * 2);
        else if (firstPoint.Column > secondPoint.Column)
            firstAntiColumn = secondPoint.Column + (colDiff * 2);
        else
            firstAntiColumn = firstPoint.Column;

        result.Add((firstAntiRow, firstAntiColumn));

        // Now start at the first point and move to the second point
        int secondAntiRow;
        int secondAntiColumn;

        if (secondPoint.Row < firstPoint.Row)
            secondAntiRow = firstPoint.Row - (rowDiff * 2);
        else if (secondPoint.Row > firstPoint.Row)
            secondAntiRow = firstPoint.Row + (rowDiff * 2);
        else
            secondAntiRow = firstPoint.Row;

        if (secondPoint.Column < firstPoint.Column)
            secondAntiColumn = firstPoint.Column - (colDiff * 2);
        else if (secondPoint.Column > firstPoint.Column)
            secondAntiColumn = firstPoint.Column + (colDiff * 2);
        else
            secondAntiColumn = firstPoint.Column;

        result.Add((secondAntiRow, secondAntiColumn));

        return result;
    }

    private static List<(int Row, int Column)> DetermineAntinodesPart2(
        (int Row, int Column) firstPoint,
        (int Row, int Column) secondPoint
    )
    {
        // Stations themselves are antinodes in Part 2, so add these to result right away
        var result = new List<(int, int)>() { firstPoint, secondPoint };

        var rowDiff = Math.Abs(firstPoint.Row - secondPoint.Row);
        var colDiff = Math.Abs(firstPoint.Column - secondPoint.Column);

        // Take first point and create the antinode using it as a frame of reference
        // Start at the second point and "move to" the first point, but double the values
        var currentFirst = firstPoint;
        var currentSecond = secondPoint;

        for (int i = 0; i < NumberOfLoops; i++)
        {
            int antiRow;
            int antiColumn;

            if (currentFirst.Row < currentSecond.Row)
                antiRow = currentSecond.Row - (rowDiff * 2);
            else if (currentFirst.Row > currentSecond.Row)
                antiRow = currentSecond.Row + (rowDiff * 2);
            else
                antiRow = currentFirst.Row;

            if (currentFirst.Column < currentSecond.Column)
                antiColumn = currentSecond.Column - (colDiff * 2);
            else if (currentFirst.Column > currentSecond.Column)
                antiColumn = currentSecond.Column + (colDiff * 2);
            else
                antiColumn = currentFirst.Column;

            result.Add((antiRow, antiColumn));

            currentSecond = currentFirst;
            currentFirst = (antiRow, antiColumn);
        }

        // Reset so we can go in other direction
        currentFirst = firstPoint;
        currentSecond = secondPoint;

        // Now start at the first point and move to the second point
        for (int i = 0; i < NumberOfLoops; i++)
        {
            int antiRow;
            int antiColumn;

            if (currentSecond.Row < currentFirst.Row)
                antiRow = currentFirst.Row - (rowDiff * 2);
            else if (currentSecond.Row > currentFirst.Row)
                antiRow = currentFirst.Row + (rowDiff * 2);
            else
                antiRow = currentFirst.Row;

            if (currentSecond.Column < currentFirst.Column)
                antiColumn = currentFirst.Column - (colDiff * 2);
            else if (currentSecond.Column > currentFirst.Column)
                antiColumn = currentFirst.Column + (colDiff * 2);
            else
                antiColumn = currentFirst.Column;

            result.Add((antiRow, antiColumn));

            currentFirst = currentSecond;
            currentSecond = (antiRow, antiColumn);
        }

        return result;
    }

    protected override string SolveSecond()
    {
        var board = new List<char[]>();
        foreach (var line in InputFromFile)
            board.Add(line.ToCharArray());

        var stationDict = new Dictionary<char, List<(int, int)>>();

        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] != '.')
                {
                    var stationChar = board[i][j];

                    if (stationDict.TryGetValue(stationChar, out var stationList))
                        stationList.Add((i, j));
                    else
                        stationDict[stationChar] = [(i, j)];
                }
            }
        }

        var possibleAntinodes = new List<(int Row, int Column)>();

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

                    possibleAntinodes.AddRange(
                        DetermineAntinodesPart2(stationCoords[i], stationCoords[j])
                    );
                }
            }
        }

        var uniqueAntinodeLocations = new HashSet<(int Row, int Column)>();

        foreach (var (Row, Column) in possibleAntinodes)
        {
            try
            {
                var _ = board[Row][Column];

                uniqueAntinodeLocations.Add((Row, Column));
            }
            catch (Exception ex)
                when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException) { }
        }

        return uniqueAntinodeLocations.Count.ToString();
    }
}
