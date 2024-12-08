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

        if (row < 0 || column < 0)
            throw new Exception("Could not find starting position");

        var uniquePositions = new HashSet<(int, int)>();

        while (true)
        {
            // Check if we're out of the grid yet
            //try
            //{
            //    var _ = board[row][column];
            //}
            //catch (ArgumentOutOfRangeException)
            //{
            //    return uniquePositions.Count.ToString();
            //}

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
                default:
                    throw new Exception("Something went wrong");
            }

            board[row][column] = currentPosition;
        }
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
