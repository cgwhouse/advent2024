using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace advent2024.Days;

public class Day6(int day) : BaseDay(day)
{
    private static readonly char[] Directions = ['<', '>', '^', 'v'];
    private const int VisitCount = 10;

    protected override string SolveFirst()
    {
        // Build board and find starting position
        var board = new List<List<char>>();

        foreach (var line in InputFromFile)
            board.Add([.. line.ToCharArray()]);

        int row = -1;
        int column = -1;

        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Count; j++)
            {
                if (Directions.Contains(board[i][j]))
                {
                    row = i;
                    column = j;
                    break;
                }
            }
        }

        var uniquePositions = new HashSet<(int, int)>();

        while (true)
        {
            // We are not out of the grid, meaning the tile we're currently on counts
            // as a valid position, increment result
            uniquePositions.Add((row, column));

            // These checks for "are we being obstructed currently" will fail
            // at the edge / when we're ready to exit the board, so return result then
            try
            {
                // See if we can move forward in the direction we are facing
                // If not, rotate and then move in that direction instead
                if (board[row][column] == '>' && board[row][column + 1] == '#')
                {
                    board[row][column] = 'v';
                }
                else if (board[row][column] == 'v' && board[row + 1][column] == '#')
                {
                    board[row][column] = '<';
                }
                else if (board[row][column] == '<' && board[row][column - 1] == '#')
                {
                    board[row][column] = '^';
                }
                else if (board[row][column] == '^' && board[row - 1][column] == '#')
                {
                    board[row][column] = '>';
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return uniquePositions.Count.ToString();
            }

            var currentPosition = board[row][column];
            board[row][column] = '.';

            // We are now rotated in the right direction (if needed), move forward based on direction
            switch (currentPosition)
            {
                case '>':
                    column++;
                    break;
                case 'v':
                    row++;
                    break;
                case '<':
                    column--;
                    break;
                case '^':
                    row--;
                    break;
            }

            board[row][column] = currentPosition;
        }
    }

    protected override string SolveSecond()
    {
        var board = new List<List<char>>();

        foreach (var line in InputFromFile)
            board.Add([.. line.ToCharArray()]);

        int startingRow = -1;
        int startingColumn = -1;

        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Count; j++)
            {
                if (Directions.Contains(board[i][j]))
                {
                    startingRow = i;
                    startingColumn = j;
                    break;
                }
            }
        }

        var result = 0;

        Parallel.For(
            0,
            board.Count,
            new ParallelOptions() { MaxDegreeOfParallelism = 5 },
            i =>
            {
                Parallel.For(
                    0,
                    board[i].Count,
                    new ParallelOptions() { MaxDegreeOfParallelism = 2 },
                    j =>
                    {
                        // Skip this trial if it's the starting location of the guard
                        if (Directions.Contains(board[i][j]))
                            return;

                        var testBoard = new List<List<char>>();
                        foreach (var row in board)
                        {
                            var testRow = new List<char>(row);
                            testBoard.Add(testRow);
                        }

                        if (testBoard[i][j] == '#')
                            return;

                        testBoard[i][j] = '#';

                        if (!CanEscape(testBoard, startingRow, startingColumn))
                            result++;
                    }
                );
            }
        );

        return result.ToString();
    }

    private static bool CanEscape(List<List<char>> board, int row, int column)
    {
        var attemptDict = new Dictionary<(int, int), int>();

        while (true)
        {
            try
            {
                // Assume currently stuck
                var stuck = true;

                while (stuck)
                {
                    if (board[row][column] == '>' && board[row][column + 1] == '#')
                    {
                        if (attemptDict.ContainsKey((row, column + 1)))
                        {
                            attemptDict[(row, column + 1)]++;

                            if (attemptDict[(row, column + 1)] == VisitCount)
                                return false;
                        }
                        else
                        {
                            attemptDict[(row, column + 1)] = 1;
                        }

                        board[row][column] = 'v';
                    }
                    else if (board[row][column] == 'v' && board[row + 1][column] == '#')
                    {
                        if (attemptDict.ContainsKey((row + 1, column)))
                        {
                            attemptDict[(row + 1, column)]++;

                            if (attemptDict[(row + 1, column)] == VisitCount)
                                return false;
                        }
                        else
                        {
                            attemptDict[(row + 1, column)] = 1;
                        }

                        board[row][column] = '<';
                    }
                    else if (board[row][column] == '<' && board[row][column - 1] == '#')
                    {
                        if (attemptDict.ContainsKey((row, column - 1)))
                        {
                            attemptDict[(row, column - 1)]++;

                            if (attemptDict[(row, column - 1)] == VisitCount)
                                return false;
                        }
                        else
                        {
                            attemptDict[(row, column - 1)] = 1;
                        }

                        board[row][column] = '^';
                    }
                    else if (board[row][column] == '^' && board[row - 1][column] == '#')
                    {
                        if (attemptDict.ContainsKey((row - 1, column)))
                        {
                            attemptDict[(row - 1, column)]++;

                            if (attemptDict[(row - 1, column)] == VisitCount)
                                return false;
                        }
                        else
                        {
                            attemptDict[(row - 1, column)] = 1;
                        }

                        board[row][column] = '>';
                    }
                    else
                    {
                        stuck = false;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // We escaped
                return true;
            }

            var currentPosition = board[row][column];
            board[row][column] = '.';

            switch (currentPosition)
            {
                case '>':
                    column++;
                    break;
                case 'v':
                    row++;
                    break;
                case '<':
                    column--;
                    break;
                case '^':
                    row--;
                    break;
            }

            board[row][column] = currentPosition;
        }
    }
}
