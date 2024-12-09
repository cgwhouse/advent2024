using System;
using System.Linq;

namespace advent2024.Days;

public class Day9(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var diskMap = InputFromFile.First() ?? throw new Exception();
        var diskMapExpanded = "";

        var currentID = 0;
        var isFile = true;

        for (int i = 0; i < diskMap.Length; i++)
        {
            var sizeOfThing = int.Parse(diskMap[i].ToString());

            // Add however many chars to the expanded disk map based on the size
            for (int j = 0; j < sizeOfThing; j++)
            {
                // If we're currently doing a file add the ID we're on
                // Otherwise just do dots
                if (isFile)
                    diskMapExpanded += currentID.ToString();
                else
                    diskMapExpanded += '.';
            }

            if (isFile)
                currentID++;

            isFile = !isFile;
        }

        Console.WriteLine(diskMapExpanded);

        throw new NotImplementedException();
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
