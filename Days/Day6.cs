using System;
using System.Collections.Generic;
using System.Linq;

namespace advent2024.Days;

public class Day6(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        // Build board and find starting position
        var board = new List<List<char>>();

        foreach (var line in InputFromFile)
            board.Add(line.ToCharArray().ToList());

        int row = -1;
        int column = -1;

        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Count; j++)
            {
                if (new char[] { '<', '>', '^', 'v' }.Contains(board[i][j]))
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
        // Build board and find starting position
        var board = new List<List<char>>();

        foreach (var line in InputFromFile)
            board.Add(line.ToCharArray().ToList());

        int startingRow = -1;
        int startingColumn = -1;

        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Count; j++)
            {
                if (new char[] { '<', '>', '^', 'v' }.Contains(board[i][j]))
                {
                    startingRow = i;
                    startingColumn = j;
                    break;
                }
            }
        }

        var result = 0;

        // Traverse the whole board, pretending we put a new obstacle at each location on the board
        // (unless it's the starting location of the guard)
        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board.Count; j++)
            {
                // Skip this trial if it's the starting location of the guard
                if (new char[] { '<', '>', '^', 'v' }.Contains(board[i][j]))
                    continue;

                var testBoard = board;
                testBoard[i][j] = '#';

                if (!CanEscape(testBoard, startingRow, startingColumn))
                    result++;
            }
        }

        return result.ToString();
    }

    private static bool CanEscape(List<List<char>> board, int row, int column)
    {
        //var uniquePositions = new HashSet<(int, int)>();
        var attempts = 0;

        while (true)
        {
            if (attempts == 1000000)
                return false;

            // We are not out of the grid, meaning the tile we're currently on counts
            // as a valid position, increment result
            //uniquePositions.Add((row, column));

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
                // We escaped
                return true;
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
            attempts++;
        }
    }
}
