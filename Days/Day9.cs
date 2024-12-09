using System;
using System.Linq;

namespace advent2024.Days;

public class Day9(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var diskMap = InputFromFile.First() ?? throw new Exception();

        // First create the expanded version of the disk map with '.' for free space
        var diskMapExpanded = "";
        var currentID = 0;
        var isFile = true;

        for (int i = 0; i < diskMap.Length; i++)
        {
            var sizeOfThing = int.Parse(diskMap[i].ToString());

            for (int j = 0; j < sizeOfThing; j++)
            {
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
