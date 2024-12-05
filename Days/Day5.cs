using System;

namespace advent2024.Days;

public class Day5(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        // dictionary of key, value is list of things that must come after
        // for each number in the list of pages
        // look at everything up until the number we're currently on
        // if any of those numbers leading up to current number are in the "must come after"
        // list for that number, it is not correct

        throw new NotImplementedException();
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
